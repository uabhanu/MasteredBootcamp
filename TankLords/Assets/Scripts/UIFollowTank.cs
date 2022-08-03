using UnityEngine;

public class UIFollowTank : MonoBehaviour
{
    private RectTransform _rectTransform;
    
    public Transform ObjectToFollow;

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
}
