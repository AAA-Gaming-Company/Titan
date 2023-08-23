using UnityEngine;

public class BoidTriggerArea : TriggerArea {
    [Header("Boids")]
    public BoidGroup oldGroup;
    public BoidGroup newGroup;

    protected override void TriggerAction() {
        if (this.oldGroup != null) {
            this.oldGroup.Kill(); //Maybe move them up a bit, but then kill
        }

        if (this.newGroup != null) {
            this.newGroup.Spawn();
        }
    }
}
