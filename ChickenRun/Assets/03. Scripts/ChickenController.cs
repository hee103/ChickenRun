
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChickenController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    public Vector2 curMovementInput;
    private int jumpCount = 0;         
    private int maxJumpCount = 1;     
    private bool isGrounded = false;
    public int chickenHp = 100;

    private Rigidbody _rigidbody;
    private Animator animator;
    [SerializeField]private ParticleSystem dustParticle;
    public Transform cameraTransform;
    public TextMeshProUGUI text;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        //dustParticle = GetComponent<ParticleSystem>();
    }

    void Start()
    {
       Cursor.lockState = CursorLockMode.Locked;
    }
    void FixedUpdate()
    {
        GroundCheck();
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
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = forward * curMovementInput.y + right * curMovementInput.x;
        moveDir *= moveSpeed;
        moveDir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = moveDir;
    }
    void GroundCheck()
    {
        float rayDistance = 0.1f;
        Vector3 rayOrigin = _rigidbody.position + Vector3.up * 0.1f; 

        Debug.DrawRay(rayOrigin, Vector3.down * rayDistance, Color.green);

        RaycastHit hit;
        bool isHit = Physics.Raycast(rayOrigin, Vector3.down, out hit, rayDistance, LayerMask.GetMask("Ground"));

        if (isHit)
        {
            isGrounded = true;
            jumpCount = 0;  
            animator.SetBool("IsJump", false);
        }
        else
        {
            isGrounded = false;
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
            if (jumpCount < maxJumpCount)
            {
                animator.SetTrigger("Jump");
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z); 
                _rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                jumpCount++;
            }

        }

    }

    public void OnDamaged(int power)
    {
        chickenHp -= power;
        if (chickenHp < 0) chickenHp = 0;
        UpdateHpUI();
    }

    private void UpdateHpUI()
    {
        text.text = chickenHp.ToString();
    }


 
}
