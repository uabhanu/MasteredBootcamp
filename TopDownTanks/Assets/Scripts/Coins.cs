using UnityEngine;
using UnityEngine.Events;
using Util;

[RequireComponent(typeof(DestroyHelper))]
public class Coins : MonoBehaviour
{
    #region Variables

    private ScoreHelper _scoreHelper;
    private ScoreManager _scoreManager;
    
    [SerializeField] private UnityEvent onCollected;

    #endregion
    
    #region Functions
    
    private void Start()
    {
        _scoreHelper = GetComponent<ScoreHelper>();
        _scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D col2D)
    {
        onCollected?.Invoke();
        _scoreManager.CoinsScoreUpdate(_scoreHelper.ScoreIncrement);
    }
    
    #endregion
}
