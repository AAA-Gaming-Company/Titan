using System.Collections;
using UnityEngine;

public abstract class Shooter : Entity {
    [Header("Shooter")]
    public WeaponType weapon;
    public Transform firePoint;

    public new void Start() {
        base.Start();

        WeaponType genericInstance = this.weapon;
        this.weapon = Instantiate(genericInstance);
        this.weapon.ready = true;
    }

    public Projectile Shoot(Vector2 targetPos) {
        if (!this.weapon.ready) {
            return null;
        }

        Projectile projectile = Instantiate(this.weapon.prefab.gameObject, this.firePoint.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.Init(this.gameObject.layer, targetPos, this.weapon.shootRange, this.weapon.projectileSpeed, this.weapon.damage);

        StartCoroutine(this.Reload());

        return projectile;
    }

    public bool isReadyToShoot() {
        return this.weapon.ready;
    }

    public float GetShootRange() {
        return this.weapon.shootRange;
    }

    private IEnumerator Reload() {
        this.weapon.ready = false;
        yield return new WaitForSeconds(this.weapon.shootDelay);
        this.weapon.ready = true;
    }
}
