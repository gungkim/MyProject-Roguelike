using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    protected Rigidbody2D rigidbody2d;


    protected virtual void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {

    }
}
