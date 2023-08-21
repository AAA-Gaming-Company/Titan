using UnityEngine;

public abstract class Entity : MonoBehaviour {
    [Header("Entity")]
    public int maxHP;

    private int currentHP;

    protected void Start() {
        this.currentHP = this.maxHP;
        this.EntityStart();
    }

    protected abstract void EntityStart();

    public void TakeDamage(int amount) {
        this.currentHP -= amount;
        if (this.currentHP <= 0) {
            this.Die();
        }
    }

    public void Die() {
        Destroy(this.gameObject);
    }
}
