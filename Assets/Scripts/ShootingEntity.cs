using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class ShootingEntity : Entity {
    [Header("Shooter")]
    public Projectile projectilePrefab;
    public float shootDelay;
    public float projectileSpeed = 2f;
    public float shootRange;
    public Transform firePoint;
    public int damage;
    public bool ready = true;

    public void Shoot(Vector2 targetPos) {
        Projectile projectile = Instantiate(this.projectilePrefab.gameObject, this.firePoint.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.Init(targetPos, this.projectileSpeed, this.damage);
        StartCoroutine(this.Reload());
    }

    private IEnumerator Reload() {
        this.ready = false;
        yield return new WaitForSeconds(this.shootDelay);
        this.ready = true;
    }
    public void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, shootRange);
    }
}
