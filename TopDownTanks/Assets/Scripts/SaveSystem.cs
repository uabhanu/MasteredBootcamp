using UnityEngine;
using UnityEngine.Events;

public class SaveSystem : MonoBehaviour
{
    #region Variables
    
    public LoadedData LoadedData { get; private set; }
    public string PlayerHealthKey = "PlayerHealth";
    public string SavePresentKey = "SavePresent";
    public string SceneIndexKey = "SceneIndex";
    public UnityEvent<bool> OnDataLoadedResult;
    
    #endregion
    
    #region Functions

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        var result = LoadData();
        OnDataLoadedResult?.Invoke(result);
    }

    public bool LoadData()
    {
        if(PlayerPrefs.GetInt(SavePresentKey) == 1)
        {
            LoadedData = new LoadedData();
            LoadedData.PlayerHealth = PlayerPrefs.GetInt(PlayerHealthKey);
            LoadedData.SceneIndex = PlayerPrefs.GetInt(SceneIndexKey);
            return true;
        }

        return false;
    }

    public void ResetData()
    {
        LoadedData = null;
        PlayerPrefs.DeleteKey(PlayerHealthKey);
        PlayerPrefs.DeleteKey(SavePresentKey);
        PlayerPrefs.DeleteKey(SceneIndexKey);
    }

    public void SaveData(int sceneIndex , int playerHealth)
    {
        if(LoadedData == null)
        {
            LoadedData = new LoadedData();
            LoadedData.PlayerHealth = playerHealth;
            LoadedData.SceneIndex = sceneIndex;

            PlayerPrefs.SetInt(PlayerHealthKey , playerHealth);
            PlayerPrefs.SetInt(SceneIndexKey , sceneIndex);
            PlayerPrefs.SetInt(SavePresentKey , 1);
        }
    }

    #endregion
}

public class LoadedData
{
    #region Variables
    
    public int PlayerHealth = -1;
    public int SceneIndex = -1;
    
    #endregion
}
