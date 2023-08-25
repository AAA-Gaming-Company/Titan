using UnityEngine;

[CreateAssetMenu(fileName = "Assets", menuName = "Scriptable Objects/Voice Command")]
public class VoiceCommand : ScriptableObject {
    public VoiceLine[] lines;
}
