using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MultipleShooter : Entity
{
    [Header("Shooter")]
    public WeaponType[] weaponTypes;
    public WeaponType activeWeapon;
    public Transform firePoint;

    public void SwitchWeapons(int id)
    {
        activeWeapon = weaponTypes[id];
    }

    public void Shoot(Vector2 targetPos)
    {
        if (!activeWeapon.ready)
        { //Just for redundency
            return;
        }

        Projectile projectile = Instantiate(this.activeWeapon.projectilePrefab.gameObject, firePoint.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.Init(this.gameObject.layer, targetPos, activeWeapon.shootRange, activeWeapon.projectileSpeed, activeWeapon.damage);

        StartCoroutine(this.Reload(activeWeapon));
    }

    public bool isReadyToShoot()
    {
        return activeWeapon.ready;
    }

    private IEnumerator Reload(WeaponType weapon)
    {
        weapon.ready = false;
        yield return new WaitForSeconds(activeWeapon.shootDelay);
        weapon.ready = true;
    }
}
