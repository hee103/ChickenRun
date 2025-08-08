
using UnityEngine;
using UnityEngine.InputSystem;

public class ChickenController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    public Vector2 curMovementInput;

    private Rigidbody _rigidbody;
    private Animator animator;
    [SerializeField]private ParticleSystem dustParticle;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        //dustParticle = GetComponent<ParticleSystem>();
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
        animator.SetBool("IsMove", curMovementInput != Vector2.zero);
        if (curMovementInput != Vector2.zero)
        {
            dustParticle.Play();
        }
        else
        {
            dustParticle.Stop();
        }
        Vector3 dir = new Vector3(curMovementInput.x, 0, curMovementInput.y); 
        dir *= moveSpeed; // 해당 방향으로 움직일 수 있게 함
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
        if (curMovementInput != Vector2.zero)
        {
            if (curMovementInput.x > 0) 
                transform.rotation = Quaternion.Euler(0, 90, 0);
            else if (curMovementInput.x < 0) 
                transform.rotation = Quaternion.Euler(0, -90, 0);
            else if (curMovementInput.y > 0) 
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else if (curMovementInput.y < 0) 
                transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

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
            animator.SetBool("IsJump", true);
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }

    }

    private void JumpEnd()
    {
        animator.SetBool("IsJump", false);
    }    
}
