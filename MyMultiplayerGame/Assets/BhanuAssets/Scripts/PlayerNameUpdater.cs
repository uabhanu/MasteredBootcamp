using BhanuAssets.Scripts.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class PlayerNameUpdater : MonoBehaviour
    {
        [SerializeField] private ChatData chatData;
        [SerializeField] private GameObject playerNamePlaceHolderObj;
        [SerializeField] private TMP_Text playerNameTMP;
        
        private void Awake()
        {
            playerNameTMP.text = chatData.PlayerName;
            playerNamePlaceHolderObj.SetActive(false);
        }
    }
}
