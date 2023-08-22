using UnityEngine;

public abstract class TriggerArea : MonoBehaviour {
    public bool destroyOnTrigger;

    protected abstract void TriggerAction();

    public void OnTriggerEnter2D(Collider2D collision) {
        this.TriggerAction();

        if (this.destroyOnTrigger) {
            Destroy(this.gameObject);
        }
    }
}
