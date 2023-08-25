using System.Collections;
using UnityEngine;

public class MegaMech : EnemyController {
    [Header("Mega Mech")]
    public float shieldRadius;
    public int shieldCooldown;
    public Sprite shield;

    private SpriteRenderer shieldRenderer;

    public new void Start() {
        base.Start();

        GameObject shield = new GameObject();
        shield.name = "Shield";
        shield.transform.parent = this.transform;
        shield.transform.localPosition = Vector3.zero;
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
        if (this.immune) {
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
        yield return new WaitForSeconds(this.shieldCooldown);
        this.RetractShield();
    }

    [RequireComponent(typeof(Collider2D))]
    public class MegaMechCollider : MonoBehaviour {
        public MegaMech megaMech; //If this were java, I would use no accessor, but it isn't

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.tag == "PlayerProjectile") {
                this.megaMech.DeployShield();
            }
        }
    }
}
