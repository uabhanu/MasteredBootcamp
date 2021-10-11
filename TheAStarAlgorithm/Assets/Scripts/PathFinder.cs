using System;
using System.Collections.Generic;
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
    private PathMarker _lastPos;
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
        
        _closed.Clear();
        _opened.Clear();
        
        _opened.Add(_startNode);
        _lastPos = _startNode;
    }

    private bool IsClosed(MapLocation marker)
    {
        for(var index = 0; index < _closed.Count; index++)
        {
            var pm = _closed[index];
            if(pm.MapLocation.Equals(marker)) return true;
        }

        return false;
    }

    private void Search(PathMarker currentNode)
    {
        if(!Equals(currentNode , _finishNode)) return;
        _isDone = true;

        for(var index = 0; index < maze.Directions.Count; index++)
        {
            var dir = maze.Directions[index];
            var neighbour = dir + currentNode.MapLocation;

            if(maze.Map[neighbour.x , neighbour.z] == 1) continue;
            if(neighbour.x < 1 || neighbour.x >= maze.width || neighbour.z < 1 || neighbour.z >= maze.depth) continue;
            if(IsClosed(neighbour)) continue;
            
            var G = Vector2.Distance(currentNode.MapLocation.ToVector2() , neighbour.ToVector2()) + currentNode.G;
            var H = Vector2.Distance(neighbour.ToVector2() , _finishNode.MapLocation.ToVector2());
            
            var F = G + H;

            var pathObj = Instantiate(pathMarkerObj , new Vector3(neighbour.x * maze.scale , 0 , neighbour.z * maze.scale) , Quaternion.identity);
            
            var values = pathObj.GetComponentsInChildren<TextMesh>();
            values[2].text = "F : " + F.ToString("0.00");
            values[0].text = "G : " + G.ToString("0.00");
            values[1].text = "H : " + H.ToString("0.00");
            
            if(!UpdateMarker(neighbour , F , G , H , currentNode)) _opened.Add(new PathMarker(F , G , H , pathObj , neighbour , currentNode));
        }

        _opened = _opened.OrderBy(marker => marker.F).ToList<PathMarker>();
        var pm = (PathMarker) _opened.ElementAt(0);
        _closed.Add(pm);

        _opened.RemoveAt(0);

        pm.MarkerObj.GetComponent<Renderer>().material = closeMaterial;
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
        
        if(Input.GetKeyDown(KeyCode.C))
        {
            Search(_lastPos);
        }
    }

    private bool UpdateMarker(MapLocation mapLocation , float f , float g , float h , PathMarker pathMarker)
    {
        for(var index = 0; index < _opened.Count; index++)
        {
            var p = _opened[index];
            
            if(p.MapLocation.Equals(mapLocation))
            {
                p.F = f;
                p.G = g;
                p.H = h;

                p.ParentPathMarker = pathMarker;

                return true;
            }
        }

        return false;
    }
}
