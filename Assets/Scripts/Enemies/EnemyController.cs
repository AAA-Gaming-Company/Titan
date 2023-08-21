using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIDestinationSetter))]
[RequireComponent(typeof(AIPath))]

public class EnemyController : ShootingEntity {
    private AIDestinationSetter destinationSetter;
    private AIPath aiPath;
    public float moveRange;

    protected override void EntityStart() {
        destinationSetter = GetComponent<AIDestinationSetter>();
        aiPath = GetComponent<AIPath>();
    }

    private void Update() {
        if (this.isReadyToShoot() && Vector2.Distance(base.transform.position, this.destinationSetter.target.position) < this.shootRange) {
            this.Shoot(this.destinationSetter.target.position);
        }

        if (Vector2.Distance(base.transform.position, this.destinationSetter.target.position) < this.moveRange) {
            this.aiPath.canMove = false;
        } else {
            this.aiPath.canMove = true;
        }
    }

    public void OnDrawGizmos() {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(base.transform.position, this.moveRange);
    }
}
