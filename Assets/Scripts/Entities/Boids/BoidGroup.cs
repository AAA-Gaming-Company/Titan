using UnityEngine;

public class BoidGroup : MonoBehaviour {
    public int flockSize = 10;
    public GameObject boidPrefab;
    public BoidSkin[] skinPool;
    public Transform target;

    public BoidSettings settings;
    public ComputeShader compute;

    private Boid[] boids = null;

    public void Spawn() {
        this.boids = new Boid[this.flockSize];

        float spreadFactor = this.flockSize * 0.2f;

        //Spawn in all of the boids
        for (int i = 0; i < this.flockSize; i++) {
            Vector3 spawnPos = base.transform.position;
            spawnPos.x += Random.Range(-spreadFactor, spreadFactor);
            spawnPos.y += Random.Range(-spreadFactor, spreadFactor);

            Boid boid = Instantiate(this.boidPrefab, spawnPos, Quaternion.identity).GetComponent<Boid>();

            this.boids[i] = boid;
            boid.transform.parent = this.transform;

            BoidSkin boidSkin = this.skinPool[Random.Range(0, this.skinPool.Length)];
            boid.Init(this.settings, boidSkin, this.target);
        }
    }

    private void Update() {
        if (this.boids == null) {
            return;
        }

        int numBoids = this.boids.Length;
        BoidData[] boidData = new BoidData[numBoids];

        for (int i = 0; i < numBoids; i++) {
            boidData[i].position = this.boids[i].position;
            boidData[i].direction = this.boids[i].right;
        }

        ComputeBuffer boidBuffer = new ComputeBuffer(numBoids, BoidData.Size);
        boidBuffer.SetData(boidData);

        this.compute.SetBuffer(0, "boids", boidBuffer);
        this.compute.SetInt("numBoids", numBoids);
        this.compute.SetFloat("viewRadius", this.settings.perceptionRadius);
        this.compute.SetFloat("avoidRadius", this.settings.avoidanceRadius);

        int threadGroups = Mathf.CeilToInt(numBoids / 1024f);
        this.compute.Dispatch(0, threadGroups, 1, 1);

        boidBuffer.GetData(boidData);

        for (int i = 0; i < numBoids; i++) {
            this.boids[i].avgFlockHeading = boidData[i].flockHeading;
            this.boids[i].centreOfFlockmates = boidData[i].flockCentre;
            this.boids[i].avgAvoidanceHeading = boidData[i].avoidanceHeading;
            this.boids[i].numPerceivedFlockmates = boidData[i].numFlockmates;

            this.boids[i].UpdateBoid();
        }

        boidBuffer.Release();
    }

    public void Kill() {
        for (int i = 0; i < this.boids.Length; i++) {
            Destroy(this.boids[i]);
        }
        this.boids = null;
        Destroy(base.gameObject);
    }

    public struct BoidData {
        public Vector2 position;
        public Vector2 direction;

        public Vector2 flockHeading;
        public Vector2 flockCentre;
        public Vector2 avoidanceHeading;
        public int numFlockmates;

        public static int Size {
            get {
                return sizeof(float) * 2 * 5 + sizeof(int);
            }
        }
    }
}
