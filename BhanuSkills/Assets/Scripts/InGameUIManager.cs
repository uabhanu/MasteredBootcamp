using Events;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameUIManager : MonoBehaviour
{
    private float _maxHealth;
    private float _maxHealthWorld;
    private GameObject[] _totalCollectibleObjs;
    private int _totalCollectedByPlayer = 0;
    private int _totalToCollect;
    private Sprite _heartSprite;
    private Sprite _heartWorldSprite;
    
    #region private Serialized Variables Declarations
    
    [SerializeField] private GameObject diegeticUIOffButtonObj;
    [SerializeField] private GameObject diegeticUIOnButtonObj;
    [SerializeField] private GameObject gameOverCanvasObj;
    [SerializeField] private GameObject goToRoofTextObj;
    [SerializeField] private GameObject healthBarOverlayObj;
    [SerializeField] private GameObject healthBarDiagetic;
    [SerializeField] private GameObject healthBarNonDiagetic;
    [SerializeField] private GameObject heartObj;
    [SerializeField] private GameObject infoObj;
    [SerializeField] private GameObject keyObj;
    [SerializeField] private GameObject minimapObj;
    [SerializeField] private GameObject pauseMenuObj;
    [SerializeField] private GameObject roofTextObj;
    [SerializeField] private GameObject scoreUINonDiagetic;
    [SerializeField] private GameObject trophyObj;
    [SerializeField] private Gradient healthBarGradient;
    [SerializeField] private Gradient healthBarWorldGradient;
    [SerializeField] private HealthBarData healthBarData;
    [SerializeField] private Image heartImage;
    [SerializeField] private Image heartWorldImage;
    [SerializeField] private Image healthBarFillImage;
    [SerializeField] private Image healthBarWorldFillImage;
    [SerializeField] private Player playerObj;
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private Slider healthBarWorldSlider;
    #endregion

    #region All Other Functions
    private void Start()
    {
        diegeticUIOffButtonObj.SetActive(false);
        diegeticUIOnButtonObj.SetActive(false);
        _heartSprite = healthBarData.GreenSprite;
        _heartWorldSprite = healthBarData.GreenSprite;
        _maxHealth = healthBarSlider.value;
        _maxHealthWorld = healthBarWorldSlider.value;

        gameOverCanvasObj.SetActive(false);
        healthBarOverlayObj.SetActive(false);
        infoObj.SetActive(false);
        keyObj.SetActive(false);
        pauseMenuObj.SetActive(false);
        goToRoofTextObj.SetActive(false);
        roofTextObj.SetActive(false);
        trophyObj.SetActive(false);
        _totalCollectibleObjs = GameObject.FindGameObjectsWithTag("Collectible");
        _totalToCollect = _totalCollectibleObjs.Length;
        SetNonDiegeticUI();
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    private void SetDiegeticUI()
    {
        diegeticUIOffButtonObj.SetActive(true);
        diegeticUIOnButtonObj.SetActive(false);
        EventsManager.InvokeEvent(BhanuSkillsEvent.DiegeticUIEnabledEvent);
        healthBarDiagetic.SetActive(true);
        healthBarNonDiagetic.SetActive(false);
        minimapObj.SetActive(false);
        scoreUINonDiagetic.SetActive(false);
    }

    private void SetNonDiegeticUI()
    {
        diegeticUIOffButtonObj.SetActive(false);
        diegeticUIOnButtonObj.SetActive(true);
        EventsManager.InvokeEvent(BhanuSkillsEvent.NonDiegeticUIEnabledEvent);
        healthBarDiagetic.SetActive(false);
        healthBarNonDiagetic.SetActive(true);
        minimapObj.SetActive(true);
        scoreUINonDiagetic.SetActive(true);
    }

    private void SetMaxHealth()
    {
        healthBarSlider.value = _maxHealth;
        healthBarFillImage.color = healthBarGradient.Evaluate(healthBarSlider.normalizedValue);
        healthBarWorldSlider.value = _maxHealthWorld;
        healthBarWorldFillImage.color = healthBarWorldGradient.Evaluate(healthBarWorldSlider.normalizedValue);
        heartImage.sprite = _heartSprite;
        heartWorldImage.sprite = _heartWorldSprite;
        heartObj.SetActive(false);
        infoObj.SetActive(true);
    }

    public int TotalCollectedByPlayer
    {
        get => _totalCollectedByPlayer;
        set => _totalCollectedByPlayer = value;
    }
    
    public int TotalToCollect
    {
        get => _totalToCollect;
        set => _totalToCollect = value;
    }

    #endregion
    
    #region Menu Button Functions

    public void DiegeticUIOffButton()
    {
        SetNonDiegeticUI();
    }
    
    public void DiegeticUIOnButton()
    {
        SetDiegeticUI();
    }
    
    public void PauseButton()
    {
        EventsManager.InvokeEvent(BhanuSkillsEvent.PauseEvent);
    }
    
    public void QuitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void RestartButton()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void ResumeButton()
    {
        EventsManager.InvokeEvent(BhanuSkillsEvent.UnpauseEvent);
    }
    #endregion
    
    #region Event Functions
    private void OnAllCollected()
    {
        keyObj.SetActive(true);
        keyObj.transform.position = new Vector3(playerObj.transform.position.x , playerObj.transform.position.y + 1.5f , playerObj.transform.position.z + 3.5f);
    }

    private void OnCollectibleCollected()
    {
        TotalCollectedByPlayer++;
    }
    
    private void OnGameOver()
    {
        diegeticUIOffButtonObj.SetActive(false);
        diegeticUIOnButtonObj.SetActive(false);
        gameOverCanvasObj.SetActive(true);
        gameObject.SetActive(false);
        healthBarOverlayObj.SetActive(false);
    }
    
    private void OnHealthAlmostEmpty()
    {
        healthBarOverlayObj.SetActive(true);
    }

    private void OnHealthGain()
    {
        SetMaxHealth();
    }

    private void OnInfo()
    {
        infoObj.SetActive(false);
        goToRoofTextObj.SetActive(true);
        trophyObj.SetActive(true);
    }

    private void OnKeyCollected()
    {
        healthBarOverlayObj.SetActive(false);
        keyObj.SetActive(false);
    }

    private void OnPause()
    {
        pauseMenuObj.SetActive(true);
    }

    private void OnTrophyCollected()
    {
        roofTextObj.SetActive(true);
        trophyObj.SetActive(false);
    }

    private void OnUnpause()
    {
        pauseMenuObj.SetActive(false);
        Time.timeScale = 1;
    }
    #endregion

    #region Event Listeners
    private void SubscribeToEvents()
    {
        EventsManager.SubscribeToEvent(BhanuSkillsEvent.AllCollectedEvent , OnAllCollected);
        EventsManager.SubscribeToEvent(BhanuSkillsEvent.CollectibleCollectedEvent , OnCollectibleCollected);
        EventsManager.SubscribeToEvent(BhanuSkillsEvent.GameOverEvent , OnGameOver);
        EventsManager.SubscribeToEvent(BhanuSkillsEvent.HealthAlmostEmptyEvent , OnHealthAlmostEmpty);
        EventsManager.SubscribeToEvent(BhanuSkillsEvent.HealthGainEvent , OnHealthGain);
        EventsManager.SubscribeToEvent(BhanuSkillsEvent.InfoEvent , OnInfo);
        EventsManager.SubscribeToEvent(BhanuSkillsEvent.KeyCollectedEvent , OnKeyCollected);
        EventsManager.SubscribeToEvent(BhanuSkillsEvent.PauseEvent , OnPause);
        EventsManager.SubscribeToEvent(BhanuSkillsEvent.TrophyCollectedEvent , OnTrophyCollected);
        EventsManager.SubscribeToEvent(BhanuSkillsEvent.UnpauseEvent , OnUnpause);
    }
    
    private void UnsubscribeFromEvents()
    {
        EventsManager.UnsubscribeFromEvent(BhanuSkillsEvent.AllCollectedEvent , OnAllCollected);
        EventsManager.UnsubscribeFromEvent(BhanuSkillsEvent.CollectibleCollectedEvent , OnCollectibleCollected);
        EventsManager.UnsubscribeFromEvent(BhanuSkillsEvent.GameOverEvent , OnGameOver);
        EventsManager.UnsubscribeFromEvent(BhanuSkillsEvent.HealthAlmostEmptyEvent , OnHealthAlmostEmpty);
        EventsManager.UnsubscribeFromEvent(BhanuSkillsEvent.HealthGainEvent , OnHealthGain);
        EventsManager.UnsubscribeFromEvent(BhanuSkillsEvent.InfoEvent , OnInfo);
        EventsManager.UnsubscribeFromEvent(BhanuSkillsEvent.KeyCollectedEvent , OnKeyCollected);
        EventsManager.UnsubscribeFromEvent(BhanuSkillsEvent.PauseEvent , OnPause);
        EventsManager.UnsubscribeFromEvent(BhanuSkillsEvent.TrophyCollectedEvent , OnTrophyCollected);
        EventsManager.UnsubscribeFromEvent(BhanuSkillsEvent.UnpauseEvent , OnUnpause);
    }
    #endregion
}
