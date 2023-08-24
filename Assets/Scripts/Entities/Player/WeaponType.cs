using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class WeaponType : ScriptableObject
{
    public Projectile projectilePrefab;
    public float shootDelay;
    public float projectileSpeed = 2f;
    public float shootRange;
    public int damage;
    public bool ready = true;
}
