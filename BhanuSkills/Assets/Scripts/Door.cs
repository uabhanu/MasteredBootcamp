using System;
using Events;
using ScriptableObjects;
using UnityEngine;

public class Door : MonoBehaviour
{
    private static readonly int DoorOpen = Animator.StringToHash("DoorOpen");

    private bool _doorOpened;
    private Material _materialToUse;

    [SerializeField] private Animator doorAnim;
    [SerializeField] private DoorData doorData;
    [SerializeField] private Player player;

    private void Start()
    {
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    private void LateUpdate()
    {
        if(player.GetKeyCollected())
        {
            _materialToUse = doorData.UnlockedMaterial;
            GetComponent<MeshRenderer>().material = _materialToUse;
        }
        else
        {
            _materialToUse = doorData.LockedMaterial;
            GetComponent<MeshRenderer>().material = _materialToUse;
        }
    }

    private void OnDoorClose()
    {
        if(_doorOpened)
        {
            _doorOpened = false;
            doorAnim.SetBool(DoorOpen , _doorOpened);   
        }
    }

    private void OnDoorOpen()
    {
        _doorOpened = true;
        doorAnim.SetBool(DoorOpen , player.GetKeyCollected());
    }

    #region Event Listeners
    private void SubscribeToEvents()
    {
        EventsManager.SubscribeToEvent(BhanuSkillsEvent.DoorCloseEvent , OnDoorClose);
        EventsManager.SubscribeToEvent(BhanuSkillsEvent.DoorOpenEvent , OnDoorOpen);
    }
    
    private void UnsubscribeFromEvents()
    {
        EventsManager.UnsubscribeFromEvent(BhanuSkillsEvent.DoorCloseEvent , OnDoorClose);
        EventsManager.UnsubscribeFromEvent(BhanuSkillsEvent.DoorOpenEvent , OnDoorOpen);
    }
    #endregion
}
