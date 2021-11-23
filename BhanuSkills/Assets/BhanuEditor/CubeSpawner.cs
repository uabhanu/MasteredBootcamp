using UnityEditor;
using UnityEngine;

namespace BhanuEditor
{
    public class CubeSpawner : EditorWindow
    {
        [SerializeField] private GameObject bhanuCubePrefab;
        private void OnGUI()
        {
            GUILayout.Label("Spawn Bhanu Cube" , EditorStyles.boldLabel);

            if(GUILayout.Button("Spawn"))
            {
                Instantiate(bhanuCubePrefab);
            }
        }

        [MenuItem("Window/Bhanu Cube")]
        public static void ShowWindow()
        {
            GetWindow<CubeSpawner>("Bhanu Cube");
        }
    }
}

