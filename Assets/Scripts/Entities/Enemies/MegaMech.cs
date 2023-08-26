using System.Collections;
using UnityEngine;

public class MegaMech : EnemyController {
    [Header("Mega Mech")]
    public float shieldRadius;
    public int shieldDuration;
    public float stunDuration;
    public Sprite shield;
    public VoiceCommand deathLine;

    private SpriteRenderer shieldRenderer;
    private bool stunned = false;

    public new void Start() {
        base.Start();

        GameObject shield = new GameObject();
        shield.name = "Shield";
        shield.transform.parent = this.transform;
        shield.transform.localPosition = new Vector3(0, 0, -2);
        shield.transform.localScale = Vector3.one;

        CircleCollider2D collier = shield.AddComponent<CircleCollider2D>();
        collier.isTrigger = true;
        collier.radius = this.shieldRadius / 2f;

        MegaMechCollider checker = shield.AddComponent<MegaMechCollider>();
        checker.megaMech = this;

        SpriteRenderer spriteRenderer = shield.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = null;
        this.shieldRenderer = spriteRenderer;
    }

    public void DeployShield() {
        if (this.immune || this.stunned) {
            return;
        }

        this.immune = true;
        this.shieldRenderer.sprite = this.shield;

        StartCoroutine(this.ShieldCooldown());
    }

    public void RetractShield() {
        if (!this.immune) {
            return;
        }

        this.immune = false;
        this.shieldRenderer.sprite = null;
    }

    private IEnumerator ShieldCooldown() {
        yield return new WaitForSeconds(this.shieldDuration);
        this.RetractShield();
    }

    public void Stun() {
        if (this.stunned) {
            return;
        }

        this.stunned = true;
        this.RetractShield();
        StartCoroutine(this.StunCooldown());
    }

    private IEnumerator StunCooldown() {
        yield return new WaitForSeconds(this.stunDuration);
        this.stunned = false;
    }

    protected new void OnDamage(int amount) {
        base.OnDamage(amount); //In this case I think it's redundant, but whatever
    }
    protected new void OnDie()
    {
        base.OnDie(); //In this case I think it's redundant, but whatever
        if (deathLine != null)
        {
            VoiceManager.Instance.SendCommand(deathLine);
        }
    }

    [RequireComponent(typeof(Collider2D))]
    public class MegaMechCollider : MonoBehaviour {
        public MegaMech megaMech; //If this were java, I would use no accessor, but it isn't

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.tag != "PlayerProjectile") {
                return;
            }

            this.megaMech.DeployShield();
            if (this.megaMech.immune) {
                Destroy(collision.gameObject);
            }
        }
    }
}
