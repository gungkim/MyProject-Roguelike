using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerInput playerInputActions;

    Animator animator;

    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rigid;

    bool isPlayerAlive = true;

    Vector2 inputDirection = Vector2.zero;

    bool isMove = false;

    readonly int InputX_Hash = Animator.StringToHash("InputX");
    readonly int InputY_Hash = Animator.StringToHash("InputY");
    readonly int IsMove_Hash = Animator.StringToHash("IsMove");

    private void Awake()
    {
        playerInputActions = new PlayerInput();

        animator = GetComponent<Animator>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();
        playerInputActions.Player.Move.performed += OnMove;
        playerInputActions.Player.Move.canceled += OnStop;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Move.performed -= OnMove;
        playerInputActions.Player.Move.canceled -= OnStop;
        playerInputActions.Player.Disable();
    }



    private void Update()
    {
        
    }

    private void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();
        animator.SetFloat(InputX_Hash, inputDirection.x);
        animator.SetFloat(InputY_Hash, inputDirection.y);
        isMove = true;
        animator.SetBool(IsMove_Hash, isMove);
    }

    private void OnStop(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }
}
