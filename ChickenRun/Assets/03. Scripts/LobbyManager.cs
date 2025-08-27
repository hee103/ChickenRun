using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private string selectedRole;


    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); 
    }

  
    public void JoinRole(string role)
    {
        selectedRole = role;
        Debug.Log($"{selectedRole}을 선택했습니다");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 5;
        options.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable
        {
            { "FarmerCount", 0 },
            { "ChickenCount", 0 }
        };
        options.CustomRoomPropertiesForLobby = new string[] { "FarmerCount", "ChickenCount" };
        PhotonNetwork.CreateRoom(null, options);
    }

    public override void OnJoinedRoom()
    {
        var props = PhotonNetwork.CurrentRoom.CustomProperties;

        int farmerCount = (int)props["FarmerCount"];
        int chickenCount = (int)props["ChickenCount"];


        if (selectedRole == "Farmer")
        {
            if (farmerCount >= 1)
            {
                PhotonNetwork.LeaveRoom();
                Debug.Log("술래가 이미 있는 방입니다");
                return;
            }
            farmerCount++;
        }
        else
        {
            if (chickenCount >= 4)
            {
                PhotonNetwork.LeaveRoom();
                Debug.Log("도망자가 이미 다 찼습니다");
                return;
            }
            chickenCount++;
        }

  
        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable
        {
            { "FarmerCount", farmerCount },
            { "ChickenCount", chickenCount }
        });

        Debug.Log($"Joined as {selectedRole}");

  
        if (farmerCount == 1 && chickenCount == 4 && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("GameScene");
        }
    }
}
