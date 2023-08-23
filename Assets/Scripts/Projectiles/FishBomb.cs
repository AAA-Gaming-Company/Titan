using UnityEngine;

public class FishBomb : Projectile {
    public GameObject boid;
    
    public override void HitFunction() {
        for (int i = 0; i < 5; i++) {
            GameObject fishy = Instantiate(boid, base.transform.position, Quaternion.identity);
            fishy.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f)));
            fishy.transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
        }
    }
}
