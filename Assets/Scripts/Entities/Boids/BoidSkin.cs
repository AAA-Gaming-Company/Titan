using UnityEngine;

[CreateAssetMenu(fileName = "Assets", menuName = "Scriptable Objects/Boid Skin")]
public class BoidSkin : ScriptableObject {
    public RuntimeAnimatorController skin;
    public float haloSize;
    public Color haloColour;
}
