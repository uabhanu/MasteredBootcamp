using Bhanu;
using Events;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private float _defaultSliderValue;
    private int _maxTime;
    
    [SerializeField] private CountdownTimer countDownTimer;
    [SerializeField] private Gradient healthBarGradient;
    [SerializeField] private HealthBarData healthBarData;
    [SerializeField] private Image healthBarFillImage;
    [SerializeField] private Image heartImage;
    [SerializeField] private Player playerObj;
    [SerializeField] private Slider healthBarSlider;

    private void Start()
    {
        _defaultSliderValue = healthBarSlider.value;
        healthBarFillImage.color = healthBarGradient.Evaluate(1f);
        _maxTime = healthBarData.MAXTime;
    }

    private void LateUpdate()
    {
        CalculateAndApplySprite();
        
         if(!playerObj.GetKeyCollected())
         {
            healthBarSlider.value = countDownTimer.SecondsLeft * (_defaultSliderValue / _maxTime);
            healthBarFillImage.color = healthBarGradient.Evaluate(healthBarSlider.normalizedValue);
            
            if(countDownTimer.SecondsLeft <= 15)
            {
                EventsManager.InvokeEvent(BhanuSkillsEvent.HealthAlmostEmptyEvent);
            }

            if(countDownTimer.SecondsLeft <= 0)
            {
                EventsManager.InvokeEvent(BhanuSkillsEvent.GameOverEvent);
            }   
         }
    }

    private void CalculateAndApplySprite()
    {
        float healthBarPercentage = (healthBarSlider.value / _defaultSliderValue) * 100;
        Sprite spriteToUse;

        if(healthBarPercentage < 66f)
        {
            spriteToUse = healthBarData.YellowSprite;
            heartImage.sprite = spriteToUse;
        }
        
        if(healthBarPercentage < 33f)
        {
            spriteToUse = healthBarData.RedSprite;
            heartImage.sprite = spriteToUse;
        }
    }
    
    public float DefaultSliderValue
    {
        get => _defaultSliderValue;
        set => _defaultSliderValue = value;
    }
}
