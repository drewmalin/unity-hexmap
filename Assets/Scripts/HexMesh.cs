using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{

    Mesh hexMesh;
    MeshCollider meshCollider;
    List<Vector3> vertices;
    List<int> triangles;

    void Awake()
    {
        GetComponent<MeshFilter>().mesh = this.hexMesh = new Mesh();
        this.meshCollider = this.gameObject.AddComponent<MeshCollider>();
        this.hexMesh.name = "Hex Mesh";
        this.vertices = new List<Vector3>();
        this.triangles = new List<int>();
    }

    /*
     * AddHexes resets the mesh, and begins drawing triangles to represent the provided HexCell locations.
     */
    public void AddHexes(HexCell[] cells)
    {
        this.hexMesh.Clear();
        this.vertices.Clear();
        this.triangles.Clear();
        for (int i = 0; i < cells.Length; i++)
        {
            AddHex(cells[i]);
        }
        this.hexMesh.vertices = this.vertices.ToArray();
        this.hexMesh.triangles = this.triangles.ToArray();
        this.hexMesh.RecalculateNormals();
        this.meshCollider.sharedMesh = this.hexMesh;
    }

    /*
     * Each Hex should be represented by 6 triangles. Each triangle will share a point (the center of the
     * hex). Using the offset values from HexUtils, draw each triangle and store the vertex and triangle count
     * for use by the native mesh.
     */
    void AddHex(HexCell cell)
    {
        Vector3 center = cell.transform.localPosition;
        for (int i = 0; i < 6; i++)
        {
            AddTriangle(
                center,
                center + HexUtils.corners[i],
                center + HexUtils.corners[i + 1]
            );
        }
    }

    private void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        int count = this.vertices.Count;

        this.vertices.Add(v1);
        this.vertices.Add(v2);
        this.vertices.Add(v3);

        this.triangles.Add(count);
        this.triangles.Add(count + 1);
        this.triangles.Add(count + 2);
    }
}
