using Bhanu.ScriptableObjects;
using UnityEngine;

namespace Bhanu
{
    public class Pellet : MonoBehaviour
    {
        [SerializeField] private PelletData pelletData;
        private void Update()
        {
            transform.Translate(Vector3.forward * pelletData.MoveSpeed * Time.deltaTime);

            if(transform.position.z >= 60f)
            {
                HelloWorldPlayer.numberOfPellets--;
                Destroy(gameObject);
            }
        }
    }
}
