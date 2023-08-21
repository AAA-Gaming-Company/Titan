using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Boid : MonoBehaviour {
    private BoidSettings settings;

    public void Init(BoidSettings settings) {
        this.settings = settings;
    }
}
