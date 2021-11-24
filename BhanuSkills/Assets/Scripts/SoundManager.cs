using System;
using Events;
using ScriptableObjects;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioClip _audioClipToPlay;
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private SoundManagerData soundManagerData;

    private void Start()
    {
        _audioClipToPlay = soundManagerData.DangerClip;
        audioSource.clip = _audioClipToPlay;
        audioSource.Play();
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    private void OnKeyCollected()
    {
        audioSource.Stop();
    }

    #region Event Listeners
    private void SubscribeToEvents()
    {
        EventsManager.SubscribeToEvent(BhanuSkillsEvent.KeyCollectedEvent , OnKeyCollected);
    }
    
    private void UnsubscribeFromEvents()
    {
        EventsManager.UnsubscribeFromEvent(BhanuSkillsEvent.KeyCollectedEvent , OnKeyCollected);
    }
    #endregion
}
