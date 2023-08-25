using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishProjectile : Projectile
{
    public  override void HitFunction(GameObject hit)
    {
        if (hit.GetComponent<MegaMech>() == true)
        {
            BoidGroup boidGroup = BoidGroup.GetGroup();
                boidGroup.StartCoroutine(boidGroup.TemporaryTargetChange(6f, hit.transform));
        }

    }
}
