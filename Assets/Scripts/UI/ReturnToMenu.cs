using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour {
    public void ReturnToMenuScene() {
        SceneManager.LoadScene(0);
    }
}
