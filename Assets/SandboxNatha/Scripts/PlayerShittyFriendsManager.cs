using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShittyFriendsManager : MonoBehaviour { 

    public List<GameObject> shittyFriendsList = new();

    public List<(int number, GameObject shittyFriend)> shittyFriendsListTest = new();

    public bool limitShittyFriendsNumber = false;
    public List<string> shittyFriendsTypes = new();

    public bool doDestroyShittyFriend;

    public void AddShittyFriend(GameObject shittyFriend)
    {
        if (!limitShittyFriendsNumber)
        {
            shittyFriendsList.Add(shittyFriend);
        }
        else
        {
            string type = shittyFriend.GetComponent<ShittyFriend>().type;
            if (type != null)
            {
                if (shittyFriendsTypes.Contains(type))
                {
                    int index = shittyFriendsTypes.IndexOf(type);
                    shittyFriendsListTest[index] = (shittyFriendsListTest[index].number + 1, shittyFriendsListTest[index].shittyFriend);
                    doDestroyShittyFriend = true;
                }
                else
                {
                    shittyFriendsListTest.Add((1, shittyFriend));
                    shittyFriendsTypes.Add(type);
                    doDestroyShittyFriend = false;
                }

                UpdateShittyFriendCounter(type);

            }
            else
            {
                Debug.LogWarningFormat("The shitty Friend of type {} has not been implemented", type);
            }
        }
    }

    public GameObject GetShittyFriend(int id)
    {
        if (!limitShittyFriendsNumber)
        {
            if (shittyFriendsList.Count >= id)
            {
                return shittyFriendsList[id];
            }
        }
        else
        {
            return shittyFriendsListTest[id].shittyFriend;
        }

        Debug.LogWarningFormat("The shitty friend with id: {0} doesn't exist!", id);
        return null;
    }

    public void UseShittyFriend()
    {

        if (!limitShittyFriendsNumber)
        {
            if (shittyFriendsList.Count >= 1)
            {
                shittyFriendsList[0].GetComponent<IShittyFriends>().UsePower();
                Destroy(shittyFriendsList[0]);
                shittyFriendsList.RemoveAt(0);
                foreach (GameObject shittyFriend in shittyFriendsList)
                {
                    shittyFriend.GetComponent<ShittyFriend>().playerNumber--;
                }
            }
        }
        else
        {
            if (shittyFriendsListTest.Count >= 1)
            {
                shittyFriendsListTest[0].shittyFriend.GetComponent<IShittyFriends>().UsePower();
                string type = shittyFriendsListTest[0].shittyFriend.GetComponent<ShittyFriend>().type;
                if (shittyFriendsListTest[0].number == 1)
                {
                    
                    Destroy(shittyFriendsListTest[0].shittyFriend);
                    shittyFriendsListTest.RemoveAt(0);
                    shittyFriendsTypes.RemoveAt(0);
                    for (int i=0;i< shittyFriendsListTest.Count; i++)
                    {
                        shittyFriendsListTest[i].shittyFriend.GetComponent<ShittyFriend>().playerNumber--;
                    }

                    UpdateShittyFriendCounter(type, 0);

                }
                else
                {
                    shittyFriendsListTest[0] = (shittyFriendsListTest[0].number - 1, shittyFriendsListTest[0].shittyFriend);
                    UpdateShittyFriendCounter(type);
                }
            }
            
        }

    }

    public void SwitchShittyFriends()
    {
        if (!limitShittyFriendsNumber) 
        {
            if (shittyFriendsList.Count > 0)
            {
                shittyFriendsList.Add(shittyFriendsList[0]);
                shittyFriendsList.RemoveAt(0);

                for (int i = 0; i < shittyFriendsList.Count; i++)
                {
                    shittyFriendsList[i].GetComponent<ShittyFriend>().playerNumber = i;
                }
            }

        }
        else
        {
            if (shittyFriendsListTest.Count > 0)
            {
                shittyFriendsListTest.Add(shittyFriendsListTest[0]);
                shittyFriendsListTest.RemoveAt(0);

                shittyFriendsTypes.Add(shittyFriendsTypes[0]);
                shittyFriendsTypes.RemoveAt(0);

                for (int i = 0; i < shittyFriendsListTest.Count; i++)
                {
                    shittyFriendsListTest[i].shittyFriend.GetComponent<ShittyFriend>().playerNumber = i;
                }
            }
                
        }

    }

    private void UpdateShittyFriendCounter(string type, int number = -1)
    {
        if (number == -1)
        {
            int index = shittyFriendsTypes.IndexOf(type);
            number = shittyFriendsListTest[index].number;
        }
        ShittyFriendsCounter.Instance.SetShittyFriendCount(type, number);
    }
}
