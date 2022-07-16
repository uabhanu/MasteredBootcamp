using UnityEngine;

namespace DataSo
{
    [CreateAssetMenu(fileName = "TrackMarksSo" , menuName = "TankData/TrackMarksSo")]
    public class TrackMarksSo : ScriptableObject
    {
        public float TrackDistance = 0.2f;
        public GameObject TrackMarksPrefab;
        public int ObjectPoolSize = 50;
    }
}
