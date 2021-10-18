using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    public sealed class GWorld 
    {
        private static readonly GWorld instance = new GWorld();
        private static Queue<GameObject> _patientObjs;
        private static WorldStates _world;

        static GWorld()
        {
            _patientObjs = new Queue<GameObject>();
            _world = new WorldStates();
        }

        private GWorld() { }

        public static GWorld Instance 
        {
            get { return instance; }
        }

        public WorldStates GetWorld() 
        {
            return _world;
        }

        public void AddPatient(GameObject patientObj)
        {
            _patientObjs.Enqueue(patientObj);
        }

        public GameObject RemovePatient()
        {
            if(_patientObjs.Count == 0)
            {
                return null;
            }

            return _patientObjs.Dequeue();
        }
    }
}
