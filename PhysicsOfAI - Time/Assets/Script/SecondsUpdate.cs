using UnityEngine;

public class SecondsUpdate : MonoBehaviour
{
    private float _timeStartOffset = 0; // Variable to store seconds elapsed since starting.
    private bool _gotStartTime = false; // Variable to ensure offset is only changed once.

    private void Update()
    {
        // Using real-word time to move the character.
        if(!_gotStartTime)
        {
            _timeStartOffset = Time.realtimeSinceStartup;
            _gotStartTime = true;
        }

        transform.position = new Vector3(transform.position.x , this.transform.position.y , Time.realtimeSinceStartup - _timeStartOffset);
    }
}
