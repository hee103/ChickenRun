
using UnityEngine;
using UnityEngine.InputSystem;

public class ChickenController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    public Vector2 curMovementInput;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
       Cursor.lockState = CursorLockMode.Locked;
    }
    void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x; //상하좌우 값을 통해 방향 
        dir *= moveSpeed; // 해당 방향으로 움직일 수 있게 함
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
    }

    //public void OnMove(InputValue value)
    //{
    //    curMovementInput = value.Get<Vector2>();
    //}

    //public void OnJump(InputValue value)
    //{
    //    _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
    //}
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero; // 벡터값에 아무것도 들어가면 안되기 때문에 0으로 만듦
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }

    }
}
