using UnityEngine;

namespace Utils
{
    public class DestroyIfDisabled : MonoBehaviour
    {
        private bool _selfDestructionEnabled = false;

        public bool SelfDestructionEnabled
        {
            get => _selfDestructionEnabled;
            set => _selfDestructionEnabled = value;
        }

        private void OnDisable()
        {
            if(SelfDestructionEnabled)
            {
                Destroy(gameObject);
            }
        }
    }
}
