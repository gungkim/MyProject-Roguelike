using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBarUI : MonoBehaviour
{
    Slider slider;
    PlayerStat playerStat;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        playerStat = FindObjectOfType<PlayerStat>();
    }

    private void Start()
    {
        if (playerStat != null)
        {
            playerStat.OnStatsChanged += UpdateExpBar;
            playerStat.OnLevelUp += UpdateExpBar; // 레벨업 시에도 업데이트
            UpdateExpBar();
        }
    }

    private void UpdateExpBar()
    {
        if (playerStat != null)
        {
            slider.maxValue = playerStat.XPToNextLevel;
            slider.value = playerStat.CurrentEXP;
        }
    }

}
