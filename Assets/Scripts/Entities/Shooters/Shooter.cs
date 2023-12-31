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

    public bool Shoot(Vector2 targetPos , WeaponType weapon) {
        if (weapon == null) {
            weapon = this.weapon;
        }
        if (!weapon.ready) {
            return false;
        }

        if (weapon.isSpawner) {
            int amount = weapon.amount[0];
            if (weapon.amount.Length > 1) {
                amount = weapon.amount[GameManager.difficultyLevel];
            }


            int damage = 0;
            if (weapon.isProjectile) {
                if (weapon.damage.Length > 1) {
                    damage = weapon.damage[GameManager.difficultyLevel];
                } else {
                    damage = weapon.damage[0];
                }
            }

            for (int i = 0; i < amount; i++) {
                GameObject newObject = Instantiate(weapon.prefab.gameObject, this.firePoint.position, Quaternion.identity);

                if (weapon.isProjectile) {
                    Projectile projectile = newObject.GetComponent<Projectile>();
                    projectile.Init(this.gameObject.layer, targetPos, weapon.useRange, weapon.projectileSpeed, damage);
                }

                if (weapon.isPathfidner) {
                    AIDestinationSetter path = newObject.GetComponent<AIDestinationSetter>();
                    path.target = this.gameObject.GetComponent<AIDestinationSetter>().target;
                }
            }
        } else {
            if (weapon.isPathfidner || weapon.isProjectile) {
                throw new UnityException("Unsupported combination of weapon parameters.");
            }
        }

        StartCoroutine(this.Reload(weapon));
        return true;
    }

    public void Shoot(Vector2 targetPos) {
        this.Shoot(targetPos, this.weapon);
    }

    public bool isReadyToShoot() {
        return this.weapon.ready;
    }

    public float GetShootRange() {
        return this.weapon.useRange;
    }

    private IEnumerator Reload(WeaponType weapon) {
        weapon.ready = false;

        float delay = weapon.useDelay[0];
        if (weapon.useDelay.Length > 1) {
            delay = weapon.useDelay[GameManager.difficultyLevel];
        }

        yield return new WaitForSeconds(delay);
        weapon.ready = true;
    }
}
