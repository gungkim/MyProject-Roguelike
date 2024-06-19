using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Data", menuName = "Scriptable Object/Projectile Data", order = 0)]
public class ProjectileData : ScriptableObject
{
    public string projectileName;
    public float speed;
    public int damage;
    public Sprite sprite;
}
