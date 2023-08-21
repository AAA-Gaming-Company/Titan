using UnityEngine;

public class Projectile : MonoBehaviour {
    private Vector2 finalPosition;
    private float maxSpeed;
    private int damageDealt;
    public ContactFilter2D filter2D;
    public bool player;

    public void Init(Vector2 targetPos, float range, float speed, int damage) {
        //Normalize the target position to extract only the direction, then
        // multiply by the bullet's range for the distance. Finally, add the
        // start position so that it doesn't try going to points around (0, 0).
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
