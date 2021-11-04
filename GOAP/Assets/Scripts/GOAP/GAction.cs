using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// As Penny instructed, this is declared as an abstract class so this can be inherited later
namespace GOAP
{
    public abstract class GAction : MonoBehaviour
    {
        [SerializeField] private WorldState[] worldStatePreConditions;
        [SerializeField] private WorldState[] worldStateAfterEffects;

        public bool Running = false;
        public float Cost = 1.0f;
        public float Duration = 0.0f;
        public GameObject TargetObj;
        //public GameObject targetTagObj; //Not sure why Penny declared this as GameObject so declared this as string below instead
        public NavMeshAgent NavMeshAgent;
        public string ActionName;
        public string TargetObjTag;
    
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
            NavMeshAgent = GetComponent<NavMeshAgent>();

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
}
