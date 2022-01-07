using System;
using Bhanu;
using Events;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class Socket : MonoBehaviour
    {
        [SerializeField] private GameObject[] totalPipes;
        [SerializeField] private bool allPipesInTheSocket;
        [SerializeField] private bool _pipeInTheSocket = false;
        
        private void Start()
        {
            PipesInTheSocketReset();
            SubscribeToEvents();
            totalPipes = GameObject.FindGameObjectsWithTag("Pipe");
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        private void Update()
        {
            if(AllPipesInTheSocket())
            {
                EventsManager.InvokeEvent(BhanuEvent.AllSocketsFilled);
            }
        }

        public bool AllPipesInTheSocket()
        {
            for(int i = 0; i < totalPipes.Length; i++)
            {
                allPipesInTheSocket = totalPipes[i].GetComponent<Pipe>().IsInTheSocket;
            }

            return allPipesInTheSocket;
        }

        private void PipesInTheSocketReset()
        {
            _pipeInTheSocket = false;
        }

        private void OnPipeInTheSocket()
        {
            _pipeInTheSocket = true;

            if(AllPipesInTheSocket())
            {
                EventsManager.InvokeEvent(BhanuEvent.AllElectricBoxesCollided);
            }
        }

        private void OnPipeNoLongerInTheSocket()
        {
            _pipeInTheSocket = false;
        }
        
        private void SubscribeToEvents()
        {
            EventsManager.SubscribeToEvent(BhanuEvent.PipeInTheSocket , OnPipeInTheSocket);
            EventsManager.SubscribeToEvent(BhanuEvent.PipeNoLongerInTheSocket , OnPipeNoLongerInTheSocket);
        }
        
        private void UnsubscribeFromEvents()
        {
            EventsManager.UnsubscribeFromEvent(BhanuEvent.PipeInTheSocket , OnPipeInTheSocket);
            EventsManager.UnsubscribeFromEvent(BhanuEvent.PipeNoLongerInTheSocket , OnPipeNoLongerInTheSocket);
        }
    }
}
