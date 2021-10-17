using UnityEngine;

// TODO Research sealed keyword
public sealed class World
{
    private static readonly World instance = new World();
    private static GameObject[] _hidingSpots;

    static World()
    {
        _hidingSpots = GameObject.FindGameObjectsWithTag("Hide");
    }

    private World() { }

    public static GameObject[] GetHidingSpots()
    {
        return _hidingSpots;
    }

    public static World Instance
    {
        get
        {
            return instance;
        }
    }
}
