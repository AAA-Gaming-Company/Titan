using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerController : ShootingEntity {
    [Header("Player")]
    public float playerSpeed;
    public GameObject spotLight;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    protected override void EntityStart() {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        this.rb.AddForce(new Vector2(x * this.playerSpeed, y * this.playerSpeed));
        if (x != 0 || y != 0) {
            //TODO: Add move sound
            UpdatePlayerSprite(x, y);
        }

        if (Input.GetMouseButtonDown(0)) {
            this.Hit();

            //Probably should also rotate the player if they click in a
            // different direction to that in which they are going.
        }
    }

    private void UpdatePlayerSprite(float x, float y) {
        //Horizontal flip
        Vector3 localScale = this.transform.localScale;
        if (x > 0) {
            localScale.x = 1;
        } else if (x < 0) {
            localScale.x = -1;
        }
        this.transform.localScale = localScale;

        //TODO: FIX
        Vector3 lookDir = Vector3.zero;
        lookDir.y = this.rb.velocity.normalized.y;
        this.spotLight.transform.rotation.SetFromToRotation(Vector3.zero, lookDir);
    }

    private void Hit() {
        //TODO: Add gunz
    }
}
