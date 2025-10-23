using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject farmerPrefab;
    public GameObject[] chickenPrefabs; // Chicken0~3

    public Transform[] spawnPoints; // 스폰 위치 미리 세팅 (5개)

    void Start()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        // 내 역할 가져오기
        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("Role", out object role))
        {
            GameObject prefabToSpawn = null;

            if ((string)role == "Farmer")
            {
                prefabToSpawn = farmerPrefab;
            }
            else if ((string)role == "Chicken")
            {
                // 치킨은 PlayerList에서 Chicken 순서에 따라 prefab 결정
                int chickenIndex = 0;
                var players = PhotonNetwork.PlayerList;
                for (int i = 0; i < players.Length; i++)
                {
                    if (players[i].CustomProperties.TryGetValue("Role", out object r) && (string)r == "Chicken")
                    {
                        if (players[i] == PhotonNetwork.LocalPlayer)
                        {
                            chickenIndex = i; // PlayerList에서 몇 번째 Chicken인지 확인
                            break;
                        }
                    }
                }

                // chickenPrefabs 배열 범위 안에 맞추기
                chickenIndex = Mathf.Clamp(chickenIndex - 1, 0, chickenPrefabs.Length - 1);
                prefabToSpawn = chickenPrefabs[chickenIndex];
            }

            // PhotonNetwork.Instantiate로 네트워크 동기화
            if (prefabToSpawn != null)
            {
                // spawnPoints 배열에서 내 번호에 맞는 위치 사용
                Transform spawn = spawnPoints[PhotonNetwork.LocalPlayer.ActorNumber - 1];
                GameObject player = PhotonNetwork.Instantiate(prefabToSpawn.name, spawn.position, spawn.rotation);

                CameraController cameraController = player.GetComponentInChildren<CameraController>();
                if (cameraController != null)
                {
                    cameraController.playerBody = player.transform;
                }
            }
        }
    }
}
