using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : ShootingEntity {
    [Header("Player")]
    public float playerSpeed;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private void Start() {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (x != 0 || y != 0) {
            //TODO: Add move sound
        }

        UpdatePlayerSprite(x, y);
        rb.AddForce(new Vector2(x * this.playerSpeed, y * this.playerSpeed));

        if (Input.GetMouseButtonDown(0)) {
            this.Hit();

            //Probably should also rotate the player if they click in a
            // different direction to that in which they are going.
        }
    }

    private void UpdatePlayerSprite(float x, float y) {
        //TODO: Update sprite
    }

    private void Hit() {
        //TODO: Add gunz
    }
}
