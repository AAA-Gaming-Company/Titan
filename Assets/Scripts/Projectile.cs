using UnityEngine;

public class Projectile : MonoBehaviour {
    private Vector2 finalPosition;
    private float maxSpeed;
    private int damageDealt;
    public ContactFilter2D filter2D;
    public bool player;

    public void Init(Vector2 targetPos, float range, float speed, int damage) {
        //Get the direction this bullet should go by getting the difference
        // between the start and end position. Then we normalize it to give it
        // a max amplitude of 1 which we then multiply by the range to get the
        // final position around (0, 0). This direction can be adapted to work
        // for us by adding the start position.
        Vector2 startPos = new Vector2(base.transform.position.x, base.transform.position.y);
        Vector2 direction = targetPos - startPos;
        this.finalPosition = (direction.normalized * range) + startPos;

        this.maxSpeed = speed;
        this.damageDealt = damage;
    }

    public void FixedUpdate() {
        //Move the projectile
        if (player) {
            base.transform.position = Vector3.MoveTowards(base.transform.position, this.finalPosition, this.maxSpeed);
            Collider2D[] hit = new Collider2D[3];
            base.gameObject.GetComponent<BoxCollider2D>().OverlapCollider(this.filter2D, hit);

            foreach (Collider2D c in hit) {
                if (c != null && c.GetComponent<Entity>() != null) {
                    if (c.gameObject.CompareTag("Enemy")) {
                        c.GetComponent<Entity>().TakeDamage(this.damageDealt);
                        Destroy(base.gameObject);
                    }
                }
            }
        } else {
            base.transform.position = Vector3.MoveTowards(base.transform.position, this.finalPosition, this.maxSpeed);
            Collider2D[] hit = new Collider2D[3];
            base.gameObject.GetComponent<BoxCollider2D>().OverlapCollider(this.filter2D, hit);

            foreach (Collider2D c in hit) {
                if (c != null && c.GetComponent<Entity>() != null) {
                    if (c.gameObject.CompareTag("Player")) {
                        c.GetComponent<Entity>().TakeDamage(this.damageDealt);
                        Destroy(base.gameObject);
                    }
                }
            }
        }

        //If we've got to the final position, then destroy
        if (base.transform.position.x == this.finalPosition.x && base.transform.position.y == this.finalPosition.y) {
            Destroy(base.gameObject);
        }
    }
}
