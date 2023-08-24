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

        StartCoroutine(this.Reload(this.weapon));

        return projectile;
    }

    public bool isReadyToShoot() {
        return this.weapon.ready;
    }

    public float GetShootRange() {
        return this.weapon.shootRange;
    }

    private IEnumerator Reload(WeaponType weapon) {
        weapon.ready = false;
        yield return new WaitForSeconds(this.weapon.shootDelay);
        weapon.ready = true;
    }
}
