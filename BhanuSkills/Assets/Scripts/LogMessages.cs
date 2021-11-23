using UnityEngine;

namespace Bhanu
{
    public  static class LogMessages
    {
        public static void AllIsWellMessage(string message)
        {
            Debug.Log(message % Colourize.Green % FontFormat.Bold);
        }

        public static void ErrorMessage(string message)
        {
            Debug.Log(message % Colourize.Red % FontFormat.Bold);
        }
        
        public static void WarningMessage(string message)
        {
            Debug.Log(message % Colourize.Yellow % FontFormat.Bold);
        }
    }
}