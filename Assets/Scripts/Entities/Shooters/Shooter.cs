using System.Collections;
using Pathfinding;
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

    public void Shoot(Vector2 targetPos) {
        if (!this.weapon.ready) {
            return;
        }

        GameObject newObject = null;
        Debug.Log(this.gameObject.name + " using " + this.weapon.name);
        if (this.weapon.isSpawner) {
            newObject = Instantiate(this.weapon.prefab.gameObject, this.firePoint.position, Quaternion.identity);
            Debug.Log("Spawned " + newObject.name);
        }

        if (this.weapon.isProjectile) {
            Projectile projectile = newObject.GetComponent<Projectile>();
            projectile.Init(this.gameObject.layer, targetPos, this.weapon.useRange, this.weapon.projectileSpeed, this.weapon.damage);
        }

        if (this.weapon.isPathfidner) {
            AIDestinationSetter path = newObject.GetComponent<AIDestinationSetter>();
            path.target = this.gameObject.GetComponent<AIDestinationSetter>().target;
        }

        StartCoroutine(this.Reload(this.weapon));
    }

    public bool isReadyToShoot() {
        return this.weapon.ready;
    }

    public float GetShootRange() {
        return this.weapon.useRange;
    }

    private IEnumerator Reload(WeaponType weapon) {
        weapon.ready = false;
        yield return new WaitForSeconds(weapon.useDelay);
        weapon.ready = true;
    }
}
