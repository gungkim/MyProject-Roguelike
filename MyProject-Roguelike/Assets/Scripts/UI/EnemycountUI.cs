using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemycountUI : MonoBehaviour
{
    TextMeshProUGUI enemyCount;

    private void Awake()
    {
        enemyCount = GetComponent<TextMeshProUGUI>();
    }
}
