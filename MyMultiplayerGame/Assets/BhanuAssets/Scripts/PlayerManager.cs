using Photon.Pun;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class PlayerManager : MonoBehaviourPunCallbacks , IPunObservable
    {
        [SerializeField] private PhotonView photonView;
        
        public static GameObject LocalPlayerInstance;

        private void Awake()
        {
            if(photonView.IsMine)
            {
                LocalPlayerInstance = gameObject;
            }
            
            DontDestroyOnLoad(gameObject);   
            
            #if UNITY_5_4_OR_NEWER
                UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
            #endif
        }

        #if UNITY_5_4_OR_NEWER
        
            public override void OnDisable()
            {
                base.OnDisable();
                UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
            }
        
        #endif

        #if UNITY_5_4_OR_NEWER
        
            private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene , UnityEngine.SceneManagement.LoadSceneMode loadingMode)
            {
                CalledOnLevelWasLoaded(scene.buildIndex);
            }
            
        #endif

        #if UNITY_5_4_OR_NEWER
        
            private void OnLevelWasLoaded(int level)
            {
                CalledOnLevelWasLoaded(level);
            }
        
        #endif

        private void CalledOnLevelWasLoaded(int level)
        {
            if(Physics.Raycast(transform.position , -Vector3.up , 5f))
            {
                transform.position = new Vector3(0f , 5f , 0f);
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream , PhotonMessageInfo messageInfo)
        {
            //LogMessages.AllIsWellMessage("PlayerManager : Stream is " + stream + " Message is : " + messageInfo);
        }
    }
}
