using System.Collections;
using UnityEngine;

public class BoidTriggerArea : TriggerArea {
    [Header("Boids")]
    public BoidGroup oldGroup;
    public BoidGroup newGroup;

    private void Awake() {
        this.destroyOnTrigger = false;
    }

    protected override void TriggerAction() {
        bool coroutine = false;
        if (this.oldGroup != null) {
            StartCoroutine(this.OldMoveAway());
            coroutine = true;
        }

        if (this.newGroup != null) {
            this.newGroup.Spawn();
        }

        if (!coroutine) {
            Destroy(this.gameObject);
        }
    }

    private IEnumerator OldMoveAway() {
        this.oldGroup.UpdateTarget(this.oldGroup.transform);
        yield return new WaitForSeconds(5);
        this.oldGroup.Kill();

        Destroy(this.gameObject);
    }
}
