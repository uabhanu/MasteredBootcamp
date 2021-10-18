using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    public class GInventory
    {
        private List<GameObject> _gameObjects = new List<GameObject>();

        public void AddGameObject(GameObject gObj)
        {
            _gameObjects.Add(gObj);
        }

        public GameObject FindGameObjectWithTag(string tag)
        {
            for(var index = 0; index < _gameObjects.Count; index++)
            {
                var gObj = _gameObjects[index];
            
                if(gObj.CompareTag(tag))
                {
                    return gObj;
                }
            }

            return null;
        }

        public void RemoveGameObject(GameObject gObj)
        {
            var indexToRemove = -1;

            foreach(var go in _gameObjects)
            {
                indexToRemove++;

                if(go == gObj)
                {
                    break;
                }
            }

            if(indexToRemove >= -1)
            {
                _gameObjects.RemoveAt(indexToRemove);
            }
        }
    }
}
