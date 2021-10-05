using ScriptableObjects;
using UnityEngine;

// A very simplistic car driving on the x-z plane.

public class Drive : MonoBehaviour 
{
    // Bhanu changed all the public variables to [Serializable] private as we shouldn't use public just for the sake of showing in the inspector
    
    //Auto Pilot On & Off created by Bhanu
    private const float AutoSpeed = 0.1f;

    [SerializeField] private bool autoPilotOn;

    // Tank speed
    [SerializeField] private float speed = 10.0f;
    // Tank rotation speed
    [SerializeField] private float rotationSpeed = 100.0f;
    // Public GameObject to store the fuelData in
    [SerializeField] private FuelData fuelData; // This is a Scriptable Object used by Bhanu

    private void Update() 
    {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        var translation = Input.GetAxis("Vertical") * speed;
        var rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, translation, 0);

        // Rotate around our y-axis
        transform.Rotate(0, 0, -rotation);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            CalculateAngle();
            CalculateDistance();   
        }

        if(Input.GetKeyDown(KeyCode.T))
        {
            autoPilotOn = !autoPilotOn;
        }

        if(autoPilotOn)
        {
            AutoPilot();
        }
    }

    private void AutoPilot()
    {
        if(!(CalculateDistance() > 5)) return;
        CalculateAngle();
        transform.Translate(transform.up * AutoSpeed , Space.World);
    }

    private void CalculateAngle()
    {
        var tankForward = transform.up;
        var fuelDirection = fuelData.FuelPosition - transform.position;

        var dot = tankForward.x * fuelDirection.x + tankForward.y * fuelDirection.y;
        var angle = Mathf.Acos(dot / (tankForward.magnitude * fuelDirection.magnitude));
        
        Debug.Log("Angle : " + angle * Mathf.Rad2Deg);
        Debug.Log("Unity Angle : " + Vector3.Angle(tankForward , fuelDirection));
        
        Debug.DrawRay(transform.position , tankForward * 10f , Color.green , 2);
        Debug.DrawRay(transform.position , fuelDirection , Color.red , 2);
        
        var clockwise = 1;

        if(CrossProduct(tankForward , fuelDirection).z < 0)
        {
            clockwise = -1;
        }

        var unityAngle = Vector3.SignedAngle(tankForward , fuelDirection , transform.forward);

        transform.Rotate(0 , 0 , unityAngle * 0.02f); //This will apply a fraction of angle instead of the whole to give a smooth rotation
    }
    
    // Calculate the distance from the tank to the fuelData
    private float CalculateDistance() 
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

        return distance;
    }

    private Vector3 CrossProduct(Vector3 vector1 , Vector3 vector2)
    {
        var xMultiplication = vector1.y * vector2.z - vector1.z * vector2.y;
        var yMultiplication = vector1.z * vector2.x - vector1.x * vector2.z;
        var zMultiplication = vector1.x * vector2.y - vector1.y * vector2.x;

        var crossProduct = new Vector3(xMultiplication , yMultiplication , zMultiplication);

        return crossProduct;
    }
}