using UnityEngine;

public class ObjectGeneratorRandomPositionUtil : MonoBehaviour
{
    #region Variables

    [SerializeField] private Color gizmosColour;
    
    public float Radius;
    public GameObject ObjPrefab;
    
    #endregion

    #region Functions

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColour;
        Gizmos.DrawWireSphere(transform.position , Radius);
    }

    //This is between private and public as only inherited classes can access unlike private where no class can assess and public where every class can access
    protected virtual GameObject GetObject()
    {
        return Instantiate(ObjPrefab);
    }
    
    protected Quaternion Random2DRotation()
    {
        return Quaternion.Euler(0 , 0 , Random.Range(0 , 360));
    }
    
    protected Vector2 GetRandomPosition()
    {
        return Random.insideUnitCircle * Radius + (Vector2)transform.position;
    }

    public void CreateObject()
    {
        Vector2 position = GetRandomPosition();
        GameObject impactObject = GetObject();
        impactObject.transform.position = position;
        impactObject.transform.rotation = Random2DRotation();
    }
    
    #endregion
}
