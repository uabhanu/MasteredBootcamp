using System.Collections.Generic;
using UnityEngine;

namespace Characters.AI
{
    public class PatrolPath : MonoBehaviour
    {
        #region Variable Declarations
        
        public int Length { get => PatrolPoints.Count; }
        public List<Transform> PatrolPoints;
        
        [Header("Gizmos Parameters")]
        public Color LineColour = Color.magenta;
        public Color PointColour = Color.blue;
        public float PointSize = 0.3f;

        #endregion
        
        #region Structs
        
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

            for(int i = PatrolPoints.Count - 1; i > 0; i--)
            {
                if(PatrolPoints[i] == null)
                {
                    return;
                }

                Gizmos.color = PointColour;
                Gizmos.DrawSphere(PatrolPoints[i].position , PointSize);

                if(PatrolPoints.Count == 1 || i == 0)
                {
                    return;
                }

                Gizmos.color = LineColour;
                Gizmos.DrawLine(PatrolPoints[i].position , PatrolPoints[i - 1].position);

                if(PatrolPoints.Count > 2 && i == PatrolPoints.Count - 1)
                {
                    Gizmos.DrawLine(PatrolPoints[i].position , PatrolPoints[0].position);
                }
            }
        }

        public PathPoint GetClosestPathPoint(Vector2 tankPos)
        {
            var minDistance = float.MaxValue;
            var index = -1;

            for(int i = 0; i < PatrolPoints.Count; i++)
            {
                var tempDistance = Vector2.Distance(tankPos , PatrolPoints[i].position);

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
            return new PathPoint { Index = newIndex , Position = PatrolPoints[newIndex].position };
        }

        #endregion
    }
}
