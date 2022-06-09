using UnityEngine;

public class LookAtAndFollow : MonoBehaviour
{
    [SerializeField] private float dotProductResult;
    [SerializeField] private float drawSphereRadius;
    [SerializeField] [Range(0f , 100f)] private float offset = 1f; //By using this, you will get a smooth effect similar to Vector.Lerp
    [SerializeField] private Transform aTransform;
    [SerializeField] private Transform bTransform;
    
    private void OnDrawGizmos()
    {
        MyLookAt();
        //UnityLookAt();
    }

    private float DotProduct()
    {
        Vector2 aPos = aTransform.position;
        Vector2 bPos = bTransform.position;
        
        dotProductResult = (aPos.x * bPos.x) + (aPos.y * aPos.y);
        return dotProductResult;
    }

    private void MyLookAt()
    {
        Vector2 aPos = aTransform.position;
        Vector2 bPos = bTransform.position;

        Vector2 aToB = bPos - aPos;
        Vector2 aToBDirection = aToB.normalized; //Normalized or Unit Vector. The reason to do this is, I care only about direction and not the length of the vector
        Vector2 offsetVector = aToBDirection * offset;
        
        Gizmos.DrawLine(aPos , aPos + aToBDirection); //We need to draw the starting point from a and also the end point from a, otherwise, it will be drawn from or to the origin

        if(DotProduct() > 0) //Note that this logic is 'a' point of view and the result will be opposite 'b' point of view
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(aPos + offsetVector , drawSphereRadius); //Draw sphere at offset vector and adding a to it because we need the position relative to a   
        }
        else
        {
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(aPos + offsetVector , drawSphereRadius); //Draw sphere at offset vector and adding a to it because we need the position relative to a
        }
    }

    private void UnityLookAt()
    {
        Vector2 aPos = aTransform.position;
        Vector2 bPos = bTransform.position;
        
        Vector2 aToB = bPos - aPos;
        Vector2 aToBDirection = aToB.normalized;
        
        transform.LookAt(bPos);
        Gizmos.DrawLine(aPos , aPos + aToBDirection);
    }
}
