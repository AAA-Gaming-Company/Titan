using UnityEngine;

[CreateAssetMenu(fileName = "Assets", menuName = "Scriptable Objects/Boid Skin")]
public class BoidSkin : ScriptableObject {
    public RuntimeAnimatorController skin;
    public Vector2 colliderWidthHeight;
    public Vector2 colliderOffset;
    public float haloSize;
    public Color haloColour;
}
