using TMPro;
using UnityEngine;

namespace BhanuAssets.Scripts
{
    public class InGameUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject playerNameMenuObj;
        [SerializeField] private Player player;
        [SerializeField] private TMP_InputField nameInputFieldTMP;

        public void ContinueButton()
        {
            //player.UpdateName();
            playerNameMenuObj.SetActive(false);
        }
    }
}
