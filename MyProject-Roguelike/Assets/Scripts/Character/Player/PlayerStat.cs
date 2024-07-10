using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class PlayerStat : MonoBehaviour, IAttack
{
    [Header("캐릭터의 스탯")]
    private int playerAttackPower;
    private int defense;
    private float maxHP;
    private float currentHP;
    private float criticalChance;
    private float attackRange;
    private float attackSpeed;
    private float moveSpeed;
    private float coolTime;
    private int expGain;

    private ItemData_Accessory itemData_Accessory;

    private List<ItemData_Accessory> accessories = new List<ItemData_Accessory>();
    private List<ItemData_Weapon> weapons = new List<ItemData_Weapon>();

    private CharacterData characterData;

    private ExpBarUI expBarUI;

    // 이벤트 정의
    public event Action OnStatsChanged;

    public event Action OnLevelUp;

    public int Level { get; private set; } = 1;
    public int CurrentEXP { get; private set; } = 0;
    public int XPToNextLevel { get; private set; } = 100;


    public ItemData_Accessory ItemData_Accessory
    {
        get { return itemData_Accessory; }
        set
        {
            itemData_Accessory = value;
            RecalculateStats();
        }
    }

    public CharacterData Character
    {
        get { return characterData; }
        set
        {
            characterData = value;
            if (characterData != null)
            {
                CalculateStats();
            }
            else
            {
                Debug.LogError("CharacterData is null when assigned to PlayerStat!");
            }
        }
    }

    public void Start()
    {
        Player player = GetComponent<Player>();
        characterData = player.characterData;
        Level = 1;
        CurrentEXP = 0;
        XPToNextLevel = CalculateExpToNextLevel(Level);
        if (characterData != null)
        {
            CalculateStats();
            currentHP = maxHP;
        }
        else
        {
            Debug.LogError("CharacterData is not assigned at Start!");
        }


        Debug.Log($"currentHP : {currentHP}, totalEXP : {CurrentEXP}");
    }

    private void CalculateStats()
    {
        if (characterData == null)
        {
            Debug.LogError("CharacterData is not assigned!");
            return;
        }

        playerAttackPower = characterData.playerAttackPower;
        defense = characterData.defense;
        maxHP = characterData.maxHP;
        criticalChance = characterData.criticalChance;
        attackRange = characterData.attackRange;
        attackSpeed = characterData.attackSpeed;
        moveSpeed = characterData.moveSpeed;
        coolTime = characterData.coolDown;
        expGain = characterData.expGain;

        if (itemData_Accessory != null)
        {
            playerAttackPower += itemData_Accessory.damage;
            defense += itemData_Accessory.defense;
            maxHP += itemData_Accessory.maxHP;
            attackRange += itemData_Accessory.attackRange;
            moveSpeed += itemData_Accessory.moveSpeed;
            attackSpeed += itemData_Accessory.attackSpeed;
            criticalChance += itemData_Accessory.criticalChance;
            coolTime += itemData_Accessory.coolDown;
            expGain += itemData_Accessory.expGain;
        }

        // 스탯이 변경되었음을 알리기 위해 이벤트 호출
        OnStatsChanged?.Invoke();
    }

    private void RecalculateStats()
    {
        // RecalculateStats 메서드는 필요시 구현
    }


    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public float MaxHP { get { return maxHP; } set { maxHP = value; } }
    public float CurrentHP { get { return currentHP; } set { currentHP = value; } }
    public int PlayerAttackPower { get { return playerAttackPower; } set { playerAttackPower = value; } }
    public float AttackRange { get { return attackRange; } set { attackRange = value; } }
    public int Defense { get { return defense; } set { defense = value; } }
    public float CriticalChance { get { return criticalChance; } set { criticalChance = value; } }

    public float CoolTime { get { return coolTime; } set { coolTime = value; } }

    public float AttackSpeed { get { return attackSpeed; }  set { attackSpeed = value; } }

    public int ExpGain { get { return expGain; }  set { expGain = value; } }

    private void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.CompareTag("Enemy"))
        {
            IAttack attack = collision.GetComponent<IAttack>();
            if (attack != null)
            {
                Damaged(attack.AttackPower);
            }
        }
    }

    public void GainExp(int baseExp)
    {
        int totalExp = baseExp;

        foreach (var accessory in accessories)
        {
            totalExp += (int)(baseExp * accessory.expGain / 100f);
        }

        CurrentEXP += totalExp;
        CheckLevelUp();
        OnStatsChanged?.Invoke();
        Debug.Log($"totalExp : {totalExp}");
    }

    private void CheckLevelUp()
    {
        while (CurrentEXP >= XPToNextLevel)
        {
            CurrentEXP -= XPToNextLevel;
            Level++;
            XPToNextLevel = CalculateExpToNextLevel(Level);
            OnLevelUp?.Invoke();
        }
    }

    /// <summary>
    /// 레벨이 오를수록 다음 레벨을 위한 경험치 총량을 늘리는 함수
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    private int CalculateExpToNextLevel(int level)
    {
        return 100 * level;
    }

    public uint AttackPower { get; }

    public void Damaged(float damage)
    {
        float finalDamage = damage * ((100 - defense) * 0.01f);
        
        currentHP -= finalDamage;
        Debug.Log($"currentHP : {currentHP}");
        OnStatsChanged?.Invoke();
        if (currentHP <= 0)
        {
            // 플레이어가 사망했음을 알리는 로직 추가
            Player player = GameManager.Instance.Player;
            player.PlayerDie();
        }
    }

    public void AddAccessory(ItemData_Accessory accessory)
    {
        accessories.Add(accessory);
    }

    public void AddWeapon(ItemData_Weapon weapon)
    {
        if (!weapons.Contains(weapon))
        {
            weapons.Add(weapon);
            RecalculateStats();
        }
    }

    public int GetAccessoryLevel(ItemData_Accessory accessory)
    {
        var item = accessories.Find(a => a == accessory);
        return item != null ? item.level : 0;
    }

    public int GetWeaponLevel(ItemData_Weapon weapon)
    {
        var item = weapons.Find(w => w == weapon);
        return item != null ? item.level : 0;
    }

    public void LevelUpAccessory(ItemData_Accessory accessory)
    {
        var item = accessories.Find(a => a == accessory);
        if (item != null && item.level < item.maxLevel)
        {
            item.level++;
            RecalculateStats();
            Debug.Log($"{item.name} has been leveled up to level {item.level}.");
        }
    }

    public void LevelUpWeapon(ItemData_Weapon weapon)
    {
        var item = weapons.Find(w => w == weapon);
        if (item != null && item.level < item.maxLevel)
        {
            item.level++;
            RecalculateStats();
            Debug.Log($"{item.name} has been leveled up to level {item.level}.");
        }
    }
}