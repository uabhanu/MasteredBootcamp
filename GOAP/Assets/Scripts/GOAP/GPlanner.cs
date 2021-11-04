using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    public class Node
    {
        public Dictionary<string , int> State;
        public float Cost;
        public GAction Action;
        public Node ParentNode;

        public Node(Node parent , float cost , Dictionary<string , int> allStates , GAction action)
        {
            ParentNode = parent;
            Cost = cost;
            State = new Dictionary<string , int>(allStates); //Create a copy to avoid pointing back to the same dictionary
            Action = action;
        }
    }
    public class GPlanner
    {
        private List<GAction> ActionSubset(List<GAction> actions , GAction removeAction)
        {
            List<GAction> subset = new List<GAction>();

            foreach(GAction ga in actions)
            {
                if(!ga.Equals(removeAction))
                {
                    subset.Add(ga);
                }
            }

            return subset;
        }
        private bool BuildGraph(Node parent , List<Node> leaves , List<GAction> usableActions , Dictionary<string , int> goal)
        {
            bool foundPath = false;

            foreach(GAction ga in usableActions)
            {
                if(ga.IsAchievableGiven(parent.State))
                {
                    Dictionary<string , int> currentState = new Dictionary<string , int>(parent.State);

                    foreach(KeyValuePair<string , int> effect in ga.AfterEffects)
                    {
                        if(!currentState.ContainsKey(effect.Key))
                        {
                            currentState.Add(effect.Key , effect.Value);
                        }
                    }

                    Node node = new Node(parent , parent.Cost + ga.Cost , currentState , ga);

                    if(GoalAchieved(goal , currentState))
                    {
                        leaves.Add(node);
                        foundPath = true; //I think this needs to be false
                    }
                    else
                    {
                        List<GAction> subset = ActionSubset(usableActions , ga);
                        bool found = BuildGraph(node , leaves , subset , goal);

                        if(found)
                        {
                            foundPath = true;
                        }
                    }
                }
            }

            return foundPath;
        }

        private bool GoalAchieved(Dictionary<string , int> goal , Dictionary<string , int> state)
        {
            foreach(KeyValuePair<string , int> pair in goal)
            {
                if(!state.ContainsKey(pair.Key))
                {
                    return false;
                }
            }

            return true;
        }
    
        public Queue<GAction> PlansQueue(List<GAction> actions , Dictionary<string , int> goal , WorldStates states)
        {
            List<GAction> usableActions = new List<GAction>();

            foreach(GAction ga in actions)
            {
                if(ga.IsAchievable())
                {
                    usableActions.Add(ga);
                }
            }

            List<Node> leaves = new List<Node>();
            Node start = new Node(null , 0 , GWorld.WorldInstance.GetWorld().GetStates() , null);

            bool success = BuildGraph(start , leaves , usableActions , goal);

            if(!success)
            {
                return null;
            }

            Node cheapest = null;

            foreach(Node leaf in leaves)
            {
                if(cheapest == null)
                {
                    cheapest = leaf;
                }
                else
                {
                    if(leaf.Cost < cheapest.Cost)
                    {
                        cheapest = leaf;
                    }
                }
            }

            List<GAction> result = new List<GAction>();
            Node n = cheapest;

            while(n != null)
            {
                if(n.Action != null)
                {
                    result.Insert(0 , n.Action);
                }

                n = n.ParentNode;
            }

            Queue<GAction> queue = new Queue<GAction>();

            foreach(GAction ga in result)
            {
                queue.Enqueue(ga);
            }
        
            Debug.Log("The Plan is : ");

            foreach(GAction ga in queue)
            {
                Debug.Log("Q : " + ga.ActionName);
            }

            return queue;
        }
    }
}