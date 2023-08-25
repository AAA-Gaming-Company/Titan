using System.Collections;
using TMPro;
using UnityEngine;

public class VoiceManager : MonoBehaviour {
    [Header("Commands")]
    public VoiceCommand objective;
    public VoiceCommand stayOnTarget;
    public VoiceCommand fishFriends;
    public VoiceCommand dodgeBullets;
    public VoiceCommand megaMech;
    public VoiceCommand finalBoss;
    public VoiceCommand wellDone;

    [Header("Display")]
    public TextMeshProUGUI text;

    public void SendCommand(VoiceCommand command) { //You can change this argument, I don't know what ot put
        VoiceLine line = command.lines[Random.Range(0, command.lines.Length)]; //Select a random lie to play in the command
        //Read the info and play it
    }
    
    public void DisplayText(string[] lines, float[] times) {
        StartCoroutine(ShowText(lines, times));
    }

    IEnumerator ShowText(string[] lines, float[] times) {
        for (int i = 0; i < lines.Length; i++) {
            text.text = lines[i];
            yield return new WaitForSeconds(times[i]);
        }

        text.text = " ";
    }
}
