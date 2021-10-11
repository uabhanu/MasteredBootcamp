using System.Collections.Generic;
using UnityEngine;

public class MapLocation       
{
    public int x;
    public int z;

    public MapLocation(int x , int z)
    {
        this.x = x;
        this.z = z;
    }

    public Vector2 ToVector2()
    {
        return new Vector2(x , z);
    }

    public static MapLocation operator + (MapLocation a , MapLocation b) //This is Operator Overloading
       => new MapLocation(a.x + b.x , a.z + b.z);

    public override bool Equals(object obj)
    {
        if((obj == null) || GetType() == obj.GetType())
        {
            return false;
        }
        else
        {
            return x == ((MapLocation) obj).x && z == ((MapLocation) obj).z;
        } 
    }
}

public class Maze : MonoBehaviour
{
    protected readonly List<MapLocation> Directions = new List<MapLocation>() {
                                            new MapLocation(1 ,0),
                                            new MapLocation(0 ,1),
                                            new MapLocation(-1 ,0),
                                            new MapLocation(0 ,-1) };
    public int width = 30; //x length
    public int depth = 30; //z length
    protected byte[,] Map;
    public int scale = 6;
    
    private void Start()
    {
        InitialiseMap();
        Generate();
        DrawMap();
    }

    public int CountAllNeighbours(int x , int z)
    {
        return CountSquareNeighbours(x , z) + CountDiagonalNeighbours(x , z);
    }
    
    private int CountDiagonalNeighbours(int x , int z)
    {
        var count = 0;
        
        if(x <= 0 || x >= width - 1 || z <= 0 || z >= depth - 1) return 5;
        if(Map[x - 1 , z - 1] == 0) count++;
        if(Map[x + 1 , z + 1] == 0) count++;
        if(Map[x - 1 , z + 1] == 0) count++;
        if(Map[x + 1 , z - 1] == 0) count++;
        
        return count;
    }

    protected int CountSquareNeighbours(int x , int z)
    {
        var count = 0;
        
        if(x <= 0 || x >= width - 1 || z <= 0 || z >= depth - 1) return 5;
        
        if(Map[x - 1 , z] == 0) count++;
        if(Map[x + 1 , z] == 0) count++;
        if(Map[x , z + 1] == 0) count++;
        if(Map[x , z - 1] == 0) count++;
        
        return count;
    }
    
    private void DrawMap()
    {
        for(var z = 0; z < depth; z++)
        for(var x = 0; x < width; x++)
        {
            if(Map[x , z] != 1) continue;
                
            var pos = new Vector3(x * scale , 0 , z * scale);
            var wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wall.transform.localScale = new Vector3(scale , scale , scale);
            wall.transform.position = pos;
        }
    }

    protected virtual void Generate()
    {
        for(var z = 0; z < depth; z++)
        for(var x = 0; x < width; x++)
        {
            if(Random.Range(0 ,100) < 50)
                Map[x , z] = 0;     //1 = wall  0 = corridor
        }
    }

    public override int GetHashCode()
    {
        return 0;
    }
    
    private void InitialiseMap()
    {
        Map = new byte[width , depth];
        for(var z = 0; z < depth; z++)
        for(var x = 0; x < width; x++)
        {
            Map[x, z] = 1;     //1 = wall  0 = corridor
        }
    }
}
