using UnityEngine;

namespace Bhanu
{
    public  static class BhanuLogMessages
    {
        public static void NoPlayerExistsMessage()
        {
            Debug.Log("Sir Bhanu, No Player in the Scene" % Colourize.Red % FontFormat.Bold);
        }
        
        public static void NoPlayerToMoveMessage()
        {
            Debug.Log("Sir Bhanu, there is no player in the scene to move around" % Colourize.Red % FontFormat.Bold);
        }

        public static void NoPlayerToRequestChangePositionMessage()
        {
            Debug.Log("Sir Bhanu, there is no player in the scene to request change position" % Colourize.Red % FontFormat.Bold);
        }

        public static void PlayerExistsMessage()
        {
            Debug.Log("Sir Bhanu, Player does exist :)" % Colourize.Green % FontFormat.Bold);
        }

        public static void IamThePlayer()
        {
            Debug.Log("Sir Bhanu, I am the Player :)" % Colourize.Green % FontFormat.Bold);
        }
    }
}
