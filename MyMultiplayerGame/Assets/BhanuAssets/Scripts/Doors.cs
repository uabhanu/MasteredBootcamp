using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class Doors : MonoBehaviour
    {
        private bool doorOpen = false;
        
        [SerializeField] private Animator anim;

        public void OpenDoors()
        {
            doorOpen = true;
            anim.SetBool("DoorTriggered" , doorOpen);
        }
    }
}
