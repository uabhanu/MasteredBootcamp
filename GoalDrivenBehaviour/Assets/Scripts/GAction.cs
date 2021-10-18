using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GAction : MonoBehaviour
{
    public string actionName = "Action";
    public float cost = 1.0f;
    public GameObject target;
    public GameObject targetTag;
    public float duration = 0;
    public WorldState[] preConditions;
    public WorldState[] afterEffects;
    public NavMeshAgent agent;

    public Dictionary<string , int> Preconditions;
    public Dictionary<string , int> Effects;

    public WorldStates AgentBeliefs;

    public bool running = false;

    public GAction()
    {
        Preconditions = new Dictionary<string, int>();
        Effects = new Dictionary<string, int>();
    }

    public void Awake()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();

        if(preConditions != null)
            
            for(var index = 0; index < preConditions.Length; index++)
            {
                var w = preConditions[index];
                Preconditions.Add(w.key , w.value);
            }

        if(afterEffects != null)

            for(var index = 0; index < afterEffects.Length; index++)
            {
                var w = afterEffects[index];
                Effects.Add(w.key , w.value);
            }
    }

    public bool IsAchievable()
    {
        return true;
    }

    public bool IsAchievableGiven(Dictionary<string , int> conditions)
    {
        foreach(var p in Preconditions)
        {
            if(!conditions.ContainsKey(p.Key)) return false;
        }
        
        return true;
    }

    public abstract bool PrePerform();
    public abstract bool PostPerform();
}
