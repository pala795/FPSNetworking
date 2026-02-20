using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private InputField usernameInput; 
    
    public void OnClickJoin()
    {
        string username = usernameInput.text;

        //LobbyManager.Instance.JoinRoom(username, room);
    }
}
