using System.Collections;
using UnityEngine;

namespace Util
{
    public class DestroyOnAudioFinishedPlaying : MonoBehaviour
    {
        #region Variables
        
        private AudioSource _audioSource;
        
        #endregion

        #region Functions
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            StartCoroutine(WaitCoroutine());
        }

        private IEnumerator WaitCoroutine()
        {
            yield return new WaitForSeconds(_audioSource.clip.length);
            Destroy(gameObject);
        }
        
        #endregion
    }
}
