using ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

//TODO Use Finite State Machines to implement the Robber Behaviour if time permits
public class Robber : MonoBehaviour
{
    [SerializeField] private Cop copScript;
    [SerializeField] private CopData copData;
    [SerializeField] private float minimumDistanceFromCop;
    [SerializeField] private GameObject target;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private RobberData robberData;
    
    private void Update()
    {
        //BhanuEvade(); // I like this effect more
        //Evade();
        //Flee(target.transform.position);
        //Pursue();
        Wander();
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
