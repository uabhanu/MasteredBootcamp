using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("BhanuSkills");
    }

    public void QuitButton()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
