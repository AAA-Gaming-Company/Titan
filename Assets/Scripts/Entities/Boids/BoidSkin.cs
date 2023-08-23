using UnityEngine;

[CreateAssetMenu(fileName = "Assets", menuName = "Scriptable Objects/Boid Skin")]
public class BoidSkin : ScriptableObject {
    public int identifier;
    public RuntimeAnimatorController skin;
    public float sizeFactor;
    public float haloSize;
    public Color haloColour;
}
