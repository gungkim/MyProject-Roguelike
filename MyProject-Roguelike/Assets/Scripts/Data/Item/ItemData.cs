using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Scriptable Object/Item Weapon Data", order = 1)]
public class ItemData : ScriptableObject
{
    [Header("아이템 기본 정보")]
    public ItemCode code;
    public ItemType type;
    public string itemName = "아이템";
    public string itemDescription = "설명";
    public Sprite itemIcon;
}
