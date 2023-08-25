using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {
    [Header("Elemnts")]
    public Button playButton;
    public Button creditsButton;
    public Button creditsCloseButton;
    public Button quitButton;
    public ProgressBar loadingBar;
    public Button noobButton;
    public Button proButton;
    public Button epicButton;

    [Header("Screens")]
    public GameObject creditsScreen;
    public GameObject levelSelect;
    public GameObject loadingScreen;

    [Header("Boids")]
    public Transform mouseTransform;

    private Camera cam;

    private void Start() {
        this.playButton.onClick.AddListener(this.ClickPlay);
        this.creditsButton.onClick.AddListener(this.ClickCredits);
        this.creditsCloseButton.onClick.AddListener(this.ClickCreditsHide);
        this.quitButton.onClick.AddListener(this.ClickQuit);

        this.noobButton.onClick.AddListener(this.ClickLevelNoob);
        this.proButton.onClick.AddListener(this.ClickLevelPro);
        this.epicButton.onClick.AddListener(this.ClickLevelEpic);

        this.creditsScreen.SetActive(false);
        this.levelSelect.SetActive(false);
        this.loadingScreen.SetActive(false);

        this.cam = Camera.main;
    }

    private void Update() {
        this.mouseTransform.position = this.cam.ScreenToWorldPoint(Input.mousePosition);
    }

    public void ClickPlay() {
        this.levelSelect.SetActive(true);
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

    private void ClickLevelNoob() {
        this.ClickLevel(0);
    }

    private void ClickLevelPro() {
        this.ClickLevel(1);
    }

    private void ClickLevelEpic() {
        this.ClickLevel(2);
    }

    public void ClickLevel(int level) {
        GameManager.difficultyLevel = level;
        StartCoroutine(this.LoadGameScene());
    }

    private IEnumerator LoadGameScene() {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Game");
        this.loadingScreen.SetActive(true);

        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            this.loadingBar.UpdateValue(progress);
            yield return null;
        }

        Destroy(this.gameObject);
    }
}
