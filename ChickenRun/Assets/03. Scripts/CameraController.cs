using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    private float xRotation = 0f;
    private Vector2 lookInput;
    private PhotonView pv;

    private void Awake()
    {
        pv = GetComponentInParent<PhotonView>();
        if (pv != null && !pv.IsMine)
        {
            enabled = false;
            GetComponentInChildren<Camera>().enabled = false;
        }
    }

    void Update()
    {
        //transform.position = playerBody.position;
        transform.rotation = playerBody.rotation * Quaternion.Euler(xRotation, 0f, 0f);

        if (pv != null && !pv.IsMine) return;

        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        // 상하 회전 (CameraRoot 자체 회전)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // 좌우 회전 (플레이어 몸 회전)
        playerBody.Rotate(Vector3.up * mouseX);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
}
