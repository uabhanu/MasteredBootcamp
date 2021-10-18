using System.Collections.Generic;
using GOAP;
using TMPro;
using UnityEngine;

public class UpdateWorld : MonoBehaviour
{
    private Dictionary<string , int> _worldStates;

    [SerializeField] private TextMeshProUGUI statesTMP;

    private void Start()
    {
        _worldStates = GWorld.Instance.GetWorld().GetStates();
        statesTMP.text = "";
    }

    private void LateUpdate()
    {
        foreach(var s in _worldStates)
        {
            statesTMP.text += s.Key + " , " + s.Value + "\n"; 
            // TODO You are getting different world states display result probably due to TMP so fix it when possible and if needed
        }
    }
}
