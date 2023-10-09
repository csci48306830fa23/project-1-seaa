using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartStand : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;
        Color[] colors = new Color[vertices.Length];

        Color currentColor = Color.white;  // Starting with white

        for (int i = 0; i < triangles.Length; i += 3)
        {
            // Assign the current color to each vertex of the triangle
            colors[triangles[i]] = currentColor;
            colors[triangles[i + 1]] = currentColor;
            colors[triangles[i + 2]] = currentColor;

            // Alternate the color for the next triangle
            currentColor = (currentColor == Color.white) ? Color.black : Color.white;
        }

        mesh.colors = colors;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
