using UnityEngine;

public class UpdateMove : MonoBehaviour
{
    private const float Speed = 2.0f; // Variable used to multiply value of Time.deltaTime to increase rate that character moves.

    private void Update()
    {
        transform.Translate(0, 0, Time.deltaTime * Speed); // Move the character in the Z direction, using 'time elapsed between updates' as amount.
    }
}
