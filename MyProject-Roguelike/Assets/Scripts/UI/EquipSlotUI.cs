using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlotUI : MonoBehaviour
{
    protected Image icon;

    protected EquipmentDataManager equipmentDataManager;

    protected virtual void Awake()
    {
        icon = GetComponent<Image>();
    }


}
