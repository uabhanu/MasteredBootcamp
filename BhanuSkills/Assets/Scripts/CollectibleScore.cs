using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleScore : MonoBehaviour
{
    private float _defaultSliderValue;
    private float _collected;
    private float _totalToCollect;
    private GameObject[] _totalObjs;

    [SerializeField] private GameObject diegeticScoreObj;
    [SerializeField] private Gradient scoreBarGradient;
    [SerializeField] private Image scoreBarFillImage;
    [SerializeField] private Slider scoreSlider;
    [SerializeField] private TMP_Text collectedValueLabel;
    [SerializeField] private TMP_Text totalValueLabel;

    private void Start()
    {
        _collected = 0;
        collectedValueLabel.text = _collected.ToString();
        _defaultSliderValue = scoreSlider.value;

        if(diegeticScoreObj != null)
        {
            diegeticScoreObj.SetActive(false);    
        }
        
        scoreBarFillImage.color = scoreBarGradient.Evaluate(_defaultSliderValue);
        _totalObjs = GameObject.FindGameObjectsWithTag("Collectible");
        _totalToCollect = _totalObjs.Length;
        totalValueLabel.text = _totalToCollect.ToString();
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    private void OnCollectibleCollected()
    {
        _collected++;
        collectedValueLabel.text = _collected.ToString();
        scoreSlider.value += 1 / _totalToCollect;
        scoreBarFillImage.color = scoreBarGradient.Evaluate(scoreSlider.normalizedValue);
    }

    private void OnDiegeticUIEnabled()
    {
        if(diegeticScoreObj != null)
        {
            diegeticScoreObj.SetActive(true);    
        }
    }

    private void OnNonDiegeticUIEnabled()
    {
        if(diegeticScoreObj != null)
        {
            diegeticScoreObj.SetActive(false);    
        }
    }
    
    #region Event Listeners
    private void SubscribeToEvents()
    {
        EventsManager.SubscribeToEvent(BhanuSkillsEvent.CollectibleCollectedEvent , OnCollectibleCollected);
        EventsManager.SubscribeToEvent(BhanuSkillsEvent.DiegeticUIEnabledEvent , OnDiegeticUIEnabled);
        EventsManager.SubscribeToEvent(BhanuSkillsEvent.NonDiegeticUIEnabledEvent , OnNonDiegeticUIEnabled);
    }
    
    private void UnsubscribeFromEvents()
    {
        EventsManager.UnsubscribeFromEvent(BhanuSkillsEvent.CollectibleCollectedEvent , OnCollectibleCollected);
        EventsManager.UnsubscribeFromEvent(BhanuSkillsEvent.DiegeticUIEnabledEvent , OnDiegeticUIEnabled);
        EventsManager.UnsubscribeFromEvent(BhanuSkillsEvent.NonDiegeticUIEnabledEvent , OnNonDiegeticUIEnabled);
    }
    #endregion
}
