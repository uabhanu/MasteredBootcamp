using ScriptableObjects;
using UnityEngine;

public class Drive : MonoBehaviour
{
    [SerializeField] private CopData copData;

    private void Update()
    {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        var translation = Input.GetAxis("Vertical") * copData.Speed;
        var rotation = Input.GetAxis("Horizontal") * copData.RotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0 , 0 , translation);
        copData.CurrentSpeed = translation;

        // Rotate around our y-axis
        transform.Rotate(0 , rotation , 0);
    }
}
