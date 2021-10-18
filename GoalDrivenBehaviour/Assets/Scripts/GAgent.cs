using System.Collections.Generic;
using UnityEngine;

public class SubGoal 
{
    // Dictionary to store our goals
    public Dictionary<string , int> sGoals;
    // Bool to store if goal should be removed
    public bool remove;

    // Constructor
    public SubGoal(string s , int i , bool r) 
    {
        sGoals = new Dictionary<string , int>();
        sGoals.Add(s , i);
        remove = r;
    }
}

public class GAgent : MonoBehaviour 
{
    private GPlanner _planner;
    private Queue<GAction> _actionQueue;
    private SubGoal _currentGoal;
    
    public Dictionary<SubGoal , int> goals = new Dictionary<SubGoal , int>();
    public GAction currentAvtion;
    public List<GAction> actions = new List<GAction>();
    public WorldStates beliefs = new WorldStates();
    
    private void Start()
    {
        var acts = this.GetComponents<GAction>();

        for(var index = 0; index < acts.Length; index++)
        {
            var a = acts[index];
            actions.Add(a);
        }
    }

    private void LateUpdate() 
    {

    }
}


