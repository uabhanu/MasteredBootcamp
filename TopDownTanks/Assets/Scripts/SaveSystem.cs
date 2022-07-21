using UnityEngine;
using UnityEngine.Events;

public class SaveSystem : MonoBehaviour
{
    #region Variables

    [SerializeField] private string eurosCollectedKey = "EurosCollected";
    [SerializeField] private string playerHealthKey = "PlayerHealth";
    [SerializeField] private string savePresentKey = "SavePresent";
    [SerializeField] private string sceneIndexKey = "SceneIndex";
    [SerializeField] private UnityEvent<bool> onDataLoadedResult;
    
    public LoadedData LoadedData { get; private set; }
    
    #endregion
    
    #region Functions

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        var result = LoadData();
        onDataLoadedResult?.Invoke(result);
    }

    public bool LoadData()
    {
        if(PlayerPrefs.GetInt(savePresentKey) == 1)
        {
            LoadedData = new LoadedData();
            LoadedData.EurosCollected = PlayerPrefs.GetInt(eurosCollectedKey);
            LoadedData.PlayerHealth = PlayerPrefs.GetInt(playerHealthKey);
            LoadedData.SceneIndex = PlayerPrefs.GetInt(sceneIndexKey);
            return true;
        }

        return false;
    }

    public void ResetData()
    {
        LoadedData = null;
        PlayerPrefs.DeleteAll();
    }

    public void SaveData(int eurosCollected , int sceneIndex , int playerHealth)
    {
        if(LoadedData == null)
        {
            LoadedData = new LoadedData();
            LoadedData.EurosCollected = eurosCollected;
            LoadedData.PlayerHealth = playerHealth;
            LoadedData.SceneIndex = sceneIndex;

            PlayerPrefs.SetInt(eurosCollectedKey , eurosCollected);
            PlayerPrefs.SetInt(playerHealthKey , playerHealth);
            PlayerPrefs.SetInt(sceneIndexKey , sceneIndex);
            PlayerPrefs.SetInt(savePresentKey , 1);
        }
    }

    #endregion
}

public class LoadedData
{
    #region Variables

    public int EurosCollected = 0;
    public int PlayerHealth = -1;
    public int SceneIndex = -1;
    
    #endregion
}
