using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Light2D))]

public class Boid : MonoBehaviour {
    [Header("Boid")]
    public GameObject haloObject;

    private BoidSettings settings;
    private Transform target;
    private BoidSkin skin;

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

    public void Init(BoidSettings settings, Transform target, BoidSkin skin) {
        //Skin settings
        this.GetComponent<Animator>().runtimeAnimatorController = skin.skin;
        this.haloObject.transform.localScale = 1.5f * new Vector3(skin.haloSize, skin.haloSize);
        this.haloObject.GetComponent<SpriteRenderer>().color = skin.haloColour;
        this.GetComponent<Light2D>().pointLightOuterRadius = 3 * skin.haloSize;

        //Set the halo Z pos
        Vector3 haloPos = this.haloObject.transform.position;
        haloPos.z += 0.1f * skin.identifier;
        this.haloObject.transform.position = haloPos;

        this.settings = settings;
        this.target = target;
        this.skin = skin;
        this.position = base.transform.position;
        this.right = base.transform.right;

        //Init stuff
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
            Vector2 seperationForce = SteerTowards(this.avgAvoidanceHeading) * (this.settings.seperateWeight * this.skin.sizeFactor);

            acceleration += alignmentForce;
            acceleration += cohesionForce;
            acceleration += seperationForce;
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

    private Vector2 SteerTowards(Vector2 vector) {
        Vector2 v = vector.normalized * this.settings.maxSpeed - this.velocity;
        return Vector2.ClampMagnitude(v, this.settings.maxSteerForce);
    }

    public void UpdateTarget(Transform target) {
        this.target = target;
    }
}
