using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    #region Variables
    
    private int _coinsScoreValue;
    private int _scoreValue;

    [SerializeField] private TMP_Text eurosValueLabelTMP;
    [SerializeField] private TMP_Text scoreValueLabelTMP;

    #endregion

    #region Functions
    
    public int CoinsScoreValue
    {
        get => _coinsScoreValue;
        set => _coinsScoreValue = value;
    }
    
    public void CoinsScoreUpdate(int increment)
    {
        _coinsScoreValue += increment;
        eurosValueLabelTMP.text = _coinsScoreValue.ToString();
    }
    
    public void ScoreUpdate(int increment)
    {
        _scoreValue += increment;
        scoreValueLabelTMP.text = _scoreValue.ToString();
    }
    
    #endregion
}
