using System.Collections;
using TMPro;
using UnityEngine;

public class TextPlayer : MonoBehaviour {
    public TextMeshProUGUI text;
    
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
