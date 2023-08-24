using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MissileShooter : MonoBehaviour
{
    public AIDestinationSetter projectilePrefab;
    public float shootDelay;
    public float projectileSpeed = 2f;
    public int amount = 4;
    public float shootRange;
    public Transform firePoint;

    private bool ready = true;
    private AIDestinationSetter destinationSetter;

    public void Start()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
    }

    public void Update()
    {
        if (ready)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (!this.ready)
        { //Just for redundency
            return;
        }

        for (int i = 0; i < amount; i++)
        {
            AIDestinationSetter projectile = Instantiate(this.projectilePrefab.gameObject, this.firePoint.position, Quaternion.identity).GetComponent<AIDestinationSetter>();
            projectile.target = destinationSetter.target;
            destinationSetter.GetComponent<AIPath>().maxSpeed = projectileSpeed;
            projectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-500f, 500f), Random.Range(-500f, 500f)));
        }

        StartCoroutine(this.Reload());
    }

    public bool isReadyToShoot()
    {
        return this.ready;
    }

    private IEnumerator Reload()
    {
        this.ready = false;
        yield return new WaitForSeconds(this.shootDelay);
        this.ready = true;
    }
}
