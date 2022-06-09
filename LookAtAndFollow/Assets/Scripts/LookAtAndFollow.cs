using UnityEngine;

public class LookAtAndFollow : MonoBehaviour
{
    [SerializeField] private float distanceAToB;
    [SerializeField] private Transform aTransform;
    [SerializeField] private Transform bTransform;
    
    private void OnDrawGizmos()
    {
        MyLookAt();
        //UnityLookAt();
    }

    private void MyLookAt()
    {
        Vector2 aPos = aTransform.position;
        Vector2 bPos = bTransform.position;

        Vector2 aToB = bPos - aPos;
        Vector2 aToBDirection = aToB.normalized; //Normalized or Unit Vector. The reason to do this is, I care only about direction and not the length of the vector
        
        //This does get the distance but when I deal with this situation, I prefer using Unity's Vector2.Distance because this is explicit and hence not confusing
        //If I just want the length of the Vector, I would use one of the two with the first line as more preference as it is less lines of code and Vector3.Distance here is confusing so it really depends on the context.
        //distanceAToB = (aPos - bPos).magnitude;
        distanceAToB = Mathf.Sqrt((aPos.x - bPos.x) * (aPos.x - bPos.x) + (aPos.y - bPos.y) * (aPos.y - bPos.y));
        Gizmos.DrawLine(aPos , aPos + aToBDirection);
    }

    private void UnityLookAt()
    {
        Vector2 aPos = aTransform.position;
        Vector2 bPos = bTransform.position;
        distanceAToB = Vector2.Distance(aPos , bPos);
        Gizmos.DrawLine(aPos , bPos);
    }
}
