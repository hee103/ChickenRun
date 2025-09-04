using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    private Vector2 lookInput;  
    private float xRotation = 0f;
    private PhotonView pv;

    private void Awake()
    {
        pv = GetComponentInParent<PhotonView>();
        if (!pv.IsMine)
        {
            enabled = false;
            GetComponent<Camera>().enabled = false;
        }
    }

    void Update()
    {
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
}