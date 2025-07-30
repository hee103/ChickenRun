using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;


public class CharacterController : MonoBehaviour
{
    public static CharacterController Instance { get; private set; }

    [SerializeField] GameObject characterPrefab;
    protected Vector2 moveInput;
    Rigidbody characterRigidbody;
    BoxCollider characterCollider;

    PlayerInputActions input;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Debug.Log("CharacterController 인스턴스 있음");
        characterRigidbody = GetComponent<Rigidbody>();
        characterCollider = GetComponent<BoxCollider>();
    }

    protected virtual void OnEnable()
    {
        input = new PlayerInputActions();
        input.Player.Enable();
        input.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    protected virtual void OnDisable()
    {
        input.Player.Disable();
    }

    public void Move(int speed)
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y) * speed * Time.fixedDeltaTime;
        Debug.Log($"Move called with input {moveInput} and move vector {move}");
        characterRigidbody.MovePosition(characterRigidbody.position + move);
    }
}
