using UnityEngine;

public class BoidGroup : MonoBehaviour {
    public int flockSize = 10;
    public Boid boidPrefab;
    public BoidSettings settings;

    private void Start() {
        //Spawn in all of the boids
        for (int i = 0; i < this.flockSize; i++) {
            Boid boid = Instantiate(this.boidPrefab.gameObject, base.transform.position, Quaternion.identity).GetComponent<Boid>();

        }
    }
}
