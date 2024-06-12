using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemCode
{
    Coin =0,
    CoinPocket,

    ShortSword,
    HolyWater,


    SpeedShoes,


    HealPotion,
    

    Bomb
}
public enum ItemType
{
    Weapon,
    Accessory,
    DropItem_Heal,
    DropItem_Active,
    DropItem_Money
}
public enum ItemSortBy
{
    Code,
    Icon,
    Name
}