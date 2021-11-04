using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    [System.Serializable]
    public class WorldState
    {
        public string key;
        public int value;
    }

    public class WorldStates
    {
        [SerializeField] private Dictionary<string , int> _states;

        private void AddState(string key , int value)
        {
            _states.Add(key , value);
        }
    
        private void RemoveState(string key)
        {
            _states.Remove(key);
        }

        public Dictionary<string , int> GetStates()
        {
            return _states;
        }

        public bool HasState(string key)
        {
            return _states.ContainsKey(key);
        }

        public void ModifyState(string key , int value)
        {
            if(_states.ContainsKey(key))
            {
                _states[key] += value;

                if(_states[key] <= 0)
                {
                    RemoveState(key);
                }
                else
                {
                    AddState(key , value);
                    //_states.Add(key , value); //The function above does the same but use this instead if needed
                }
            }
        }

        public void SetState(string key , int value)
        {
            if(_states.ContainsKey(key))
            {
                _states[key] = value;
            }
            else
            {
                _states.Add(key , value);
            }
        }

        public WorldStates()
        {
            _states = new Dictionary<string , int>();
        }
    }
}