using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUIManager : MonoBehaviour
{
    #region Variables
    
    [SerializeField] private GameObject inGameMenuObj;
    [SerializeField] private GameObject pauseMenuObj;
    
    #endregion
    
    #region Functions

    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseButton()
    {
        inGameMenuObj.SetActive(false);
        pauseMenuObj.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResumeButton()
    {
        inGameMenuObj.SetActive(true);
        pauseMenuObj.SetActive(false);
        Time.timeScale = 1;
    }
    
    #endregion
}
