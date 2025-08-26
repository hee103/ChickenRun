using Photon.Pun;
using UnityEngine;

public class PhotonConnectTest : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // Photon 서버에 연결 시도
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Photon 연결 시도 중...");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Photon 서버 연결 성공!");
    }

    public override void OnDisconnected(Photon.Realtime.DisconnectCause cause)
    {
        Debug.Log("Photon 서버 연결 실패: " + cause.ToString());
    }
}
