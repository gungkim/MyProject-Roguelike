using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemCode
{
    Coin =0,
    CoinPocket,


    HealPotion,



    Bomb,



    SpeedShoes,



    ShortSword,
    HolyWater,
    FireBall,
    IceShot,
}
public enum ItemType
{
    DropItem_Money,
    DropItem_Heal,
    DropItem_Active,
    Accessory,
    Weapon,
}
public enum ItemSortBy
{
    Code,
    Icon,
    Name
}