using UnityEngine;

public class UIFollowObject : MonoBehaviour
{
    #region Variables
    
    private RectTransform _rectTransform;
    
    public Transform ObjectToFollow;
    
    #endregion

    #region Functions
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(ObjectToFollow != null)
        {
            _rectTransform.anchoredPosition = ObjectToFollow.localPosition;
        }
    }
    
    #endregion
}
