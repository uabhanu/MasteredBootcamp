using System;
using BhanuAssets.Scripts.ScriptableObjects;
using Events;
using System.Collections;
using TMPro;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class CountdownTimer : MonoBehaviour
    {
        private bool _takingAway = false;
        private int _secondsLeft;

        [SerializeField] private CountdownData countDownData;
        //[SerializeField] private TMP_Text timerDisplayText;

        private void Start()
        {
            _secondsLeft = countDownData.MAXTime;
            //timerDisplayText.text = "00 : " + _secondsLeft;
        }

        private void Update()
        {
            if(!_takingAway && _secondsLeft > 0)
            {
                //timerDisplayText.enabled = true;
                StartCoroutine(TimerTake());
            }

            if(_secondsLeft <= 0)
            {
                //timerDisplayText.enabled = false;
                EventsManager.InvokeEvent(BhanuEvent.NoInternetEvent);
            }
        }

        public void ResetCounter()
        {
            _secondsLeft = countDownData.MAXTime;
            //timerDisplayText.text = "00 : " + _secondsLeft;
            StartCoroutine(TimerTake());
        }

        private IEnumerator TimerTake()
        {
            _takingAway = true;
            yield return new WaitForSeconds(1f);
            _secondsLeft--;
            _takingAway = false;
            //timerDisplayText.text = "00 : " + _secondsLeft;
        }
        
        public int SecondsLeft
        {
            get => _secondsLeft;
            set => _secondsLeft = value;
        }
    }
}
