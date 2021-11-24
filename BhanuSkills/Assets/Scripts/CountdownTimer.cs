using System.Collections;
using ScriptableObjects;
using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    private bool _takingAway = false;
    private int _secondsLeft = 60;

    [SerializeField] private HealthBarData healthBarData;
    [SerializeField] private Player playerObj;
    [SerializeField] private TMP_Text timerDisplayText;
    
    private void Start()
    {
        _secondsLeft = healthBarData.MAXTime;
        timerDisplayText.text = "00 : " + _secondsLeft;
    }
    
    private void Update()
    {
        if(!playerObj.GetKeyCollected())
        {
            if(!_takingAway && _secondsLeft > 0)
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
    
    public int SecondsLeft
    {
        get => _secondsLeft;
        set => _secondsLeft = value;
    }
}
