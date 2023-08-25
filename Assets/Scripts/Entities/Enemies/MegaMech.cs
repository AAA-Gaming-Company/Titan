using System.Collections;
using UnityEngine;

public class MegaMech : EnemyController {
    [Header("Mega Mech")]
    public float shieldRadius;
    public int shieldCooldown;

    public new void Start() {
        base.Start();

        GameObject shield = new GameObject();
        shield.name = "Shield";
        shield.transform.parent = this.transform;
        shield.transform.localPosition = Vector3.zero;
        shield.transform.localScale = Vector3.one;

        CircleCollider2D collier = shield.AddComponent<CircleCollider2D>();
        collier.isTrigger = true;
        collier.radius = this.shieldRadius;

        MegaMechCollider checker = shield.AddComponent<MegaMechCollider>();
        checker.megaMech = this;
    }

    public void DeployShield() {
        if (this.immune) {
            return;
        }
        this.immune = true;

        StartCoroutine(this.ShieldCooldown());

        Debug.Log("Deployed");
    }

    public void RetractShield() {
        if (!this.immune) {
            return;
        }
        this.immune = false;

        Debug.Log("Retracted");
    }

    private IEnumerator ShieldCooldown() {
        yield return new WaitForSeconds(this.shieldCooldown);
        this.RetractShield();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, this.shieldRadius);
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
