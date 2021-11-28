using Events;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameUIManager : MonoBehaviour
{
    private float _maxHealth;
    private GameObject[] _totalCollectibleObjs;
    private int _totalCollectedByPlayer = 0;
    private int _totalToCollect;
    
    #region private Serialized Variables Declarations

    [SerializeField] private bool diegeticUI;
    [SerializeField] private GameObject gameOverMenuObj;
    [SerializeField] private GameObject goToRoofTextObj;
    [SerializeField] private GameObject healthBarOverlayObj;
    [SerializeField] private GameObject healthUIDiagetic;
    [SerializeField] private GameObject healthUINonDiagetic;
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
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private Image healthBarFillImage;
    [SerializeField] private Image healthBarWorldFillImage;
    [SerializeField] private Player playerObj;
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private Slider healthBarWorldSlider;
    #endregion

    #region All Other Functions
    private void Start()
    {
        if(diegeticUI)
        {
            healthUIDiagetic.SetActive(true);

            healthUINonDiagetic.SetActive(false);
            minimapObj.SetActive(false);
            scoreUINonDiagetic.SetActive(false);
        }
        else
        {
            healthUIDiagetic.SetActive(false);

            healthUINonDiagetic.SetActive(true);
            minimapObj.SetActive(true);
            scoreUINonDiagetic.SetActive(true);
        }
        
        gameOverMenuObj.SetActive(false);
        healthBarOverlayObj.SetActive(false);
        infoObj.SetActive(false);
        keyObj.SetActive(false);
        pauseMenuObj.SetActive(false);
        goToRoofTextObj.SetActive(false);
        roofTextObj.SetActive(false);
        trophyObj.SetActive(false);
        _totalCollectibleObjs = GameObject.FindGameObjectsWithTag("Collectible");
        _totalToCollect = _totalCollectibleObjs.Length;
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }
    
    public bool DiegeticUI
    {
        get => diegeticUI;
        set => diegeticUI = value;
    }

    private void DisplayMenu()
    {
        gameOverMenuObj.SetActive(true);
    }

    private void SetMaxHealth()
    {
        _maxHealth = healthBar.DefaultSliderValue;
        healthBarSlider.value = _maxHealth;
        healthBarFillImage.color = healthBarGradient.Evaluate(healthBarSlider.normalizedValue);
        healthBarWorldSlider.value = _maxHealth;
        healthBarWorldFillImage.color = healthBarWorldGradient.Evaluate(healthBarWorldSlider.normalizedValue);
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
        healthBarOverlayObj.SetActive(false);
        DisplayMenu();
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
        Time.timeScale = 0;
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
