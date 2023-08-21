using UnityEngine;
using System.Collections;
using MoreMountains.Feedbacks;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerController : ShootingEntity {
    [Header("Player")]
    public float playerSpeed;
    public GameObject spotLight;
    public MMF_Player move;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private float inputX = 0;
    private float inputY = 0;

    private float previousInputX = 0;
    private float previousInputY = 0;

    private Camera cam;

    protected override void EntityStart() {
        this.rb = GetComponent<Rigidbody2D>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.cam = Camera.main;
    }

    private void FixedUpdate() {
        this.inputX = Input.GetAxis("Horizontal");
        this.inputY = Input.GetAxis("Vertical");

        this.rb.AddForce(new Vector2(this.inputX * this.playerSpeed, this.inputY * this.playerSpeed));
        if (this.inputX != 0 || this.inputY != 0)
        {
            if (previousInputX * 2 + previousInputY == 0)
            {
                move.PlayFeedbacks();
            }
        }else if (inputX * 2 + inputY == 0)
        {
            if (previousInputX * 2 != 0 || previousInputY != 0)
            {
                move.StopFeedbacks();
            }

        }

        this.previousInputX = inputX;
        this.previousInputY = inputY;

        if (Input.GetMouseButton(0)) {
            this.Hit();

            //Probably should also rotate the player if they click in a
            // different direction to that in which they are going.
        }
    }

    private void Update() {
        this.UpdatePlayerSprite();
    }

    private void UpdatePlayerSprite() {
        //Horizontal flip
        Vector3 localScale = base.transform.localScale;
        if (this.inputX > 0) {
            localScale.x = 1;
        } else if (this.inputX < 0) {
            localScale.x = -1;
        }
        this.transform.localScale = localScale;

        //Move the spot light slightly up or down when going in that direction
        float upDown = this.rb.velocity.y * (5 * localScale.x);
        this.spotLight.transform.rotation = Quaternion.Euler(0, 0, upDown - (90 * localScale.x));
    }

    private void Hit() {
        this.Shoot(this.cam.ScreenToWorldPoint(Input.mousePosition));
    }

}
