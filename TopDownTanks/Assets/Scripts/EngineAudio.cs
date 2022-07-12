using UnityEngine;

public class EngineAudio : MonoBehaviour
{
    #region Variables
    
    private AudioSource _audioSource;

    [SerializeField] private float currentVolume;

    public float DeltaVolume = 0.01f;
    public float MaxVolume = 0.10f;
    public float MinVolume = 0.05f;

    #endregion

    #region Functions
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        currentVolume = MinVolume;
    }

    private void Start()
    {
        _audioSource.volume = currentVolume;
    }

    public void ControlEngineVolume(float tankMoveSpeed)
    {
        if(tankMoveSpeed > 0)
        {
            if(currentVolume < MaxVolume)
            {
                currentVolume += DeltaVolume * Time.deltaTime;
            }
        }
        else
        {
            if(currentVolume > MinVolume)
            {
                currentVolume -= DeltaVolume * Time.deltaTime;
            }
        }

        currentVolume = Mathf.Clamp(currentVolume , MinVolume , MaxVolume);
        _audioSource.volume = currentVolume;
    }
    
    #endregion
}