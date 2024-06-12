using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Scriptable Object/Item Active Data", order = 3)]
public class ItemData_Active : ItemData
{
    public uint maxStackCount = 1;
}
