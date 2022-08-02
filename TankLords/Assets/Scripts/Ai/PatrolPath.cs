using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class PatrolPath : MonoBehaviour
    {
        #region Variables

        [Header("Gizmos Parameters")]
        public Color PointsColor = Color.blue;
        public Color LineColor = Color.magenta;
        public float PointSize = 0.3f;
        
        public List<Transform> PatrolPoints = new List<Transform>();
        
        public struct PathPoint
        {
            public int Index;
            public Vector2 Position;
        }
        
        #endregion
        
        #region Functions

        private void OnDrawGizmos()
        {
            if(PatrolPoints.Count == 0)
            {
                return;
            }

            for(int i = PatrolPoints.Count - 1; i >= 0; i--)
            {
                if(i == -1 || PatrolPoints[i] == null)
                {
                    return;
                }

                Gizmos.color = PointsColor;
                Gizmos.DrawSphere(PatrolPoints[i].position , PointSize);

                if(PatrolPoints.Count == 1 || i == 0)
                {
                    return;
                }

                Gizmos.color = LineColor;
                Gizmos.DrawLine(PatrolPoints[i].position , PatrolPoints[i - 1].position);

                if(PatrolPoints.Count >= 2 && i == PatrolPoints.Count - 1)
                {
                    Gizmos.DrawLine(PatrolPoints[i].position , PatrolPoints[0].position);
                }
            }
        }

        public PathPoint GetClosestPathPoint(Vector2 tankPosition)
        {
            var index = -1;
            var minDistance = float.MaxValue;

            for(int i = 0; i < PatrolPoints.Count; i++)
            {
                var tempDistance = Vector2.Distance(tankPosition , PatrolPoints[i].position);

                if(tempDistance < minDistance)
                {
                    minDistance = tempDistance;
                    index = i;
                }
            }

            return new PathPoint { Index = index , Position = PatrolPoints[index].position };
        }

        public PathPoint GetNextPathPoint(int index)
        {
            var newIndex = index + 1 >= PatrolPoints.Count ? 0 : index + 1;
            return  new PathPoint { Index = newIndex , Position = PatrolPoints[newIndex].position };
        }

        public int Length
        {
            get => PatrolPoints.Count;
        }
        
        #endregion
    }
}
