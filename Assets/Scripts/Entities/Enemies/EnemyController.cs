using UnityEngine;
using Pathfinding;
using MoreMountains.Feedbacks;

[RequireComponent(typeof(AIDestinationSetter))]
[RequireComponent(typeof(AIPath))]

public class EnemyController : Shooter {
    [Header("Enemy")]
    public float moveRange;
    public LayerMask playerLayer;
    public MMF_Player shoot;

    private AIDestinationSetter destinationSetter;
    private AIPath aiPath;

    protected override void EntityStart() {
        destinationSetter = GetComponent<AIDestinationSetter>();
        aiPath = GetComponent<AIPath>();
    }

    private void Update() {
        if (this.isReadyToShoot() && Vector2.Distance(base.transform.position, this.destinationSetter.target.position) < this.shootRange && CanHitPlayer()) {
            this.Shoot(this.destinationSetter.target.position);
            shoot.PlayFeedbacks();
        }

        if (Vector2.Distance(base.transform.position, this.destinationSetter.target.position) < this.moveRange) {
            this.aiPath.canMove = false;
        } else {
            this.aiPath.canMove = true;
        }
    }

    //TODO: Maybe decrease the amount of times this is actually called, it's expensive.
    private bool CanHitPlayer() {
        Vector3 direction = this.destinationSetter.target.position - base.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(base.transform.position, direction, Vector2.Distance(base.transform.position, this.destinationSetter.target.position));
        return hit.collider.gameObject.CompareTag("Player");
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(base.transform.position, this.moveRange);

        //Remove this code when fixed
        if (this.destinationSetter != null) {
            Vector3 direction = this.destinationSetter.target.position - base.transform.position;
            Gizmos.DrawRay(base.transform.position, direction);
        }
    }

    protected override void OnDie() { //Ignore
    }

    protected override void OnDamage(int amount) {
    }
}
