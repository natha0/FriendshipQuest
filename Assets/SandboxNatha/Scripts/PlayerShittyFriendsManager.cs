using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerShittyFriendsManager : MonoBehaviour { 
    public ShittyFriendManagerModule[] shittyFriendsList;
    private ShittyFriendManagerModule currentModule;
    private int ShittyFriendTotal
    {
        get
        { int sum = 0; foreach (ShittyFriendManagerModule mod in shittyFriendsList) { sum += mod.number; } return sum; }
    }

    private int ShittyFriendTypeCount { get { 
            int count = 0;
            foreach (ShittyFriendManagerModule mod in shittyFriendsList)
            {
                count += mod.number > 0 ? 1 : 0;
            }
            return count;
        } 
    }

    private void Start()
    {
        foreach(ShittyFriendManagerModule module in shittyFriendsList)
        {
            module.Initialise();
        }
    }

    public bool AddShittyFriend(GameObject shittyFriend)
    {
        bool shittyFriendAdded = false;

        string type = shittyFriend.GetComponent<ShittyFriend>().type;
        if (type != null)
        {
            ShittyFriendManagerModule module = Array.Find(shittyFriendsList,mod => mod.type == type);
            if (module.number < module.maxNumber)
            {
                module.number++;

                if (module.number == 1)
                {
                    module.shittyFriendClone = Instantiate(module.shittyFriend, shittyFriend.transform.position, Quaternion.identity);
                    module.orderNumber = ShittyFriendTypeCount - 1;

                    ShittyFriend cloneProperties = module.shittyFriendProperties;
                    cloneProperties.playerNumber = module.orderNumber;
                    cloneProperties.attached = true;

                    if (ShittyFriendTypeCount == 1)
                    {
                        currentModule = module;
                        ShittyFriendsCounter.Instance.SetSelectedShittyFriend(type);
                    }
                }

                UpdateShittyFriendCounter(type, module.number);
                shittyFriendAdded = true;
            }
        }
        else
        {
            Debug.LogWarningFormat("The shitty Friend of type {} has not been implemented", type);
        }
        return shittyFriendAdded;
    }

    public GameObject GetShittyFriend(int id)
    {
        if (shittyFriendsList.Length >= id)
        {
            ShittyFriendManagerModule module= Array.Find(shittyFriendsList, mod => mod.orderNumber == id);
            return module.shittyFriendClone;
        }

        Debug.LogWarningFormat("The shitty friend with id: {0} doesn't exist!", id);
        return null;
    }

    public void UseShittyFriend()
    {
        if (ShittyFriendTotal >= 1)
        {
            bool PowerUsed = currentModule.shittyFriendClone.GetComponent<IShittyFriends>().UsePower();
            if(PowerUsed)
            {
                currentModule.number--;
                string type = currentModule.type;
                if (currentModule.number == 0)
                {
                    Destroy(currentModule.shittyFriendClone);
                    currentModule.orderNumber = -1;
                    foreach (ShittyFriendManagerModule module in shittyFriendsList)
                    {
                        if (module.orderNumber != -1)
                        {
                            module.orderNumber--;
                            module.shittyFriendProperties.playerNumber--;
                        }
                    }

                    UpdateShittyFriendCounter(type, 0);
                    if (ShittyFriendTotal > 0)
                    {
                        currentModule = Array.Find(shittyFriendsList, mod => mod.orderNumber == 0);
                        ShittyFriendsCounter.Instance.SetSelectedShittyFriend(currentModule.type);
                    }
                    else
                    {
                        ShittyFriendsCounter.Instance.SetSelectedShittyFriend(type, false);
                    }
                }
                else
                {
                    UpdateShittyFriendCounter(type);
                }
            }
        }

    }

    public void SwitchShittyFriends()
    {
        if (ShittyFriendTotal > 0)
        {
            foreach(ShittyFriendManagerModule module in shittyFriendsList)
            {
                if (module.orderNumber > 0)
                {
                    module.orderNumber--;
                    module.shittyFriendProperties.playerNumber--;
                }
                else if (module.orderNumber == 0)
                {
                    module.orderNumber = ShittyFriendTypeCount - 1;
                    module.shittyFriendProperties.playerNumber = ShittyFriendTypeCount - 1;
                }
            }
            currentModule = Array.Find(shittyFriendsList,module => module.orderNumber==0);
            ShittyFriendsCounter.Instance.SetSelectedShittyFriend(currentModule.type);
        }
    }

    private void UpdateShittyFriendCounter(string type, int number = -1)
    {
        if (number == -1)
        {
            ShittyFriendManagerModule module = Array.Find(shittyFriendsList, mod => mod.type == type);
            number = module.number;
        }
        ShittyFriendsCounter.Instance.SetShittyFriendCount(type, number);
    }
}


