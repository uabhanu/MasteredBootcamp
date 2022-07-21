using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _scoreValue;
    
    [SerializeField] private TMP_Text scoreValueLabelTMP; 

    public void ScoreUpdate(int increment)
    {
        _scoreValue += increment;
        scoreValueLabelTMP.text = _scoreValue.ToString();
    }
}
