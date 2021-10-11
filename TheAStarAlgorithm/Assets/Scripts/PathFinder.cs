using System.Linq;
using UnityEngine;

public class PathMarker
{
    public float F;
    public float G;
    public float H;

    public GameObject MarkerObj;
    
    public MapLocation MapLocation;

    public PathMarker ParentPathMarker;

    public PathMarker(float f , float g , float h , GameObject mObj , MapLocation mLocation , PathMarker pMarker)
    {
        F = f;
        G = g;
        H = h;

        MarkerObj = mObj;

        MapLocation = mLocation;

        ParentPathMarker = pMarker;
    }
    
    public override bool Equals(object obj)
    {
        if((obj == null) || GetType() == obj.GetType())
        {
            return false;
        }
        else
        {
            return MapLocation.Equals(((PathMarker) obj).MapLocation);
        } 
    }
    
    public override int GetHashCode()
    {
        return 0;
    }

}

public class PathFinder : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}
