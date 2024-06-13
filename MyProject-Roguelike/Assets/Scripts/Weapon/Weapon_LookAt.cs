using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon_LookAt : WeaponBase
{
    new GameObject gameObject;

    private Vector2 inputDirection;
    private Vector2 lastInputDirection;

    PlayerInput inputActions;

    protected override void Awake()
    {
        base.Awake();

        inputActions = new PlayerInput();
    }

    private new void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMovePerfomed;
    }


    private new void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMovePerfomed;
        inputActions.Player.Disable();
    }
    private void OnMovePerfomed(InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();
        if (inputDirection != Vector2.zero)
        {
            lastInputDirection = inputDirection;
        }
    }

    protected override void Fire()
    {
        Vector2 direction = inputDirection != Vector2.zero ? inputDirection : lastInputDirection;

        if (direction != Vector2.zero)
        {
            rigidbody2d.velocity = direction.normalized * (itemData_Weapon.attackSpeed + playerStat.AttackSpeed);
        }
    }
}
