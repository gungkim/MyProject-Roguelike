using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Scriptable Object/Item Heal Data", order = 2)]
public class ItemData_Heal : ItemData
{
    public uint maxStackCount = 1;
}
