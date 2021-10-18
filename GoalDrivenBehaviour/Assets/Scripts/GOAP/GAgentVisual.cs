using GOAP;
using UnityEngine;

[ExecuteInEditMode]
public class GAgentVisual : MonoBehaviour
{
    public GAgent thisAgent;
    
    private void Start()
    {
        thisAgent = GetComponent<GAgent>();
    }

    private void Update()
    {
        
    }
}
