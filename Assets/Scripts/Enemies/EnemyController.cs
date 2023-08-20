using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIDestinationSetter))]
[RequireComponent(typeof(AIPath))]
public class EnemyController : ShootingEntity{
    private AIDestinationSetter destinationSetter;
    private AIPath aiPath;
    public float moveRange; 


    private void Start() {
        base.Start();
        destinationSetter = GetComponent<AIDestinationSetter>();
        aiPath = GetComponent<AIPath>();
    }

    private void Update() {
        if (ready && Vector2.Distance(transform.position, destinationSetter.target.position) < shootRange) {
            this.Shoot(destinationSetter.target.position);
        }
        if (Vector2.Distance(transform.position, destinationSetter.target.position) < moveRange) {
            aiPath.canMove = false;
        }else {
            aiPath.canMove = true;
        }

    }

    public void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(transform.position, moveRange);
    }
}
