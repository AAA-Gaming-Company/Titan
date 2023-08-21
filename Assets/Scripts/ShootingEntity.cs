using System.Collections;
using UnityEngine;

public abstract class ShootingEntity : Entity {
    [Header("Shooter")]
    public Projectile projectilePrefab;
    public float shootDelay;
    public float projectileSpeed = 2f;
    public float shootRange;
    public Transform firePoint;
    public int damage;

    private bool ready = true;

    public void Shoot(Vector2 targetPos) {
        if (!this.ready) { //Just for redundency
            return;
        }

        Projectile projectile = Instantiate(this.projectilePrefab.gameObject, this.firePoint.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.Init(targetPos, this.shootRange, this.projectileSpeed, this.damage);
        StartCoroutine(this.Reload());
    }

    public bool isReadyToShoot() {
        return this.ready;
    }

    private IEnumerator Reload() {
        this.ready = false;
        yield return new WaitForSeconds(this.shootDelay);
        this.ready = true;
    }

    protected void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, shootRange);
    }
}
