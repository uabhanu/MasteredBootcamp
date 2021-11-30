using TMPro;
using UnityEngine;

public class Wobbler : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices;
    
    [SerializeField] private TMP_Text textMesh;

    private void Update()
    {
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;
        vertices = mesh.vertices;

        for(int i = 0; i < vertices.Length; i++)
        {
            Vector3 offset = Wobble(Time.time + i);
            vertices[i] += offset;
        }

        mesh.vertices = vertices;
        textMesh.canvasRenderer.SetMesh(mesh);
    }

    private Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * 5.5f) , Mathf.Cos(time * 0.8f));
    }
}
