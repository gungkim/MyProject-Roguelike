using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    PlayerInput inputActions;

    Player player;

    EnemyBase enemy;

    private void Awake()
    {
        inputActions = new PlayerInput();
    }

    private void Start()
    {
        player = GameManager.Instance.Player;   
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Equip.performed += OnEquip;
        inputActions.Player.Equip.canceled += OnEquip;
    }

    private void OnDisable()
    {
        inputActions.Player.Equip.canceled -= OnEquip;
        inputActions.Player.Equip.performed -= OnEquip;
        inputActions.Player.Disable();
    }

    private void OnEquip(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {

        if (gameObject.activeSelf)
        {
            OpenInventory();
        }
        else
        {
            CloseInventory();
        }
    }

    private void OpenInventory()
    {
        gameObject.SetActive(true);
        inputActions.Player.Disable();
    }

    private void CloseInventory()
    {
        gameObject.SetActive(false);
        inputActions.Player.Enable();
    }
}
