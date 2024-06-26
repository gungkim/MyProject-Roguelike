using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    Player player;
    public Player Player
    {
        get
        {
            if (player == null)
                player = FindAnyObjectByType<Player>();
            return player;
        }
    }

    ItemDataManager equipmentDataManager;

    protected override void OnInitialize()
    {
        player = FindAnyObjectByType<Player>();
    }
}
