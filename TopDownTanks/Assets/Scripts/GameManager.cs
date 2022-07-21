using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables
    
    private GameObject _playerObj;
    private SaveSystem _saveSystem;

    [SerializeField] private ScoreManager scoreManager;
    
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
            _playerObj = playerInput.gameObject;
        }

        _saveSystem = FindObjectOfType<SaveSystem>();

        if(_playerObj != null && _saveSystem != null && _saveSystem.LoadedData != null)
        {
            var damageable = _playerObj.GetComponentInChildren<Damageable>();

            if(damageable != null)
            {
                damageable.Health = _saveSystem.LoadedData.PlayerHealth;   
            }
        }
    }
    
    public void LoadLevel()
    {
        if(_saveSystem != null && _saveSystem.LoadedData != null)
        {
            SceneManager.LoadScene(_saveSystem.LoadedData.SceneIndex);
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
        if(_playerObj != null)
        {
            _saveSystem.SaveData(scoreManager.CoinsScoreValue , SceneManager.GetActiveScene().buildIndex + 1 , _playerObj.GetComponentInChildren<Damageable>().Health);
        }
    }

    #endregion
}
