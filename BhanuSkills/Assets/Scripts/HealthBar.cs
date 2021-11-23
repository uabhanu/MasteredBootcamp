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
    [SerializeField] private Slider healthBarSlider;

    private void Start()
    {
        _defaultSliderValue = healthBarSlider.value;
        healthBarFillImage.color = healthBarGradient.Evaluate(1f);
        _maxTime = healthBarData.MAXTime;
    }

    private void LateUpdate()
    {
        healthBarSlider.value = countDownTimer.SecondsLeft * (_defaultSliderValue / _maxTime);
        healthBarFillImage.color = healthBarGradient.Evaluate(healthBarSlider.normalizedValue);
    }
}
