using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashProjectile : Projectile {
    public float range;
    public LayerMask layerMask;

    public override void HitFunction(GameObject hit) {
        Collider2D[] hitCollider = Physics2D.OverlapCircleAll(transform.position, range, layerMask);

        foreach (Collider2D collider in hitCollider) {
            collider.GetComponent<Entity>().TakeDamage(base.damageDealt);
        }
    }
}
