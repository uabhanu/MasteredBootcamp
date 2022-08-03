using UnityEngine;

public class EngineAudio : MonoBehaviour
{
    #region Variables
    
    private AudioSource _audioSource;

    [SerializeField] private float currentVolume;

    public float MaxVolume = 0.10f;
    public float MinVolume = 0.05f;
    public float VolumeIncrease = 0.01f;
    
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

    public void ControlEngineVolume(float speed)
    {
        if(speed > 0)
        {
            if(currentVolume < MaxVolume)
            {
                currentVolume += VolumeIncrease * Time.deltaTime;
            }
        }
        else
        {
            if(currentVolume > MinVolume)
            {
                currentVolume -= VolumeIncrease * Time.deltaTime;
            }
        }

        currentVolume = Mathf.Clamp(currentVolume , MinVolume , MaxVolume);
        _audioSource.volume = currentVolume;
    }

    #endregion
}
