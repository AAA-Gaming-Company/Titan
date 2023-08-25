using UnityEngine;
using MoreMountains.Feedbacks;

public abstract class Entity : MonoBehaviour {
    [Header("Entity")]
    public int maxHP;

    public MMF_Player damageFeedback = null;
    public MMF_Player deathFeedback = null;

    public int currentHP;
    private bool dead = false;
    protected bool immune = false; //Currently only used for shielding, reserved for other use too

    public void Start() {
        this.currentHP = this.maxHP;
    }

    public void TakeDamage(int amount) {
        if (this.immune) {
            return;
        }

        this.OnDamage(amount);

        this.currentHP -= amount;
        if (this.damageFeedback != null) {
            this.damageFeedback.PlayFeedbacks();
        }

        if (this.currentHP <= 0) {
            this.Die();
        }
    }

    protected abstract void OnDie();
    protected abstract void OnDamage(int amount);

    public void Die() {
        this.OnDie();

        if (this.deathFeedback != null) {
            this.deathFeedback.transform.parent = null;
            this.deathFeedback.PlayFeedbacks();
        }

        this.dead = true;
        Destroy(this.gameObject);
    }

    public bool isDead() {
        return this.dead;
    }
}
