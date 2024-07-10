using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldCountUI : MonoBehaviour
{
    TextMeshProUGUI goldCount;



    private void Awake()
    {
        Transform child = transform.GetChild(0);
        goldCount = child.GetComponent<TextMeshProUGUI>();
    }

    private void goldCounting()
    {

    }
}
