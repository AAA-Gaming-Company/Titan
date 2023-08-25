using UnityEngine;

public class FishProjectile : Projectile {
    public override void HitFunction(GameObject hit) {
        MegaMech megaMech = hit.GetComponent<MegaMech>();
        megaMech.Stun();

        if (megaMech != null) {
            BoidGroup boidGroup = BoidGroup.GetGroup();
            if (boidGroup != null) {
                boidGroup.StartCoroutine(boidGroup.TemporaryTargetChange(6f, hit.transform));
            }
        }
    }
}
