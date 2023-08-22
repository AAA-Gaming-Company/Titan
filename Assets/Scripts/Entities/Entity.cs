using UnityEngine;
using MoreMountains.Feedbacks;

public abstract class Entity : MonoBehaviour {
    [Header("Entity")]
    public int maxHP;

    public MMF_Player damageFeedback = null;
    public MMF_Player deathFeedback = null;

    private int currentHP;
    private bool dead = false;

    protected void Start() {
        this.currentHP = this.maxHP;
        this.EntityStart();
    }

    protected abstract void EntityStart();

    public void TakeDamage(int amount) {
        this.currentHP -= amount;
        if (this.damageFeedback != null) {
            this.damageFeedback.PlayFeedbacks();
        }

        if (this.currentHP <= 0) {
            this.Die();
        }
    }

    protected abstract void OnDie();

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
