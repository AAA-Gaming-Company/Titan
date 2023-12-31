using UnityEngine;

public abstract class Projectile : MonoBehaviour {
    private bool ready = false;
    private static int wallLayer = -1;

    private Vector2 finalPosition;
    private float maxSpeed;
    private int ignoreLayer;

    public int damageDealt;
    public ContactFilter2D filter2D;

    private static void FindWallLayer() {
        if (Projectile.wallLayer == -1) {
            Projectile.wallLayer = LayerMask.NameToLayer("Walls");
            if (Projectile.wallLayer == -1) {
                throw new UnityException("No layer found for the walls!");
            }
        }
    }

    public void Init(int ignoreLayer, Vector2 targetPos, float range, float speed, int damage) {
        this.ignoreLayer = ignoreLayer;

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

        //Call last
        this.ready = true;
        Projectile.FindWallLayer();
    }

    private void FixedUpdate() {
        if (!this.ready) {
            return;
        }

        //Move the projectile
        base.transform.position = Vector3.MoveTowards(base.transform.position, this.finalPosition, this.maxSpeed);
        Collider2D[] hit = new Collider2D[3];
        base.gameObject.GetComponent<BoxCollider2D>().OverlapCollider(this.filter2D, hit);

        foreach (Collider2D c in hit) {
            if (c != null) {
                if (c.gameObject.layer != this.ignoreLayer) {
                    Entity entity = c.GetComponent<Entity>();
                    if (entity != null) {
                        this.HitFunction(c.gameObject);
                        entity.TakeDamage(this.damageDealt);
                        Destroy(base.gameObject);
                        return;
                    }
                } else if (c.gameObject.layer == Projectile.wallLayer) {
                    this.HitFunction(null);
                    Destroy(base.gameObject);
                }
            }
        }

        //If we've got to the final position, then destroy
        if (base.transform.position.x == this.finalPosition.x && base.transform.position.y == this.finalPosition.y) {
            Destroy(base.gameObject);
        }
    }

    public abstract void HitFunction(GameObject hit);
}
