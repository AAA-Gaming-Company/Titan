using UnityEngine;

public class MegaMech : EnemyController {
    [Header("Mega Mech")]
    public float shieldRadius;

    public new void Start() {
        base.Start();

        GameObject shield = new GameObject();
        shield.transform.parent = this.transform;

        CircleCollider2D collier = shield.AddComponent<CircleCollider2D>();
        collier.isTrigger = true;
        collier.radius = this.shieldRadius;

        MegaMechCollider checker = shield.AddComponent<MegaMechCollider>();
    }

    public void DeployShield() {
        
    }

    [RequireComponent(typeof(Collider2D))]
    public class MegaMechCollider : MonoBehaviour {
        private void OnTriggerEnter2D(Collider2D collision) {
            
        }
    }
}
