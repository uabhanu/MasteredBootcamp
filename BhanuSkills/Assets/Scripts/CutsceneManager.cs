using Events;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private GameObject winCutSceneObj;
    private void Start()
    {
        winCutSceneObj.SetActive(false);
        SubscribeToEvents();   
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    #region Event Functions
    private void OnTrophyCollected()
    {
        winCutSceneObj.SetActive(true);
    }
    #endregion

    #region Event Listeners
    private void SubscribeToEvents()
    {
        EventsManager.SubscribeToEvent(BhanuSkillsEvent.TrophyCollectedEvent , OnTrophyCollected);
    }
    
    private void UnsubscribeFromEvents()
    {
        EventsManager.UnsubscribeFromEvent(BhanuSkillsEvent.TrophyCollectedEvent , OnTrophyCollected);
    }
    #endregion
}
