using System.Collections;
using Events;
using ScriptableObjects;
using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    private bool _bGamePaused;
    private bool _takingAway = false;
    private int _secondsLeft = 60;

    [SerializeField] private HealthBarData healthBarData;
    [SerializeField] private Player playerObj;
    [SerializeField] private TMP_Text timerDisplayText;
    
    #region MonoBehaviour Functions
    
    private void Start()
    {
        _secondsLeft = healthBarData.MAXTime;
        SubscribeToEvents();
        timerDisplayText.text = "00 : " + _secondsLeft;
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    private void Update()
    {
        if(!playerObj.GetKeyCollected())
        {
            if(!_takingAway && _secondsLeft > 0 && !_bGamePaused)
            {
                StartCoroutine(TimerTake());
            }   
        }
        else
        {
            timerDisplayText.enabled = false;
        }
    }

    private IEnumerator TimerTake()
    {
        _takingAway = true;
        yield return new WaitForSeconds(1f);
        _secondsLeft--;
        _takingAway = false;
        timerDisplayText.text = "00 : " + _secondsLeft;
    }
    
    #endregion
    
    #region Helper Functions
    
    public int SecondsLeft
    {
        get => _secondsLeft;
        set => _secondsLeft = value;
    }
    
    #endregion

    #region Event Functions
    
    private void OnPause()
    {
        _bGamePaused = true;
    }
    
    private void OnUnpause()
    {
        _bGamePaused = false;
    }
    
    #endregion
    
    #region Event Listeners
    private void SubscribeToEvents()
    {
        EventsManager.SubscribeToEvent(BhanuSkillsEvent.PauseEvent , OnPause);
        EventsManager.SubscribeToEvent(BhanuSkillsEvent.UnpauseEvent , OnUnpause);
    }
    
    private void UnsubscribeFromEvents()
    {
        EventsManager.UnsubscribeFromEvent(BhanuSkillsEvent.PauseEvent , OnPause);
        EventsManager.UnsubscribeFromEvent(BhanuSkillsEvent.UnpauseEvent , OnUnpause);
    }
    #endregion
}
