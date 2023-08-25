using UnityEngine;
using UnityEngine.UI;

public class TutorialTriggerArea : TriggerArea {
    [Header("Tutorial")]
    public GameObject tutoScreen;
    public Button closeButton;

    private void Start() {
        this.tutoScreen.SetActive(false);
        this.closeButton.onClick.AddListener(this.CloseButton);
        this.destroyOnTrigger = false;
    }

    protected override void TriggerAction() {
        this.tutoScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    private void CloseButton() {
        Time.timeScale = 1f;
        Destroy(this.tutoScreen);
        Destroy(this.gameObject);
    }
}
