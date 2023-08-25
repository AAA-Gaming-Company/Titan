using UnityEngine;

public class FishProjectile : Projectile {
    public override void HitFunction(GameObject hit) {
        if (hit.GetComponent<MegaMech>() == true) {
            BoidGroup boidGroup = BoidGroup.GetGroup();
            if (boidGroup == null) {
                return;
            }

            boidGroup.StartCoroutine(boidGroup.TemporaryTargetChange(6f, hit.transform));
        }
    }
}
