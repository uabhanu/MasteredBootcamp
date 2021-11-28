using Events;
using StarterAssets;
using UnityEngine;

public class Player : ThirdPersonController //Inheritance
{
    private bool _keyCollected = false;
    private bool _keyFound = false;

    [SerializeField] private InGameUIManager inGameUIManager;

    private void Start()
    {
        base.Start();
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    // private void OnTriggerExit(Collider other)
    // {
    //     if(other.tag.Equals("Door"))
    //     {
    //         EventsManager.InvokeEvent(BhanuSkillsEvent.DoorCloseEvent);
    //     }
    // }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Collectible"))
        {
            if(inGameUIManager.TotalCollectedByPlayer == inGameUIManager.TotalToCollect)
            {
                EventsManager.InvokeEvent(BhanuSkillsEvent.AllCollectedEvent);
            }
        }
        
        if(other.tag.Equals("Door"))
        {
            EventsManager.InvokeEvent(BhanuSkillsEvent.DoorOpenEvent);
        }

        if(other.tag.Equals("Heart"))
        {
            EventsManager.InvokeEvent(BhanuSkillsEvent.HealthGainEvent);
        }

        if(other.tag.Equals("Info"))
        {
            EventsManager.InvokeEvent(BhanuSkillsEvent.InfoEvent);
        }

        if(other.tag.Equals("Key"))
        {
            EventsManager.InvokeEvent(BhanuSkillsEvent.KeyCollectedEvent);
        }
        
        if(other.tag.Equals("Trophy"))
        {
            EventsManager.InvokeEvent(BhanuSkillsEvent.TrophyCollectedEvent);
        }
    }

    private void OnAllCollected()
    {
        _keyFound = true;
        SetKeyFound(_keyFound);
    }

    private void OnKeyCollected()
    {
        _keyCollected = true;
        SetKeyCollected(_keyCollected);
    }
    
    private void SetKeyCollected(bool keyCollected)
    {
        _keyCollected = keyCollected;
    }

    private void SetKeyFound(bool keyFound)
    {
        _keyFound = keyFound;
    }
    
    public bool GetKeyCollected()
    {
        return _keyCollected;
    }
    
    
    #region Event Listeners
    private void SubscribeToEvents()
    {
        EventsManager.SubscribeToEvent(BhanuSkillsEvent.AllCollectedEvent , OnAllCollected);
        EventsManager.SubscribeToEvent(BhanuSkillsEvent.KeyCollectedEvent , OnKeyCollected);
    }
    
    private void UnsubscribeFromEvents()
    {
        EventsManager.UnsubscribeFromEvent(BhanuSkillsEvent.AllCollectedEvent , OnAllCollected);
        EventsManager.UnsubscribeFromEvent(BhanuSkillsEvent.KeyCollectedEvent , OnKeyCollected);
    }
    #endregion
}
