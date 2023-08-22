using UnityEngine;
using MoreMountains.Feedbacks;

public abstract class Entity : MonoBehaviour {
    [Header("Entity")]
    public int maxHP;

    public MMF_Player damageFeedback;
    public MMF_Player deathFeedback;

    //TODO: Should be made private once testing is finished
    public int currentHP;

    protected void Start() {
        this.currentHP = this.maxHP;
        this.EntityStart();
    }

    protected abstract void EntityStart();

    public void TakeDamage(int amount) {
        this.currentHP -= amount;
        this.damageFeedback.PlayFeedbacks();

        if (this.currentHP <= 0) {
            this.Die();
        }
    }

    public void Die() {
        this.deathFeedback.transform.parent = null;
        this.deathFeedback.PlayFeedbacks();
        Destroy(this.gameObject);
    }
}
