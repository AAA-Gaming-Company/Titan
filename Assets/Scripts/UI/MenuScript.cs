using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {
    public Button playButton;
    public Button creditsButton;
    public Button quitButton;

    private void Start() {
        this.playButton.onClick.AddListener(this.ClickPlay);
        this.creditsButton.onClick.AddListener(this.ClickCredits);
        this.quitButton.onClick.AddListener(this.ClickQuit);
    }

    public void ClickPlay() {
        throw new UnityException(":))");
    }

    public void ClickCredits() {
        throw new UnityException(":))");
    }

    public void ClickQuit() {
        Application.Quit();
    }
}
