using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleCamTest : MonoBehaviour
{
    public Transform playerBody;
    public float sensitivity = 100f;
    private float xRot = 0f;
    private Vector2 look;

    void LateUpdate()
    {
        float mx = look.x * sensitivity * Time.deltaTime;
        float my = look.y * sensitivity * Time.deltaTime;

        xRot -= my;
        xRot = Mathf.Clamp(xRot, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);

        if (playerBody != null)
            playerBody.Rotate(Vector3.up * mx);

        Debug.Log($"plyY={playerBody.eulerAngles.y:F2}, camLocalY={transform.localEulerAngles.y:F2}");
    }

    public void OnLook(InputAction.CallbackContext ctx)
    {
        look = ctx.ReadValue<Vector2>();
    }
}
