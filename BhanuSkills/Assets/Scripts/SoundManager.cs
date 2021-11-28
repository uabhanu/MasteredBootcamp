using Events;
using ScriptableObjects;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioClip _audioClipToPlay;
    
    [SerializeField] private AudioSource musicSource;
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
    
    #region Event Functions
    private void OnHealthAlmostEmpty()
    {
        _audioClipToPlay = soundManagerData.HeartBeatClip;
        soundsSource.clip = _audioClipToPlay;
        soundsSource.Play();
    }

    private void OnKeyCollected()
    {
        musicSource.Stop();
        soundsSource.Stop();
    }

    private void OnPause()
    {
        musicSource.Pause();
    }

    private void OnUnpause()
    {
        musicSource.Play();
    }
    #endregion

    #region Event Listeners
    private void SubscribeToEvents()
    {
        EventsManager.SubscribeToEvent(BhanuSkillsEvent.HealthAlmostEmptyEvent , OnHealthAlmostEmpty);
        EventsManager.SubscribeToEvent(BhanuSkillsEvent.KeyCollectedEvent , OnKeyCollected);
        EventsManager.SubscribeToEvent(BhanuSkillsEvent.PauseEvent , OnPause);
        EventsManager.SubscribeToEvent(BhanuSkillsEvent.UnpauseEvent , OnUnpause);
    }
    
    private void UnsubscribeFromEvents()
    {
        EventsManager.UnsubscribeFromEvent(BhanuSkillsEvent.HealthAlmostEmptyEvent , OnHealthAlmostEmpty);
        EventsManager.UnsubscribeFromEvent(BhanuSkillsEvent.KeyCollectedEvent , OnKeyCollected);
        EventsManager.UnsubscribeFromEvent(BhanuSkillsEvent.PauseEvent , OnPause);
        EventsManager.UnsubscribeFromEvent(BhanuSkillsEvent.UnpauseEvent , OnUnpause);
    }
    #endregion
}
