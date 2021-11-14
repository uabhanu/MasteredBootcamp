using UnityEngine;

namespace Bhanu
{
    public class GameManager : MonoBehaviour
    {
        private static GameObject playerObj;
        private void Start()
        {
            InvokeRepeating("GetPlayer" , 1f , 1f);
        }

        private void GetPlayer()
        {
            playerObj = GameObject.FindWithTag("Player");
        }
        
        public static bool PlayerExists()
        {
            if(playerObj != null)
            {
                return true;
            }
            
            return false;
        }
    }
}
