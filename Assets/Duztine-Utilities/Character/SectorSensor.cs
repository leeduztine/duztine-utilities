using System.Collections.Generic;
using UnityEngine;

public class SectorSensor : MonoBehaviour
{
    [SerializeField] private float distance = 10f;
    [SerializeField] private float angle = 60f;
    [SerializeField] private float height = 1f;
    [SerializeField] private Color meshColor = Color.red;

    [SerializeField] private int capacity = 50;
    [SerializeField] private int frequency = 30;
    [SerializeField] private float interval;
    [SerializeField] private float timer;
    [SerializeField] private LayerMask targetLayer;

    private Mesh sensorMesh;
    private List<GameObject> detectedTargetList = new List<GameObject>();
    private Collider[] colliders;
    private int count;

    public bool HasTargetInSight
    {
        get => detectedTargetList.Count > 0;
    }

    private void Awake()
    {
        colliders = new Collider[capacity];
        sensorMesh = CreateWedgeMesh();

        interval = 1f / frequency;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            timer = interval;
            Scan();
        }
    }
    
    private void Scan()
    {
        count = Physics.OverlapSphereNonAlloc(transform.position, distance,
            colliders, targetLayer, QueryTriggerInteraction.Collide);
        
        detectedTargetList.Clear();
        for (int i = 0; i < count; ++i)
        {
            GameObject obj = colliders[i].gameObject;
            if (HasFoundTarget(obj))
            {
                detectedTargetList.Add(obj);
            }
        }
    }

    private Mesh CreateWedgeMesh()
    {
        Mesh mesh = new Mesh();

        int segments = 10;
        int numTriangles = (segments * 4) + 2 + 2;
        int numVertices = numTriangles * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];
        
        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0f, -angle, 0f) * Vector3.forward * distance;
        Vector3 bottomRight = Quaternion.Euler(0f, angle, 0f) * Vector3.forward * distance;

        Vector3 topCenter = bottomCenter + Vector3.up * height;
        Vector3 topRight = bottomRight + Vector3.up * height;
        Vector3 topLeft = bottomLeft + Vector3.up * height;

        #region Create vertices array
        
        int index = 0;
        
        //left
        vertices[index++] = bottomCenter;
        vertices[index++] = bottomLeft;
        vertices[index++] = topLeft;

        vertices[index++] = topLeft;
        vertices[index++] = topCenter;
        vertices[index++] = bottomCenter;
        
        //right
        vertices[index++] = bottomCenter;
        vertices[index++] = topCenter;
        vertices[index++] = topRight;
        
        vertices[index++] = topRight;
        vertices[index++] = bottomRight;
        vertices[index++] = bottomCenter;

        float currentAngle = -angle;
        float deltaAngle = (angle * 2) / segments;
        for (int i = 0; i < segments; ++i)
        {
            bottomLeft = Quaternion.Euler(0f, currentAngle, 0f) * Vector3.forward * distance;
            bottomRight = Quaternion.Euler(0f, currentAngle + deltaAngle, 0f) * Vector3.forward * distance;

            topRight = bottomRight + Vector3.up * height;
            topLeft = bottomLeft + Vector3.up * height;
            
            //far
            vertices[index++] = bottomLeft;
            vertices[index++] = bottomRight;
            vertices[index++] = topRight;
        
            vertices[index++] = topRight;
            vertices[index++] = topLeft;
            vertices[index++] = bottomLeft;

            //top
            vertices[index++] = topCenter;
            vertices[index++] = topLeft;
            vertices[index++] = topRight;

            //bot
            vertices[index++] = bottomCenter;
            vertices[index++] = bottomRight;
            vertices[index++] = bottomLeft;
            
            currentAngle += deltaAngle;
        }
        
        
        #endregion

        for (int i = 0; i < numVertices; ++i)
        {
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        
        return mesh;
    }

    private bool HasFoundTarget(GameObject target)
    {
        Vector3 origin = transform.position;
        Vector3 destination = target.transform.position;
        Vector3 direction = destination - origin;
        
        if (direction.y < 0f || direction.y > height)
        {
            return false;
        }
        
        direction.y = 0f;
        float deltaAngle = Vector3.Angle(direction, transform.forward);
        if (deltaAngle > angle)
        {
            return false;
        }
        
        return true;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = HasTargetInSight ? meshColor : new Color(0f,0f,1f,0.5f);
        
        if (sensorMesh)
        {
            Gizmos.DrawMesh(sensorMesh, transform.position, transform.rotation);
        }
        else
        {
            Debug.LogError("Error: no sensor-mesh");
        }
        
        Gizmos.DrawWireSphere(transform.position, distance);
        for (int i = 0; i < count; ++i)
        {
            if (colliders[i])
            {
                Gizmos.DrawSphere(colliders[i].transform.position, 0.5f);
            }
        }
        
        Gizmos.color = Color.green;
        detectedTargetList.ForEach(target =>
        {
            if (target) Gizmos.DrawSphere(target.transform.position, 0.5f);
        });
    }
}