using UnityEngine;

public class Projectile : MonoBehaviour {
    private Vector2 pos;
    private float maxSpeed;
    private int damageDealt;
    public ContactFilter2D filter2D;

    public void Init(Vector2 targetPos, float speed, int damage) {
        this.pos = targetPos;
        this.maxSpeed = speed;
        this.damageDealt = damage;
    }

    public void FixedUpdate() {
        //Move the projectile
        this.transform.position = Vector3.MoveTowards(this.transform.position, this.pos, this.maxSpeed);
        Collider2D[] hit = new Collider2D[3];
        this.gameObject.GetComponent<BoxCollider2D>().OverlapCollider(filter2D, hit);

        foreach (Collider2D c in hit) {
            if (c != null && c.GetComponent<Entity>() != null) {
                if (c.gameObject.CompareTag("Player")) {
                    c.GetComponent<Entity>().TakeDamage(damageDealt);
                    Destroy(this.gameObject);
                }
            }
        }

        if (this.transform.position.x == pos.x && this.transform.position.y == this.pos.y) {
            Destroy(this.gameObject);
        }
    }
}
