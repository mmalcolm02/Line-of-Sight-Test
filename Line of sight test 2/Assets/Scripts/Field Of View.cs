using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = new Mesh(); //create mesh
        GetComponent<MeshFilter>().mesh = mesh; //call in the mesh filter component

        float fov = 90.0f;//angle of view
        Vector3 origin = Vector3.zero; //setting origin of mesh
        int rayCount = 20;//number of mesh points in arc
        float angle = 0f; //initial point of arc
        float angleIncrease = fov / rayCount; //incremental angle in arc
        float viewDistance = 10.0f; //distance of sight

        Vector3[] vertices = new Vector3[rayCount + 1 + 1]; //number of mesh vertices
        Vector2[] uv = new Vector2[vertices.Length]; // as above 
        int[] triangles = new int[rayCount * 3]; //order of connection

        vertices[0] = origin; //specify first vertex is zero

        int vertexIndex = 1; //specify starting the vertex from 1
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++) //depending on raycount incresae iterations of vertices and triangles
        {
            Vector3 vertex; //create variable for vertices
            RaycastHit hit; //specify a raycast hit variable
            Ray vertexRay = new Ray(origin, GetVectorFromAngle(angle)); //send ray
            Debug.DrawRay(origin, GetVectorFromAngle(angle) * viewDistance); //draw to see where rays are going - wrong direction!

            if (Physics.Raycast(vertexRay,out hit,viewDistance)) //if ray hits
            {
                vertex = hit.point; //hit point becomes the vertex
            }
            else
            {
                vertex = origin + GetVectorFromAngle(angle) * viewDistance; //otherwise the full distance
            }
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;

            }

            vertexIndex++;
            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Vector3 GetVectorFromAngle(float angle)
    {
        //angle = 0 > 360
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));


    }

}
