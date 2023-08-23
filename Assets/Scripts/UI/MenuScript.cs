using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {
    [Header("Buttons")]
    public Button playButton;
    public Button creditsButton;
    public Button quitButton;
    public Button creditsCloseButton;

    [Header("Screens")]
    public GameObject creditsScreen;
    public GameObject loadingScreen;

    [Header("Boids")]
    public Transform mouseTransform;

    private Camera cam;

    private void Start() {
        this.playButton.onClick.AddListener(this.ClickPlay);
        this.creditsButton.onClick.AddListener(this.ClickCredits);
        this.quitButton.onClick.AddListener(this.ClickQuit);
        this.creditsCloseButton.onClick.AddListener(this.ClickCreditsHide);

        this.creditsScreen.SetActive(false);
        this.loadingScreen.SetActive(false);

        this.cam = Camera.main;
    }

    private void Update() {
        this.mouseTransform.position = this.cam.ScreenToWorldPoint(Input.mousePosition);
    }

    public void ClickPlay() {
        throw new UnityException(":))");
    }

    public void ClickCredits() {
        this.creditsScreen.SetActive(true);
    }

    public void ClickCreditsHide() {
        this.creditsScreen.SetActive(false);
    }

    public void ClickQuit() {
        Application.Quit();
    }
}
