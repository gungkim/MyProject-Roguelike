using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{
    Slider slider;
    public GameObject player;
    private RectTransform canvasRect;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        canvasRect = GetComponent<RectTransform>();
    }

    private void Start()
    {
        PlayerStat playerStat = player.GetComponent<PlayerStat>();
        playerStat.OnStatsChanged += UpdateCurrentHP;
        UpdateCurrentHP();
    }

    private void LateUpdate()
    {
        if(player != null)
        {
            Vector2 offset = new Vector2(0, (float)-1.2);
            canvasRect.position = (Vector2)player.transform.position + offset;
        }
    }

    private void UpdateCurrentHP()
    {
        PlayerStat playerStat = player.GetComponent<PlayerStat>();
        if (playerStat != null)
        {
            slider.maxValue = playerStat.MaxHP;
            slider.value = playerStat.CurrentHP;
        }
    }
}
