using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    private Image icon;

    private void Awake()
    {
        icon = GetComponent<Image>();
    }
}
