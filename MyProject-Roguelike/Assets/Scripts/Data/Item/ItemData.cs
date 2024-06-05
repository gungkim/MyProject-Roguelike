using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Scriptable Object/Item Weapon Data", order = 1)]
public class ItemData : ScriptableObject
{
    [Header("������ �⺻ ����")]
    public ItemCode code;
    public ItemType type;
    public string itemName = "������";
    public string itemDescription = "����";
    public Sprite itemIcon;
}
