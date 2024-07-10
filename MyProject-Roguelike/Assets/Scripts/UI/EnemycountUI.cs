using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemycountUI : MonoBehaviour
{
    TextMeshProUGUI enemyCountText;
    private int enemyCount = 0;


    private void Awake()
    {
        enemyCountText = GetComponent<TextMeshProUGUI>();
    }

    public void CountingEnemy()
    {
        enemyCount++;
        UpdateCounting();
    }

    private void UpdateCounting()
    {
        enemyCountText.text = $"{enemyCount}"; 
    }
}
