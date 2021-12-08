using BhanuAssets.Scripts.ScriptableObjects;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class Player : MonoBehaviour
    {
        private Material materialToUse;
        private MeshRenderer playerRenderer;
        
        [SerializeField] private PhotonView photonView;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private TMP_Text nameTMP;

        private void Start()
        {
            SelectRenderer();
        }

        private void Update()
        {
            Move();
        }

        [PunRPC]
        private void Move()
        {
            if(photonView.IsMine)
            {
                if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                {
                    MoveForward();
                }
            
                if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    MoveBackward();
                }
            
                if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    MoveLeft();
                }
            
                if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    MoveRight();
                }   
            }
            // else
            // {
            //     if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            //     {
            //         photonView.RPC("MoveForward" , RpcTarget.MasterClient);
            //     }
            //
            //     if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            //     {
            //         photonView.RPC("MoveBackward" , RpcTarget.MasterClient);
            //     }
            //
            //     if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            //     {
            //         photonView.RPC("MoveLeft" , RpcTarget.MasterClient);
            //     }
            //
            //     if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            //     {
            //         photonView.RPC("MoveRight" , RpcTarget.MasterClient);
            //     }
            // }
        }

        [PunRPC]
        private void MoveBackward()
        {
            transform.Translate(Vector3.back * playerData.MoveSpeed * Time.deltaTime);
        }
        
        [PunRPC]
        private void MoveForward()
        {
            transform.Translate(Vector3.forward * playerData.MoveSpeed * Time.deltaTime);
        }
        
        [PunRPC]
        private void MoveLeft()
        {
            transform.Translate(Vector3.left * playerData.MoveSpeed * Time.deltaTime);
        }
        
        [PunRPC]
        private void MoveRight()
        {
            transform.Translate(Vector3.right * playerData.MoveSpeed * Time.deltaTime);
        }

        private void SelectRenderer()
        {
            playerRenderer = GetComponent<MeshRenderer>();
            
            if(photonView.IsMine)
            {
                materialToUse = playerData.LocalMaterial;
                playerRenderer.material = materialToUse;
            }
            else
            {
                materialToUse = playerData.RemoteMaterial;
                playerRenderer.material = materialToUse;
            }
        }

        public void UpdateName()
        {
            GameObject nameInput = GameObject.Find("NameInputField (TMP)");
            nameTMP.text = nameInput.GetComponent<TMP_InputField>().text;
        }
    }
}
