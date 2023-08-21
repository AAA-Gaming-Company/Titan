using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]

public class Boid : MonoBehaviour {
    private BoidSettings settings;
    private Transform target;

    [HideInInspector]
    public Vector2 position;
    [HideInInspector]
    public Vector2 right;
    [HideInInspector]
    public Vector2 velocity;

    [HideInInspector]
    public Vector2 avgFlockHeading;
    [HideInInspector]
    public Vector2 avgAvoidanceHeading;
    [HideInInspector]
    public Vector2 centreOfFlockmates;
    [HideInInspector]
    public int numPerceivedFlockmates;

    public void Init(BoidSettings settings, BoidSkin skin, Transform target) {
        this.settings = settings;
        this.target = target;

        this.GetComponent<Animator>().runtimeAnimatorController = skin.skin;
        this.GetComponent<BoxCollider2D>().size = skin.colliderWidthHeight;
        this.GetComponent<BoxCollider2D>().offset = skin.colliderOffset;

        this.position = base.transform.position;
        this.right = base.transform.right;

        float startSpeed = (this.settings.minSpeed + this.settings.maxSpeed) / 2;
        this.velocity = this.right * startSpeed;
    }

    public void UpdateBoid() {
        Vector2 acceleration = Vector2.zero;

        if (this.target != null) {
            Vector2 offsetToTarget = (Vector2) this.target.position - this.position;
            acceleration = SteerTowards(offsetToTarget) * this.settings.targetWeight;
        }

        if (this.numPerceivedFlockmates != 0) {
            this.centreOfFlockmates /= this.numPerceivedFlockmates;

            Vector2 offsetToFlockmatesCentre = (this.centreOfFlockmates - this.position);

            Vector2 alignmentForce = SteerTowards(this.avgFlockHeading) * this.settings.alignWeight;
            Vector2 cohesionForce = SteerTowards(offsetToFlockmatesCentre) * this.settings.cohesionWeight;
            Vector2 seperationForce = SteerTowards(this.avgAvoidanceHeading) * this.settings.seperateWeight;

            acceleration += alignmentForce;
            acceleration += cohesionForce;
            acceleration += seperationForce;
        }

        if (IsHeadingForCollision()) {
            Vector2 collisionAvoidDir = ObstacleRays();
            Vector2 collisionAvoidForce = SteerTowards(collisionAvoidDir) * this.settings.avoidCollisionWeight;
            acceleration += collisionAvoidForce;
        }

        this.velocity += acceleration * Time.deltaTime;
        float speed = this.velocity.magnitude;
        Vector2 dir = this.velocity / speed;
        speed = Mathf.Clamp(speed, this.settings.minSpeed, this.settings.maxSpeed);
        this.velocity = dir * speed;

        base.transform.position += (Vector3) this.velocity * Time.deltaTime;
        base.transform.right = dir;
        this.position = base.transform.position;
        this.right = dir;
    }

    private bool IsHeadingForCollision() {
        if (Physics2D.CircleCast(this.position, this.settings.boundsRadius, this.right, this.settings.collisionAvoidDst, this.settings.obstacleMask)) {
            return true;
        } else { }

        return false;
    }

    private Vector2 ObstacleRays() {
        Vector2[] rayDirections = BoidHelper.directions;

        for (int i = 0; i < rayDirections.Length; i++) {
            Vector2 dir = base.transform.TransformDirection(rayDirections[i]);
            if (!Physics2D.CircleCast(this.position, this.settings.boundsRadius, dir, this.settings.collisionAvoidDst, this.settings.obstacleMask)) {
                return dir;
            }
        }

        return this.right;
    }

    private Vector2 SteerTowards(Vector2 vector) {
        Vector2 v = vector.normalized * this.settings.maxSpeed - this.velocity;
        return Vector2.ClampMagnitude(v, this.settings.maxSteerForce);
    }
}
