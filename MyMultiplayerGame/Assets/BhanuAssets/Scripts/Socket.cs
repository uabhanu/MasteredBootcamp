using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class Socket : MonoBehaviour
    {
        private bool _pipeInside;

        public bool PipeInside
        {
            get => _pipeInside;
            set => _pipeInside = value;
        }
    }
}
