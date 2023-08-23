using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Light2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class Boid : Entity {
    [Header("Boid")]
    public GameObject haloObject;

    private BoidSettings settings;
    private BoidGroup group;
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

    public void Init(BoidSettings settings, BoidGroup group, BoidSkin skin, Transform target) {
        this.settings = settings;
        this.group = group;
        this.target = target;

        //Skin settings
        this.GetComponent<Animator>().runtimeAnimatorController = skin.skin;
        BoxCollider2D boxCollider2D = this.GetComponent<BoxCollider2D>();
        boxCollider2D.size = skin.colliderWidthHeight;
        boxCollider2D.offset = skin.colliderOffset;
        haloObject.transform.localScale = 1.5f * new Vector3(skin.haloSize, skin.haloSize);
        haloObject.GetComponent<SpriteRenderer>().color = skin.haloColour;
        this.GetComponent<Light2D>().pointLightOuterRadius = 3 * skin.haloSize;

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

    protected override void EntityStart() { //Ignore this
    }

    protected override void OnDie() {
        this.group.DeclareDead(this);
    }

    protected override void OnDamage(int amount) {
    }
}
