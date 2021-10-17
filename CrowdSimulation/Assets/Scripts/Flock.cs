using UnityEngine;

public class Flock : MonoBehaviour 
{ 
    // Bool used to check the swim limits
    private bool _turning = false;
    
    // Prefab initial speed;
    private float _speed;
    
    // Access the FlockManager script
    public FlockManager myManager;

    private void Start() 
    {
        // Assign a random speed to each this prefab
        _speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
    }

    // Update is called once per frame
    private void Update() 
    {
        // Determine the bounding box of the manager cube
        var b = new Bounds(myManager.transform.position , myManager.swimLimits * 2.0f);

        // If the fish is outside the bounds of the cube or about to hit something
        // then start turning around
        var hit = new RaycastHit();
        var direction = Vector3.zero;

        if(!b.Contains(transform.position)) 
        {
            _turning = true;
            direction = myManager.transform.position - transform.position;
        } 
        
        else if(Physics.Raycast(transform.position , this.transform.forward * 50.0f , out hit)) 
        {
            _turning = true;
            // Debug.DrawRay(this.transform.position, this.transform.forward * 50.0f, Color.red);
            direction = Vector3.Reflect(this.transform.forward , hit.normal);
        } 
        
        else 
        {

            _turning = false;
        }

        // Test if we're turning
        if(_turning) 
        {
            // Turn towards the centre of the cube
            transform.rotation = Quaternion.Slerp(transform.rotation , Quaternion.LookRotation(direction) , myManager.rotationSpeed * Time.deltaTime);
        } 
        else 
        {
            // 10% chance of altering prefab speed
            if(Random.Range(0.0f , 100.0f) < 10.0f) 
            {
                _speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
            }

            // 20& chance of applying the flocking rules
            if(Random.Range(0.0f , 100.0f) < 20.0f) 
            {
                ApplyRules();
            }
        }

        transform.Translate(0.0f , 0.0f, Time.deltaTime * _speed);
    }

    private void ApplyRules() 
    {
        float nDistance;
        
        GameObject[] gos;
        gos = myManager.allFish;

        var vcentre = Vector3.zero;
        var vavoid = Vector3.zero;
        var gSpeed = 0.01f;
        var groupSize = 0;

        for(var index = 0; index < gos.Length; index++)
        {
            var go = gos[index];
            
            if(go != this.gameObject)
            {
                nDistance = Vector3.Distance(go.transform.position , this.transform.position);

                if(nDistance <= myManager.neighbourDistance)
                {
                    vcentre += go.transform.position;
                    groupSize++;

                    if(nDistance < 1.0f)
                    {
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }

                    var anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock._speed;
                }
            }
        }

        if(groupSize > 0) 
        {
            // Find the average centre of the group then add a vector to the target (goalPos)
            vcentre = vcentre / groupSize + (myManager.goalPos - this.transform.position);
            _speed = gSpeed / groupSize;

            var direction = (vcentre + vavoid) - transform.position;
            
            if(direction != Vector3.zero) 
            {
                transform.rotation = Quaternion.Slerp(transform.rotation , Quaternion.LookRotation(direction) , myManager.rotationSpeed * Time.deltaTime);
            }
        }
    }
}
