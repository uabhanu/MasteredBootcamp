using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// As Penny instructed, this is declared as an abstract class so this can be inherited later
public abstract class GAction : MonoBehaviour
{
    [SerializeField] private bool running = false;
    [SerializeField] private float cost = 1.0f;
    [SerializeField] private float duration = 0.0f;
    [SerializeField] private GameObject targetObj;
    [SerializeField] private GameObject targetTagObj;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private string actionName = "Action";
    [SerializeField] private WorldState[] worldStatePreConditions;
    [SerializeField] private WorldState[] worldStateAfterEffects;

    //For some reason, these can't be declared as [SerializeField] private
    public Dictionary<string , int> Preconditions;
    public Dictionary<string , int> AfterEffects;
    public WorldStates AgentBeliefs;

    public GAction()
    {
        AfterEffects = new Dictionary<string , int>();
        Preconditions = new Dictionary<string , int>();
    }

    public void Awake()
    {
        
        navMeshAgent = GetComponent<NavMeshAgent>(); //This may not be necessary as you have SerializedFiled but leave it for now

        if(worldStatePreConditions != null)
        {
            foreach(WorldState ws in worldStatePreConditions)
            {
                Preconditions.Add(ws.key , ws.value);
            }
        }

        if(worldStateAfterEffects != null)
        {
            foreach(WorldState ws in worldStateAfterEffects)
            {
                AfterEffects.Add(ws.key , ws.value);
            }
        }
    }

    public bool IsAchievable()
    {
        return true;
    }

    public bool IsAchievableGiven(Dictionary<string , int> conditions)
    {
        foreach(KeyValuePair<string , int> pair in Preconditions)
        {
            if(!conditions.ContainsKey(pair.Key))
            {
                return false;
            }
        }

        return true;
    }

    public abstract bool PrePerform(); 
    public abstract bool PostPerform();
}
