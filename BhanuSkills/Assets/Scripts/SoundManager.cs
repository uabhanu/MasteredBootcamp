using Events;
using ScriptableObjects;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioClip _audioClipToPlay;
    
    [SerializeField] private AudioSource soundsSource;
    [SerializeField] private SoundManagerData soundManagerData;

    private void Start()
    {
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }
    
    private void OnHealthAlmostEmpty()
    {
        _audioClipToPlay = soundManagerData.HeartBeatClip;
        soundsSource.clip = _audioClipToPlay;
        soundsSource.Play();
    }

    private void OnKeyCollected()
    {
        soundsSource.Stop();
    }
    

    #region Event Listeners
    private void SubscribeToEvents()
    {
        EventsManager.SubscribeToEvent(BhanuSkillsEvent.HealthAlmostEmptyEvent , OnHealthAlmostEmpty);
        EventsManager.SubscribeToEvent(BhanuSkillsEvent.KeyCollectedEvent , OnKeyCollected);
    }
    
    private void UnsubscribeFromEvents()
    {
        EventsManager.UnsubscribeFromEvent(BhanuSkillsEvent.HealthAlmostEmptyEvent , OnHealthAlmostEmpty);
        EventsManager.UnsubscribeFromEvent(BhanuSkillsEvent.KeyCollectedEvent , OnKeyCollected);
    }
    #endregion
}
