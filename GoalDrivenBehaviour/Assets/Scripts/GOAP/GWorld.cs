using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    public sealed class GWorld 
    {
        private static readonly GWorld instance = new GWorld();
        private static Queue<GameObject> _cubicleObjs;
        private static Queue<GameObject> _patientObjs;
        private static WorldStates _world;

        static GWorld()
        {
            _cubicleObjs = new Queue<GameObject>();
            _patientObjs = new Queue<GameObject>();
            _world = new WorldStates();

            var cubes = GameObject.FindGameObjectsWithTag("Cubicle");

            for(var index = 0; index < cubes.Length; index++)
            {
                var cube = cubes[index];
                _cubicleObjs.Enqueue(cube);
            }

            if(cubes.Length > 0)
            {
                _world.ModifyState("FreeCubicle" , cubes.Length);
            }
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

        public void AddCubicle(GameObject cubicleObj)
        {
            _patientObjs.Enqueue(cubicleObj);
        }

        public void AddPatient(GameObject patientObj)
        {
            _patientObjs.Enqueue(patientObj);
        }
        
        public GameObject RemoveCubicle()
        {
            if(_cubicleObjs.Count == 0)
            {
                return null;
            }

            return _cubicleObjs.Dequeue();
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
