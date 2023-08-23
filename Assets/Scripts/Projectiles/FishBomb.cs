using UnityEngine;

public class FishBomb : Projectile {
    public GameObject boid;
    
    public override void HitFunction() {
        for (int i = 0; i < Random.Range(4, 25); i++) {
            GameObject fishy = Instantiate(boid, base.transform.position, Quaternion.identity);
            fishy.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-500f, 500f), Random.Range(-500f, 500f)));
            fishy.transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
        }
    }
}
