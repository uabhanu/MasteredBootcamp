using UnityEngine;

public class FollowWp : MonoBehaviour 
{
    // Array of way points
    public GameObject[] waypoints;
    // waypoint index
    private int _currentWp = 0;
    // Tank speed
    public float speed = 10.0f;
    // Tank rotation speed
    public float rotSpeed = 10.0f;
    // Limit how far the tracker moves in front of the tank
    public float lookAhead = 10.0f;

    // Store a tracker that the tanks will follow
    private GameObject _trackerObj;

    private void Start() 
    {

        // Create a cylinder for visual purposes
        _trackerObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        // Destroy the cylinder collider so it doesn'y cuase any issues with physics
        DestroyImmediate(_trackerObj.GetComponent<Collider>());
        // Disable the trackers mesh renderer so you can't see it in the game
        _trackerObj.GetComponent<MeshRenderer>().enabled = false;
        // Rotate and place the tracker
        _trackerObj.transform.position = transform.position;
        _trackerObj.transform.rotation = transform.rotation;
    }

    private void ProcessTracker() 
    {

        // Check the tracker doesn't get to far ahead of the tank
        if(Vector3.Distance(_trackerObj.transform.position , transform.position) > lookAhead) return;

        // Check if the tank has reached a certain distance from the current waypoint
        if(Vector3.Distance(_trackerObj.transform.position , waypoints[_currentWp].transform.position) < 3.0f) 
        {
            // Select next waypoint
            _currentWp++;
        }

        // Check we haven't reached the last waypoint
        if(_currentWp >= waypoints.Length) 
        {
            // Reset if we have
            _currentWp = 0;
        }

        // Aim the tracker at the current waypoint
        _trackerObj.transform.LookAt(waypoints[_currentWp].transform);
        // Move the tracker towards the waypoint
        _trackerObj.transform.Translate(0.0f , 0.0f , (speed + 20.0f) * Time.deltaTime);
    }

    // Update is called once per frame
    private void Update() 
    {
        // Call the ProcessTracker method to move the tracker
        ProcessTracker();

        // Create a Quaternion to look at a Vector
        var lookAtWp = Quaternion.LookRotation(_trackerObj.transform.position - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookAtWp, rotSpeed * Time.deltaTime);
        // Move the tank
        this.transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
    }
}