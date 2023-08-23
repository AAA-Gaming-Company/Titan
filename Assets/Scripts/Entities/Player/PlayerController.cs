using UnityEngine;
using MoreMountains.Feedbacks;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : Shooter {
    [Header("Player")]
    public float playerSpeed;
    public GameObject spotLight;
    public MMF_Player moveFeedback;
    public MMF_Player shootFeedback;
    public SpriteRenderer window;

    private Rigidbody2D rb;

    private float inputX = 0;
    private float inputY = 0;
    private float hpIncrement = 0;
    private bool isPlayingMove = false;
    private Camera cam;

    protected override void EntityStart() {
        this.rb = GetComponent<Rigidbody2D>();
        this.cam = Camera.main;

        float h, s, v;
        Color.RGBToHSV(this.window.color, out h, out s, out v);
        this.hpIncrement = v / base.maxHP ;
    }

    private void FixedUpdate() {
        this.inputX = Input.GetAxis("Horizontal");
        this.inputY = Input.GetAxis("Vertical");

        this.rb.AddForce(new Vector2(this.inputX * this.playerSpeed, this.inputY * this.playerSpeed));

        if (Input.GetMouseButton(0)) {
            this.Hit();

            //Probably should also rotate the player if they click in a
            // different direction to that in which they are going.
        }
    }

    private void Update() {
        this.UpdatePlayerSprite();

        //Play and pause engine depending on if the player is moving.
        //We use the absolute value of the velocity because velocity could be negative.
        if (Mathf.Abs(this.rb.velocity.x) > 0.1f || Mathf.Abs(this.rb.velocity.y) > 0.1f) {
            if (!this.isPlayingMove) {
                this.moveFeedback.PlayFeedbacks();
                this.isPlayingMove = true;
            }
        } else {
            if (this.isPlayingMove) {
                this.moveFeedback.StopFeedbacks();
                this.isPlayingMove = false;
            }
        }
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
        if (base.isReadyToShoot()) {
            this.shootFeedback.PlayFeedbacks();
            this.Shoot(this.cam.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    protected override void OnDie() { //Ignore
    }

    protected override void OnDamage(int amount) {
        float h, s, v;
        Color.RGBToHSV(this.window.color, out h, out s, out v);
        v -= hpIncrement * amount;

        this.window.color = Color.HSVToRGB(h, s, v);
    }
}
