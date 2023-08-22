using UnityEngine;

public class BoidGroup : MonoBehaviour {
    public int flockSize = 10;
    public GameObject boidPrefab;
    public BoidSkin[] skinPool;
    public Transform target;

    public BoidSettings settings;
    public ComputeShader compute;

    private Boid[] boids;

    private void Start() {
        this.boids = new Boid[this.flockSize];

        float spreadFactor = this.flockSize * 0.2f;

        //Spawn in all of the boids
        for (int i = 0; i < this.flockSize; i++) {
            Vector3 spawnPos = base.transform.position;
            spawnPos.x += Random.Range(-spreadFactor, spreadFactor);
            spawnPos.y += Random.Range(-spreadFactor, spreadFactor);
            spawnPos.z += Random.Range(0, 2) == 0 ? -2 : 2; //This will give either -2 or 2, but not in between

            Boid boid = Instantiate(this.boidPrefab, spawnPos, Quaternion.identity).GetComponent<Boid>();

            this.boids[i] = boid;
            boid.transform.parent = this.transform;

            BoidSkin boidSkin = this.skinPool[Random.Range(0, this.skinPool.Length)];
            boid.Init(this.settings, this, boidSkin, this.target);
        }
    }

    private void Update() {
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

    public void DeclareDead(Boid deadBoid) {
        Boid[] oldArray = this.boids;
        int oldLengh = this.boids.Length;

        this.boids = new Boid[oldLengh - 1];
        int counter = 0;

        for (int i = 0; i < oldLengh; i++) {
            Boid boidInQuestion = oldArray[i];
            if (boidInQuestion.GetInstanceID() != deadBoid.GetInstanceID()) {
                this.boids[counter] = boidInQuestion;
                counter++;
            }
        }
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
