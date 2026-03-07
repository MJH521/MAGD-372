using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    public int xSize = 20;
    public int zSize = 20;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        StartCoroutine(CreateShape()); // 1
/*        CreateShape();*/ // 2
        UpdateMesh();
    }

    // 1
    private void Update()
    {
        UpdateMesh();
    }

    IEnumerator CreateShape() // 1
/*    void CreateShape()*/ // 2
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;

                yield return new WaitForSeconds(.01f); // 1
            }
            vert++;
        }

        // Video 1
        //vertices = new Vector3[]
        //{
        //    new Vector3 (0, 0, 0),
        //    new Vector3 (0, 0, 1),
        //    new Vector3 (1, 0, 0),
        //    new Vector3(1, 0, 1)
        //};

        //triangles = new int[]
        //{
        //    0, 1, 2,
        //    1, 3, 2
        //};
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    // 1
    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }

        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }

    // 1 = Comment or uncomment for Movement Showcase
    // 2 = Comment or uncomment for Static Showcase
}