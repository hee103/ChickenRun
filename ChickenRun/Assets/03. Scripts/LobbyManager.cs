using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private string selectedRole;

    private void Awake()
    {
        // 방장이 씬을 바꾸면 자동으로 다른 클라이언트도 씬을 로드하게 함
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("서버 연결 시도...");
    }

    /// 역할 선택 버튼에서 호출
    public void JoinRole(string role)
    {
        selectedRole = role;
        Debug.Log($"{selectedRole} 역할 선택");
        PhotonNetwork.JoinRandomRoom();
    }


    /// 참가 가능한 방이 없을 때 새로 생성
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions options = new RoomOptions
        {
            MaxPlayers = 5
        };

        PhotonNetwork.CreateRoom(null, options);
        Debug.Log("랜덤 방 참가 실패 → 새로운 방 생성");
    }

    /// 방에 들어갔을 때 내 역할을 PlayerProperties에 저장
    public override void OnJoinedRoom()
    {
        Debug.Log($"{PhotonNetwork.NickName} joined as {selectedRole}");

        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable
        {
            { "Role", selectedRole }
        });
    }


    /// 플레이어 속성이 바뀔 때마다 실행됨 
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        int farmerCount = 0;
        int chickenCount = 0;

        // 방에 속한 모든 플레이어들의 Role 집계
        foreach (var player in PhotonNetwork.PlayerList)
        {
            if (player.CustomProperties.TryGetValue("Role", out object role))
            {
                if ((string)role == "Farmer") farmerCount++;
                else if ((string)role == "Chicken") chickenCount++;
            }
        }

        Debug.Log($"현재 Farmer={farmerCount}, Chicken={chickenCount}, 방장={PhotonNetwork.IsMasterClient}");

        // 인원 다 채워졌으면 씬 이동
        if (farmerCount == 1 && chickenCount == 4 && PhotonNetwork.IsMasterClient)
        {
            Debug.Log("인원 다 채워짐! 방장이 Main 씬 로드");
            PhotonNetwork.LoadLevel("Main");
        }
    }
}
