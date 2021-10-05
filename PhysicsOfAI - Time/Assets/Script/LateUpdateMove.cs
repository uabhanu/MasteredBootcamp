using UnityEngine;

public class LateUpdateMove : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.Translate(0 , 0 , Time.deltaTime); // Move the character in the Z direction, using 'time elapsed between updates' as amount.
    }
}
