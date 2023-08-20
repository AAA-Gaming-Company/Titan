using UnityEngine;

public class Entity : MonoBehaviour {
    [Header("Entity")]
    public int maxHP;

    private int currentHP;

    protected void Start() {
        this.currentHP = this.maxHP;
    }

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
