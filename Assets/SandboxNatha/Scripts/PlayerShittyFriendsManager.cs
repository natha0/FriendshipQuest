using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShittyFriendsManager : MonoBehaviour { 
    public List<(int number, GameObject shittyFriend)> shittyFriendsList = new();
    public List<string> shittyFriendsTypes = new();

    public bool AddShittyFriend(GameObject shittyFriend)
    {
        string type = shittyFriend.GetComponent<ShittyFriend>().type;
        if (type != null)
        {
            bool doDestroyShittyFriend;
            if (shittyFriendsTypes.Contains(type))
            {
                int index = shittyFriendsTypes.IndexOf(type);
                shittyFriendsList[index] = (shittyFriendsList[index].number + 1, shittyFriendsList[index].shittyFriend);
                doDestroyShittyFriend = true;
            }
            else
            {
                shittyFriendsList.Add((1, shittyFriend));
                shittyFriendsTypes.Add(type);
                doDestroyShittyFriend = false;
                if (shittyFriendsList.Count == 1)
                {
                    ShittyFriendsCounter.Instance.SetSelectedShittyFriend(type);
                }
            }
            UpdateShittyFriendCounter(type);
            return doDestroyShittyFriend;
        }
        else
        {
            Debug.LogWarningFormat("The shitty Friend of type {} has not been implemented", type);
            return true;
        }
        
    }

    public GameObject GetShittyFriend(int id)
    {
        if (shittyFriendsList.Count >= id)
        {
            return shittyFriendsList[id].shittyFriend;
        }

        Debug.LogWarningFormat("The shitty friend with id: {0} doesn't exist!", id);
        return null;
    }

    public void UseShittyFriend()
    {
        if (shittyFriendsList.Count >= 1)
        {
            bool PowerUsed = shittyFriendsList[0].shittyFriend.GetComponent<IShittyFriends>().UsePower();
            if(PowerUsed)
            {
                string type = shittyFriendsList[0].shittyFriend.GetComponent<ShittyFriend>().type;
                if (shittyFriendsList[0].number == 1)
                {
                    Destroy(shittyFriendsList[0].shittyFriend);
                    shittyFriendsList.RemoveAt(0);
                    shittyFriendsTypes.RemoveAt(0);
                    for (int i = 0; i < shittyFriendsList.Count; i++)
                    {
                        shittyFriendsList[i].shittyFriend.GetComponent<ShittyFriend>().playerNumber--;
                    }
                    UpdateShittyFriendCounter(type, 0);
                    if (shittyFriendsTypes.Count > 0)
                    {
                        ShittyFriendsCounter.Instance.SetSelectedShittyFriend(shittyFriendsTypes[0]);
                    }
                    else
                    {
                        ShittyFriendsCounter.Instance.SetSelectedShittyFriend(type, false);
                    }
                }
                else
                {
                    shittyFriendsList[0] = (shittyFriendsList[0].number - 1, shittyFriendsList[0].shittyFriend);
                    UpdateShittyFriendCounter(type);
                }
            }
            
        }
            
        

    }

    public void SwitchShittyFriends()
    {
        if (shittyFriendsList.Count > 0)
        {
            shittyFriendsList.Add(shittyFriendsList[0]);
            shittyFriendsList.RemoveAt(0);

            shittyFriendsTypes.Add(shittyFriendsTypes[0]);
            shittyFriendsTypes.RemoveAt(0);

            for (int i = 0; i < shittyFriendsList.Count; i++)
            {
                shittyFriendsList[i].shittyFriend.GetComponent<ShittyFriend>().playerNumber = i;
            }
            ShittyFriendsCounter.Instance.SetSelectedShittyFriend(shittyFriendsTypes[0]);
        }
    }

    private void UpdateShittyFriendCounter(string type, int number = -1)
    {
        if (number == -1)
        {
            int index = shittyFriendsTypes.IndexOf(type);
            number = shittyFriendsList[index].number;
        }
        ShittyFriendsCounter.Instance.SetShittyFriendCount(type, number);
    }
}
