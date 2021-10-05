using ScriptableObjects;
using UnityEngine;

// A very simplistic car driving on the x-z plane.

public class Drive : MonoBehaviour 
{
    // Bhanu changed all the public variables to [Serializable] private as we shouldn't use public just for the sake of showing in the inspector
    // Tank speed
    [SerializeField] private float speed = 10.0f;
    // Tank rotation speed
    [SerializeField] private float rotationSpeed = 100.0f;
    // Public GameObject to store the fuelData in
    [SerializeField] private FuelData fuelData; // This is a Scriptable Object used by Bhanu

    // Calculate the distance from the tank to the fuelData
    private void CalculateDistance() 
    {
        // Tank position
        var tP = this.transform.position; //Rider recommended using var so did it.
        // Fuel position
        var fP = fuelData.FuelPosition;

        // Calculate the distance using pythagoras
        float distance = Mathf.Sqrt(Mathf.Pow(tP.x - fP.x, 2.0f) + Mathf.Pow(tP.y - fP.y, 2.0f) + Mathf.Pow(tP.z - fP.z, 2.0f));
        // Calculate the distance using Unitys vector distance function
        float unityDistance = Vector3.Distance(tP, fP); // Bhanu understood this as Unity's Distance also uses Pythagoras Theorem

        // Print out the two results to the console
        Debug.Log("Distance: " + distance);
        Debug.Log("Unity Distance: " + unityDistance);
    }

    private void Update() 
    {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, translation, 0);

        // Rotate around our y-axis
        transform.Rotate(0, 0, -rotation);

        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            CalculateDistance();
        }
    }
}