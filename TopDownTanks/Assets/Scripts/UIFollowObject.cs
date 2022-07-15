using UnityEngine;

public class UIFollowObject : MonoBehaviour
{
    #region Variables
    
    private RectTransform _rectTransform;
    
    [SerializeField] private Transform objectToFollow;
    
    #endregion

    #region Functions
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(objectToFollow != null)
        {
            _rectTransform.anchoredPosition = objectToFollow.localPosition;
        }
    }
    
    #endregion
}
