using UnityEngine;

namespace Bhanu
{
    public class Colourize
    {
        public static Colourize Red = new Colourize(Color.red);
        public static Colourize Yellow = new Colourize(Color.yellow);
        public static Colourize Green = new Colourize(Color.green);
        public static Colourize Blue = new Colourize(Color.blue);
        public static Colourize Cyan = new Colourize(Color.cyan);
        public static Colourize Magenta = new Colourize(Color.magenta);

        private readonly string _prefix;

        private const string _suffix = "</color>";
        
        private Colourize(Color color)
        {
            _prefix = $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>";
        }

        public static string operator % (string text , Colourize color)
        {
            return color._prefix + text + _suffix;
        }
    }
}