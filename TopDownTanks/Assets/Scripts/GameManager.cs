using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables
    
    public GameObject PlayerObj;
    public SaveSystem SaveSystem;
    
    #endregion

    #region Functions
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += Initialize;
    }

    private void Initialize(Scene scene , LoadSceneMode sceneMode)
    {
        var playerInput = FindObjectOfType<PlayerInput>();

        if(playerInput != null)
        {
            PlayerObj = playerInput.gameObject;
        }

        SaveSystem = FindObjectOfType<SaveSystem>();

        if(PlayerObj != null && SaveSystem != null && SaveSystem.LoadedData != null)
        {
            var damageable = PlayerObj.GetComponentInChildren<Damageable>();

            if(damageable != null)
            {
                damageable.Health = SaveSystem.LoadedData.PlayerHealth;   
            }
        }
    }
    
    public void LoadLevel()
    {
        if(SaveSystem != null && SaveSystem.LoadedData != null)
        {
            SceneManager.LoadScene(SaveSystem.LoadedData.SceneIndex);
            return;
        }
        
        LoadNextLevel();
    }
    
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitButton()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
                 Application.Quit();
        #endif
    }

    public void SaveData()
    {
        if(PlayerObj != null)
        {
            SaveSystem.SaveData(SceneManager.GetActiveScene().buildIndex + 1 , PlayerObj.GetComponentInChildren<Damageable>().Health);
        }
    }

    #endregion
}
