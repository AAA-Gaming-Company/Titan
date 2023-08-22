using UnityEngine;
using MoreMountains.Feedbacks;

public abstract class Entity : MonoBehaviour {
    [Header("Entity")]
    public int maxHP;

    public int currentHP;

    public MMF_Player damageFeedback;
    public MMF_Player deathFeedback;

    protected void Start() {
        this.currentHP = this.maxHP;
        this.EntityStart();
    }

    protected abstract void EntityStart();

    public void TakeDamage(int amount) {
        this.currentHP -= amount;
        damageFeedback.PlayFeedbacks();
        if (this.currentHP <= 0) {
            this.Die();
        }
    }

    public void Die() {
        deathFeedback.transform.parent = null;
        deathFeedback.PlayFeedbacks();
        Destroy(this.gameObject);
    }
}
