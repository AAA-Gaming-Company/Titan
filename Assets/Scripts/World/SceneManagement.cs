using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
