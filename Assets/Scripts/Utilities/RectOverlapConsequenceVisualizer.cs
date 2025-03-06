using System;
using UnityEngine;

public class RectOverlapConsequenceVisualizer : MonoBehaviour
{
    public void Initialize(Vector3 center, Vector3 halfExtents, Quaternion rotation)
    {   
        _center = center;
        _halfExtents = halfExtents;
        _rotation = rotation;
    }
    
    private Vector3 _center;
    private Vector3 _halfExtents;
    private Quaternion _rotation;
    
    [SerializeField] private Material _lineMaterial;
    [SerializeField] private Color _color = Color.yellow;

    private void OnRenderObject()
    {
        if (_lineMaterial == null)
            return;
    
        _lineMaterial.SetPass(0);
        
        GL.PushMatrix();
        
        Matrix4x4 matrix = Matrix4x4.TRS(_center, _rotation, Vector3.one);
        GL.MultMatrix(matrix);
        
        GL.Begin(GL.LINES);
        GL.Color(_color);
    
        
        Vector3[] corners = Utility.CalculateCorners(Vector3.zero, _halfExtents);
        int[,] edges = Utility.GetBoxEdges();
        
    
        // Draw each edge.
        for (int i = 0; i < edges.GetLength(0); i++)
        {
            GL.Vertex(corners[edges[i, 0]]);
            GL.Vertex(corners[edges[i, 1]]);
        }
    
        GL.End();
        
        GL.PopMatrix();
        
        Destroy(gameObject,0.35f);
    }

}