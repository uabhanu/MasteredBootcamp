using System;
using Events;
using UnityEngine;

public class AimTurret : MonoBehaviour
{
    #region Serialized Field Private Variable Declarations
    
    [SerializeField] private float maxRotAngle;
    [SerializeField] private float offset;
    [SerializeField] private float rotationSpeed;
    
    #endregion

    #region MonoBehaviour Functions
    
    private void Start()
    {
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }
    
    #endregion
    
    #region Player Input Event Functions

    private void OnMoveTurret(Vector2 mouseVector)
    {
        float currentZRotation = transform.rotation.z * 2 * Mathf.Rad2Deg; //For now have to do x2 to get the rotation value correctly;

        
        //The rotation of the turret doesn't go beyond the max angle which is good but once the max angle hits, the whole rotation stops and doesn't start again
        //TODO Fix above
        if(Math.Abs(currentZRotation - Mathf.Clamp(currentZRotation , -maxRotAngle , maxRotAngle)) < offset)
        {
            Debug.Log("Rotate Now");
            transform.Rotate(0f , 0f , -mouseVector.x * rotationSpeed * Time.deltaTime , Space.Self);
        }

        if(currentZRotation < -maxRotAngle)
        {
            transform.rotation = new Quaternion(0f , 0f , 0f , -maxRotAngle); //This is giving some strange effect which is not so bad
        }
        
        if(currentZRotation > maxRotAngle)
        {
            transform.rotation = new Quaternion(0f , 0f , 0f , maxRotAngle); //This is giving some strange effect which is not so bad
        }
    }
    
    #endregion
    
    #region Event Listeners

    private void SubscribeToEvents()
    {
        EventsManager.SubscribeToPlayerInputEvent(PlayerInputEvent.InputEventMoveTurret , OnMoveTurret);
    }
    
    private void UnsubscribeFromEvents()
    {
        EventsManager.UnsubscribeFromPlayerInputEvent(PlayerInputEvent.InputEventMoveTurret , OnMoveTurret);
    }

    #endregion
}
