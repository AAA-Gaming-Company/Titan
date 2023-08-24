using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashProjectile : Projectile {
    public float range;
    public LayerMask layerMask;

    public override void HitFunction() {
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, range, layerMask);

        foreach (Collider2D collider in hit) {
            collider.GetComponent<Entity>().TakeDamage(base.damageDealt);
        }
    }
}
