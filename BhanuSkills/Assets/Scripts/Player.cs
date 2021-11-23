using Events;
using StarterAssets;
using UnityEngine;

public class Player : ThirdPersonController //Inheritance
{
    private bool _keyFound = false;

    private void OnHealthGain()
    {
        
    }
    
    private void OnHealthLoss()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals("Door"))
        {
            EventsManager.InvokeEvent(BhanuSkillsEvent.DoorCloseEvent);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Door"))
        {
            EventsManager.InvokeEvent(BhanuSkillsEvent.DoorOpenEvent);
        }
    }

    public bool GetKeyFound()
    {
        return _keyFound;
    }
    
    private void SetKeyFound(bool keyFound)
    {
        _keyFound = keyFound;
    }

    #region Event Listeners
    private void SubscribeToEvents()
    {
        EventsManager.SubscribeToEvent(BhanuSkillsEvent.HealthGainEvent , OnHealthGain);
        EventsManager.SubscribeToEvent(BhanuSkillsEvent.HealthLossEvent , OnHealthLoss);
    }
    
    private void UnsubscribeFromEvents()
    {
        EventsManager.UnsubscribeFromEvent(BhanuSkillsEvent.HealthGainEvent , OnHealthGain);
        EventsManager.UnsubscribeFromEvent(BhanuSkillsEvent.HealthLossEvent , OnHealthLoss);
    }
    #endregion
}
