using Cinemachine;
using ScriptableObjects;
using StarterAssets;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private StarterAssetsInputs _starterAssetsInputs;
    
    [SerializeField] private CinemachineVirtualCamera cmAimCamera;
    [SerializeField] private PlayerData playerData;

    private void Awake()
    {
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        if(_starterAssetsInputs.aim)
        {
            cmAimCamera.gameObject.SetActive(true);
        }
        else
        {
            cmAimCamera.gameObject.SetActive(false);
        }
    }
}
