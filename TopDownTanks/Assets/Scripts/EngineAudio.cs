using UnityEngine;

public class EngineAudio : MonoBehaviour
{
    #region Variables
    
    private AudioSource _audioSource;

    [SerializeField] private float currentVolume;
    [SerializeField] private float deltaVolume = 0.01f;
    [SerializeField] private float maxVolume = 0.10f;
    [SerializeField] private float minVolume = 0.05f;

    #endregion

    #region Functions
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        currentVolume = minVolume;
    }

    private void Start()
    {
        _audioSource.volume = currentVolume;
    }

    public void ControlEngineVolume(float tankMoveSpeed)
    {
        if(tankMoveSpeed > 0)
        {
            if(currentVolume < maxVolume)
            {
                currentVolume += deltaVolume * Time.deltaTime;
            }
        }
        else
        {
            if(currentVolume > minVolume)
            {
                currentVolume -= deltaVolume * Time.deltaTime;
            }
        }

        currentVolume = Mathf.Clamp(currentVolume , minVolume , maxVolume);
        _audioSource.volume = currentVolume;
    }
    
    #endregion
}