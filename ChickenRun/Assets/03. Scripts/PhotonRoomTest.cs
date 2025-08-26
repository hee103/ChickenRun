using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonRoomTest : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("서버 연결 성공, 랜덤 룸 입장 시도...");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("랜덤 룸 입장 실패, 새로운 룸 생성");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("룸 입장 성공! 현재 룸 플레이어 수: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }
}
