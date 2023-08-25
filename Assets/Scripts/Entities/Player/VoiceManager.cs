using System.Collections;
using TMPro;
using UnityEngine;

public class VoiceManager : Singleton<VoiceManager> {
    public TextMeshProUGUI text;

    private AudioSource audioSource;

    private void Start() {
        this.audioSource = this.gameObject.AddComponent<AudioSource>();
        this.audioSource.playOnAwake = false;
    }

    //Can be called like so: VoiceManager.Instance.SendCommand([VOICE COMMAND SO]);
    public void SendCommand(VoiceCommand command) {
        VoiceLine line = command.lines[Random.Range(0, command.lines.Length)];

        this.audioSource.clip = line.clip;
        this.audioSource.Play();
        StartCoroutine(ShowText(line.transcription, line.displayTime));
    }

    private IEnumerator ShowText(string line, float time) {
        this.text.text = line;
        yield return new WaitForSeconds(time);
        this.text.text = "";
    }
}
