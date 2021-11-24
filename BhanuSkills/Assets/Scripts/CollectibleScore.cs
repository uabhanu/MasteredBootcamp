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
    
    [SerializeField] private GameObject scoreBarWorldCanvasObj;
    [SerializeField] private Gradient scoreBarGradient;
    [SerializeField] private Image scoreBarFillImage;
    [SerializeField] private InGameMenuManager ingameMenuManager;
    [SerializeField] private Slider scoreSlider;
    [SerializeField] private TMP_Text collectedDisplayLabel;
    [SerializeField] private TMP_Text collectedValueLabel;
    [SerializeField] private TMP_Text totalDisplayLabel;
    [SerializeField] private TMP_Text totalValueLabel;

    private void Start()
    {
        _collected = 0;
        collectedValueLabel.text = _collected.ToString();
        _defaultSliderValue = scoreSlider.value;
        scoreBarFillImage.color = scoreBarGradient.Evaluate(_defaultSliderValue);
        _totalObjs = GameObject.FindGameObjectsWithTag("Collectible");
        _totalToCollect = _totalObjs.Length;
        totalValueLabel.text = _totalToCollect.ToString();
        SubscribeToEvents();
        
        ingameMenuManager = FindObjectOfType<InGameMenuManager>();
        
         if(!ingameMenuManager.DiegeticUI)
         {
            scoreBarWorldCanvasObj.SetActive(false);
         }
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
    
    #region Event Listeners
    private void SubscribeToEvents()
    {
        EventsManager.SubscribeToEvent(BhanuSkillsEvent.CollectibleCollectedEvent , OnCollectibleCollected);
    }
    
    private void UnsubscribeFromEvents()
    {
        EventsManager.UnsubscribeFromEvent(BhanuSkillsEvent.CollectibleCollectedEvent , OnCollectibleCollected);
    }
    #endregion
}
