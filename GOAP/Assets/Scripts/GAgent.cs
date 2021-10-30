using System.Collections.Generic;
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
    private GPlanner _gPlanner;
    private Queue<GAction> _actionQueue;
    private SubGoal _currentSubGoal;

    [SerializeField] private GAction currentAction;
    [SerializeField] private List<GAction> actionsList = new List<GAction>();
    
    public Dictionary<SubGoal , int> SubGoalsList = new Dictionary<SubGoal , int>();

    private void Start()
    {
        GAction[] acts = GetComponents<GAction>();

        foreach(GAction ga in acts)
        {
            actionsList.Add(ga);
        }
    }

    private void LateUpdate()
    {
        
    }
}
