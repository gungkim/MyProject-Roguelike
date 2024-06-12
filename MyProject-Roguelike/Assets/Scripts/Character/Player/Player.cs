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
    private BoxCollider2D boxCollider2D;

    bool isPlayerAlive = true;

    Vector2 inputDirection = Vector2.zero;

    bool isMove = false;

    bool isAlive = true;

    readonly int InputX_Hash = Animator.StringToHash("InputX");
    readonly int InputY_Hash = Animator.StringToHash("InputY");
    readonly int IsMove_Hash = Animator.StringToHash("IsMove");
    readonly int DieHash = Animator.StringToHash("IsDie");
    internal readonly Vector3 position;
    private PlayerStat playerStat;

    public PlayerStat PlayerStat { get { return playerStat; } }

    public CharacterData characterData;

    private void Awake()
    {
        playerInputActions = new PlayerInput();

        animator = GetComponent<Animator>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        rigid = GetComponent<Rigidbody2D>();

        playerStat = GetComponent<PlayerStat>();
    }

    private void Start()
    {
        playerStat.Character = characterData;
        playerStat.OnStatsChanged += OnStatsChanged;
        Debug.Log($"{playerStat.MoveSpeed}");
    }

    private void OnEnable()
    {
        if (isPlayerAlive)
        {
            playerInputActions.Player.Enable();
            playerInputActions.Player.Move.performed += OnMove;
            playerInputActions.Player.Move.canceled += OnStop;
        }
    }

    private void OnDisable()
    {
        if (isPlayerAlive)
        {
             playerInputActions.Player.Move.canceled -= OnStop;
            playerInputActions.Player.Move.performed -= OnMove;
            playerInputActions.Player.Disable();
        }
    }


    private void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + Time.fixedDeltaTime * playerStat.MoveSpeed * inputDirection);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();
        animator.SetFloat(InputX_Hash, inputDirection.x);
        animator.SetFloat(InputY_Hash, inputDirection.y);
        isMove = true;
        animator.SetBool(IsMove_Hash, isMove);
    }

    private void OnStop(InputAction.CallbackContext context)
    {
        inputDirection = Vector2.zero;
        isMove = false;
        animator.SetBool(IsMove_Hash, isMove);
    }

    ///// <summary>
    ///// �÷��̾ �׾��� �� �̵� �Ұ��ϰ� �����
    ///// </summary>
    //private void DiscconnectInput()
    //{
    //    playerInputActions.Player.Disable();
    //}

    private void OnStatsChanged()
    {
        // �ʿ��� �۾� ����
        Debug.Log("Stats updated.");
        // ���� ���� �� ������ �߰� �۾��� �ִٸ� ���⿡ �߰�
    }

    private void PlayerDie()
    {

        if(PlayerStat.MaxHP <= 0)
        {
            isAlive = false;
            animator.SetTrigger(DieHash);

            playerInputActions.Player.Disable();
        }
    }


}

// playerStat���� ü��, �̵��ӵ� ��������
// �÷��̾� hp�� 0�� �Ǿ��� �� �״� �ִϸ��̼� �߰��ϱ�