using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUI : MonoBehaviour
{
    ItemData_Accessory ItemData_Accessory;
    ItemData_Weapon ItemData_Weapon;

    public GameObject levelUpPanel;  // 레벨업 UI 패널
    public List<Image> itemSlotImages;  // 아이템 슬롯 이미지 리스트
    public List<TextMeshProUGUI> itemSlotDescriptions;  // 아이템 설명 텍스트 리스트

    private Player player;
    private List<ItemData_Accessory> displayedAccessories = new List<ItemData_Accessory>();
    private List<ItemData_Weapon> displayedWeapons = new List<ItemData_Weapon>();
    private int selectedIndex = 0;  // 현재 선택된 슬롯 인덱스

    private void Awake()
    {
        player = GameManager.Instance.Player;
        levelUpPanel.SetActive(false);
    }

    private void Start()
    {
        
    }

    private void PickRandomItem(out ItemData_Accessory randomAccessory, out ItemData_Weapon randomWeapon)
    {
        randomAccessory = null;
        randomWeapon = null;

        //List<ItemData_Accessory> validAccessories = allAccessories.FindAll(item => !IsMaxLevel(item));
        //List<ItemData_Weapon> validWeapons = allWeapons.FindAll(item => !IsMaxLevel(item));

    }

    //private bool IsMaxLevel(ItemData_Accessory accessory)
    //{
    //    // Check if accessory is at max level
    //    return player.GetAccessoryLevel(accessory) >= accessory.maxLevel;
    //}

    //private bool IsMaxLevel(ItemData_Weapon weapon)
    //{
    //    // Check if weapon is at max level
    //    return player.GetWeaponLevel(weapon) >= weapon.maxLevel;
    //}

    public void DisplayLevelUpOptions(List<ItemData_Accessory> validAccessories, List<ItemData_Weapon> validWeapons)
    {
        levelUpPanel.SetActive(true);
        displayedAccessories = validAccessories;
        displayedWeapons = validWeapons;

        for (int i = 0; i < itemSlotImages.Count; i++)
        {
            if (i < validAccessories.Count)
            {
                itemSlotImages[i].sprite = validAccessories[i].itemIcon;
                itemSlotDescriptions[i].text = validAccessories[i].itemDescription;

                if (player.PlayerStat.GetAccessoryLevel(validAccessories[i]) > 0)
                {
                    itemSlotDescriptions[i].text += "\n" + "Level Up: " + validAccessories[i].levelUpDescription;
                }
            }
            else if (i < validAccessories.Count + validWeapons.Count)
            {
                int weaponIndex = i - validAccessories.Count;
                itemSlotImages[i].sprite = validWeapons[weaponIndex].itemIcon;
                itemSlotDescriptions[i].text = validWeapons[weaponIndex].itemDescription;

                if (player.PlayerStat.GetWeaponLevel(validWeapons[weaponIndex]) > 0)
                {
                    itemSlotDescriptions[i].text += "\n" + "Level Up: " + validWeapons[weaponIndex].levelUpDescription;
                }
            }
            else
            {
                itemSlotImages[i].gameObject.SetActive(false);
                itemSlotDescriptions[i].gameObject.SetActive(false);
            }
        }
    }

    public void OnItemSelected(int index)
    {
        if (index < displayedAccessories.Count)
        {
            ItemData_Accessory selectedAccessory = displayedAccessories[index];
            player.PlayerStat.AddAccessory(selectedAccessory);
            player.PlayerStat.LevelUpAccessory(selectedAccessory);
        }
        else
        {
            int weaponIndex = index - displayedAccessories.Count;
            ItemData_Weapon selectedWeapon = displayedWeapons[weaponIndex];
            player.PlayerStat.AddWeapon(selectedWeapon);
            player.PlayerStat.LevelUpWeapon(selectedWeapon);
        }

        levelUpPanel.SetActive(false);
    }
}
