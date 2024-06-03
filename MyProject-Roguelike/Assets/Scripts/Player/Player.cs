using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerInput playerInputActions;

    Animator animator;



    private void Awake()
    {
        playerInputActions = GetComponent<PlayerInput>();

        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();
        playerInputActions.Player.Move.performed += OnMove;
        playerInputActions.Player.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Move.performed -= OnMove;
        playerInputActions.Player.Move.canceled -= OnMove;
        playerInputActions.Player.Disable();
    }

    private void Update()
    {
        
    }

    private void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext _)
    {

    }
}
