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
    private BoxCollider2D weaponCollider2D;

    bool isPlayerAlive = true;

    Vector2 inputDirection = Vector2.zero;

    public Vector2 InputDirection => inputDirection;

    bool isMove = false;

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

        Transform child = transform.GetChild(1);
        weaponCollider2D = child.GetComponent<BoxCollider2D>();
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
    ///// 플레이어가 죽었을 때 이동 불가하게 만들기
    ///// </summary>
    //private void DiscconnectInput()
    //{
    //    playerInputActions.Player.Disable();
    //}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            IAttack attack = collision.GetComponent<IAttack>();
            if (attack != null)
            {
                playerStat.Damaged(attack.AttackPower);
                
            }
        }
    }


    private void OnStatsChanged()
    {
        // 필요한 작업 수행
        Debug.Log("Stats updated.");
        // 스탯 변경 시 수행할 추가 작업이 있다면 여기에 추가
    }

    /// <summary>
    /// 플레이어가 죽을 때의 함수
    /// </summary>
    public void PlayerDie()
    {

        if(PlayerStat.MaxHP <= 0)
        {
            isPlayerAlive = false;
            animator.SetTrigger(DieHash);

            playerInputActions.Player.Disable();
        }
    }

    public BoxCollider2D WeaponCollider
    {
        get { return weaponCollider2D; }
    }
}

// playerStat에서 체력, 이동속도 가져오기