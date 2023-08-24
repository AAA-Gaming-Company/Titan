using UnityEngine;
using MoreMountains.Feedbacks;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : Shooter {
    [Header("Player")]
    public float playerSpeed;
    public GameObject spotLight;
    public GameObject fishBomb;
    public MMF_Player moveFeedback;
    public MMF_Player shootFeedback;
    public MMF_Player loadFeedback;
    public SpriteRenderer window;

    private Rigidbody2D rb;

    private float inputX = 0;
    private float inputY = 0;
    private float hpIncrement = 0;
    private bool isPlayingMove = false;
    private Camera cam;
    private bool ramping = false;

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

            if (!this.ramping) {
                this.loadFeedback.PlayFeedbacks();
                this.ramping = true;
            }
            //Probably should also rotate the player if they click in a
            // different direction to that in which they are going.
        } else {
            this.ramping = false;
            this.loadFeedback.StopFeedbacks();
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

    public void FishBomb() {
        Projectile projectile = Instantiate(this.fishBomb.gameObject, base.firePoint.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.Init(this.gameObject.layer, this.cam.ScreenToWorldPoint(Input.mousePosition), base.shootRange * 1.5f, base.projectileSpeed / 2, base.damage * 2);
    }
    public void Heal(int amount)
    {
        currentHP += amount;
        Mathf.Clamp(currentHP, 0, maxHP);
        float h, s, v;
        Color.RGBToHSV(this.window.color, out h, out s, out v);
        v += hpIncrement * amount;

        this.window.color = Color.HSVToRGB(h, s, v);
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
