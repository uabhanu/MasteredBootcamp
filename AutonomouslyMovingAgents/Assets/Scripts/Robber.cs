using ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

public class Robber : MonoBehaviour
{
    private bool _coolDown;
    
    [SerializeField] private Cop copScript;
    [SerializeField] private CopData copData;
    [SerializeField] private float minimumDistanceFromCop;
    [SerializeField] private GameObject target;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private RobberData robberData;
    
    private void Update()
    {
        //BhanuEvade(); // I like this effect more

        if(!_coolDown)
        {
            if(CopCanSeeMe() && CanSeeCop())
            {
                CleverHide();
                _coolDown = true;
                Invoke("BehaviourCooldown" , 5);
            }
            else
            {
                Pursue();
            }   
        }
        //Evade();
        //Flee(target.transform.position);
        //Hide();
        //Pursue();
        //Wander();
    }

    private void BehaviourCooldown()
    {
        _coolDown = false;
    }
    
    private void BhanuEvade()
    {
        var targetDir = target.transform.position - transform.position;
        var lookAhead = targetDir.magnitude / (agent.speed + copData.CurrentSpeed);
        var relativeHeading = Vector3.Angle(transform.forward , -transform.TransformVector(target.transform.forward));
        var fromTargetAngle = Vector3.Angle(transform.forward , transform.TransformVector(targetDir));
        var currentDistanceAwayFromCop = Vector3.Distance(transform.position , copScript.gameObject.transform.position);

        if((fromTargetAngle < 90 && relativeHeading > 20) || currentDistanceAwayFromCop < minimumDistanceFromCop)
        {
             Flee(target.transform.position);
        }
    }

    private bool CanSeeCop()
    {
        RaycastHit hit;
        var rayToTarget = target.transform.position - transform.position;

        if(Physics.Raycast(transform.position , rayToTarget , out hit))
        {
            if(hit.transform.gameObject.CompareTag("Cop"))
            {
                return true;
            }
        }

        return false;
    }
    
    private void CleverHide()
    {
        var distance = Mathf.Infinity;
        var chosenDirection = Vector3.zero;
        var chosenSpot = Vector3.zero;

        var chosenGameObject = World.GetHidingSpots()[0];

        for(var i = 0; i < World.GetHidingSpots().Length; i++)
        {
            var hideDir = World.GetHidingSpots()[i].transform.position - target.transform.position;
            var hidePos = World.GetHidingSpots()[i].transform.position + hideDir.normalized;

            if(Vector3.Distance(transform.position , hidePos) < distance)
            {
                chosenSpot = hidePos;
                chosenDirection = hideDir;
                chosenGameObject = World.GetHidingSpots()[i];
                distance = Vector3.Distance(transform.position , hidePos);
            }
        }
        
        var hideCollider = chosenGameObject.GetComponent<Collider>();

        Ray backRay = new Ray(chosenSpot , -chosenDirection.normalized);
        RaycastHit hit;
        var rayDistance = 100.0f;
        hideCollider.Raycast(backRay , out hit , rayDistance);

        Seek(hit.point + chosenDirection.normalized);
    }

    private bool CopCanSeeMe()
    {
        var toAgent = transform.position - target.transform.position;
        var lookingAngle = Vector3.Angle(target.transform.forward , toAgent);

        if(lookingAngle < 60)
        {
            return true;
        }

        return false;
    }

    private void Evade()
    {
        var targetDir = target.transform.position - transform.position;
        var lookAhead = targetDir.magnitude / (agent.speed + copData.CurrentSpeed);
        Flee(target.transform.position + target.transform.forward * lookAhead);
    }
    
    private void Flee(Vector3 location)
    {
        var fleeVector = location - transform.position;
        agent.SetDestination(transform.position - fleeVector);
    }

    private void Hide()
    {
        var distance = Mathf.Infinity;
        var chosenSpot = Vector3.zero;

        for(var i = 0; i < World.GetHidingSpots().Length; i++)
        {
            var hideDir = World.GetHidingSpots()[i].transform.position - target.transform.position;
            var hidePos = World.GetHidingSpots()[i].transform.position + hideDir.normalized;

            if(Vector3.Distance(transform.position , hidePos) < distance)
            {
                chosenSpot = hidePos;
                distance = Vector3.Distance(transform.position , hidePos);
            }
        }

        Seek(chosenSpot);
    }

    private void Pursue()
    {
        var targetDir = target.transform.position - transform.position;
        var relativeHeading = Vector3.Angle(transform.forward , transform.TransformVector(target.transform.forward));
        var toTargetAngle = Vector3.Angle(transform.forward , transform.TransformVector(targetDir));

        if((toTargetAngle > 90 && relativeHeading < 20) || (copData.CurrentSpeed < 0.01f))
        {
            Seek(target.transform.position);
        }
        
        var lookAhead = targetDir.magnitude / (agent.speed + copData.CurrentSpeed);
        Seek(target.transform.position + target.transform.forward * lookAhead);
    }
    
    private void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    private void Wander()
    {
        var wanderDistance = robberData.WanderDistance;
        var wanderJitter = robberData.WanderJitter;
        var wanderRadius = robberData.WanderRadius;
        var wanderTarget = robberData.WanderTarget;
        
        var randomValue = Random.Range(-1.0f , 1.0f);
        wanderTarget += new Vector3(randomValue * wanderJitter , 0 , randomValue * wanderJitter);
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        var localTarget  = wanderTarget + new Vector3(0 , 0 , wanderDistance);
        var worldTarget = gameObject.transform.InverseTransformVector(localTarget);
        
        Seek(worldTarget);
    }
}
