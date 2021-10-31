using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SubGoal
{
    public bool bRemove;
    public Dictionary<string , int> SubGoals;

    public SubGoal(string s , int i , bool r)
    {
        SubGoals = new Dictionary<string , int>();
        SubGoals.Add(s , i);
        bRemove = r;
    }
}
public class GAgent : MonoBehaviour
{
    private bool _invoked = false;
    private GPlanner _gPlanner;
    private Queue<GAction> _actionQueue;
    private SubGoal _currentSubGoal;

    [SerializeField] private GAction currentAction;
    [SerializeField] private List<GAction> actionsList = new List<GAction>();
    
    public Dictionary<SubGoal , int> SubGoalsList = new Dictionary<SubGoal , int>();

    //Unlike the usual Start(), the function here is public as it needs to be accessed by child classes like Patient, etc.
    public void Start()
    {
        GAction[] acts = GetComponents<GAction>();

        foreach(GAction ga in acts)
        {
            actionsList.Add(ga);
        }
    }

    private void LateUpdate()
    {
        if(currentAction != null && currentAction.Running)
        {
            //If navmesh agent has a goal and has reached the goal
            if(currentAction.NavMeshAgent.hasPath && currentAction.NavMeshAgent.remainingDistance < 1f)
            {
                if(!_invoked)
                {
                    Invoke("CompleteAction" , currentAction.Duration);
                    _invoked = true;
                }
            }

            return;
        }
        
        if(_gPlanner == null || _actionQueue == null)
        {
            _gPlanner = new GPlanner();

            var sortedGoals = from entry in SubGoalsList orderby entry.Value descending select entry;

            foreach(KeyValuePair<SubGoal , int> subGoal in sortedGoals)
            {
                _actionQueue = _gPlanner.PlansQueue(actionsList , subGoal.Key.SubGoals , null);

                if(_actionQueue != null)
                {
                    _currentSubGoal = subGoal.Key;
                    break;
                }
            }
        }

        if(_actionQueue != null && _actionQueue.Count == 0)
        {
            if(_currentSubGoal.bRemove)
            {
                SubGoalsList.Remove(_currentSubGoal);
            }

            _gPlanner = null;
        }

        if(_actionQueue != null && _actionQueue.Count > 0)
        {
            currentAction = _actionQueue.Dequeue();

            if(currentAction.PrePerform())
            {
                if(currentAction.TargetObj == null && currentAction.TargetObjTag != null)
                {
                    currentAction.TargetObj = GameObject.FindWithTag(currentAction.TargetObjTag); //Never used FindWithTag before
                }

                if(currentAction.TargetObj != null)
                {
                    currentAction.Running = true;
                    currentAction.NavMeshAgent.SetDestination(currentAction.TargetObj.transform.position);
                }
            }
            else
            {
                _actionQueue = null;
            }
        }
    }

    private void CompleteAction()
    {
        currentAction.Running = false;
        currentAction.PostPerform();
        _invoked = false;
    }
}
