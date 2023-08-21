using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIDestinationSetter))]
[RequireComponent(typeof(AIPath))]

public class EnemyController : ShootingEntity {
    private AIDestinationSetter destinationSetter;
    private AIPath aiPath;
    public float moveRange;
    public LayerMask obstacle;

    protected override void EntityStart() {
        destinationSetter = GetComponent<AIDestinationSetter>();
        aiPath = GetComponent<AIPath>();
    }

    private void Update() {
        if (this.isReadyToShoot() && Vector2.Distance(base.transform.position, this.destinationSetter.target.position) < this.shootRange && CanHitPlayer()) {
            this.Shoot(this.destinationSetter.target.position);
        }

        if (Vector2.Distance(base.transform.position, this.destinationSetter.target.position) < this.moveRange) {
            this.aiPath.canMove = false;
        } else {
            this.aiPath.canMove = true;
        }
    }

    private bool CanHitPlayer()
    {
        // Alex help, it's supposed to check whether the player is behind a wall.
        return !Physics2D.Raycast(transform.position, -destinationSetter.target.position, Vector2.Distance(base.transform.position, this.destinationSetter.target.position), obstacle);
    }

    public void OnDrawGizmos() {
        Gizmos.DrawWireSphere(base.transform.position, this.moveRange);
        Gizmos.DrawRay(transform.position, -destinationSetter.target.position);
    }
}
