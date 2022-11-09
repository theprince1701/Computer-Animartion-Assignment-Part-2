using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilSpillPlacer : MonoBehaviour
{
    [SerializeField] private float spacing = .1f;
    [SerializeField] private float resolution = 1;


    private List<GameObject> oilSpill = new List<GameObject>();

    void Start () {
        Vector2[] points = FindObjectOfType<PathCreator>().path.CalculateEvenlySpacedPoints(spacing, resolution);
        foreach (Vector2 p in points)
        {
            GameObject g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            g.transform.position = p;
            g.transform.localScale = Vector3.one * spacing * 0.1f;
        }
    }
}
