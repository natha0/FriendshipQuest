using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 7.0f;
    public float rotationSmoothness=40f;
    private float horizontalInput;
    private float verticallInput;

    private bool canDash = true;
    [HideInInspector] public bool isDashing;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;

    public bool lookAtMouse = true;
    private Vector3 movementDirection;
    [SerializeField] private Rigidbody rb;
    [SerializeField] public TrailRenderer tr;

    private Animator animator;
    private AudioManager audioManager;

    private Vector3 initialPosition;

    private Player player;

    private bool isDialogue;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        initialPosition = transform.position;
        player = GetComponent<Player>();
        isDialogue = false;

        DialogueSystem.Instance.DialogueStart += DialogueStart;
        DialogueSystem.Instance.DialogueEnd += DialogueEnd;
    }

    void Update()
    {
        if (isDashing || player.isGameOver || isDialogue)
        {
            return;
        }
        horizontalInput = Input.GetAxis("Horizontal");
        verticallInput = Input.GetAxis("Vertical");
        movementDirection = (verticallInput * Vector3.forward + horizontalInput * Vector3.right);
        if (movementDirection.magnitude>1)
        {
            movementDirection = movementDirection.normalized;
        }
        

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        if (movementDirection.magnitude > .1f)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDashing || player.isGameOver || isDialogue)
        {
            return;
        }

        rb.velocity = speed * movementDirection;

        if (lookAtMouse)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new(Vector3.up, Vector3.up);
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                Vector3 target = ray.GetPoint(distance);
                Vector3 direction = target - transform.position;
                float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, rotation, 0);
            }
        }
        else
        {
            if (movementDirection.magnitude >= 0.1)
            {
                //transform.rotation = Quaternion.LookRotation(movementDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementDirection), Time.deltaTime * rotationSmoothness);
            }
            else
            {
                rb.angularVelocity = new Vector3(0, 0, 0);
            }
        }

        if(Mathf.Abs(transform.position.y-initialPosition.y) > 0.3f)
        {
            transform.position = new Vector3(transform.position.x,initialPosition.y,transform.position.z);
        }

        
    }

    private IEnumerator Dash()
    {
        if (movementDirection.magnitude > 0.1)
        {
            canDash = false;
            isDashing = true;
            rb.useGravity = false;
            rb.velocity = dashingPower * movementDirection.normalized;
            tr.emitting = true;
            audioManager.Play("Dash");
            yield return new WaitForSeconds(dashingTime);
            tr.emitting = false;
            rb.useGravity = true;
            isDashing = false;
            yield return new WaitForSeconds(dashingCooldown);
            canDash = true;
        }
    }

    private void DialogueStart()
    {
        isDialogue = true;
    }

    private void DialogueEnd()
    {
        isDialogue = false;
    }

}
