using System;
using System.Collections.Generic;
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
    private bool _isDone = false;
    
    public GameObject finishObj;
    public GameObject pathMarkerObj;
    public GameObject startObj;

    private List<PathMarker> _closed = new List<PathMarker>();
    private List<PathMarker> _opened = new List<PathMarker>();

    public Maze maze;
    
    public Material closeMaterial;
    public Material openMaterial;

    private PathMarker _finishNode;
    private PathMarker _startNode;

    private void BeginSearch()
    {
        _isDone = true;
        RemoveAllMarkers();

        var locations = new List<MapLocation>();

        for(var z = 1; z < maze.depth - 1; z++)
        {
            for(var x = 1; x < maze.width - 1; x++)
            {
                if(maze.Map[x , z] != 1)
                {
                    locations.Add(new MapLocation(x , z));
                }
            }   
        }
        
        locations.Shuffle();

        var startLocation = new Vector3(locations[0].x * maze.scale , 0 , locations[0].z * maze.scale);
        _startNode = new PathMarker(0 , 0 , 0 , Instantiate(startObj , startLocation , Quaternion.identity) , new MapLocation(locations[0].x , locations[0].z) , null);
        
        var finishLocation = new Vector3(locations[1].x * maze.scale , 0 , locations[1].z * maze.scale);
        _finishNode = new PathMarker(0 , 0 , 0 , Instantiate(finishObj , finishLocation , Quaternion.identity) , new MapLocation(locations[1].x , locations[1].z) , null);
    }

    private static void RemoveAllMarkers()
    {
        var markerObjs = GameObject.FindGameObjectsWithTag("Marker");

        foreach(var markerObj in markerObjs)
        {
            Destroy(markerObj);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            BeginSearch();
        }
    }
}
