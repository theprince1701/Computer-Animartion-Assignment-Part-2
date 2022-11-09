using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PathPlacer : MonoBehaviour {

    [SerializeField] private float spacing = .1f;
    [SerializeField] private float resolution = 1;
    [SerializeField] private float spawnTimer = 10f;
    [SerializeField] private GameObject[] placeableObjects;

    private List<PathSpawnPoint> _spawnPoints = new List<PathSpawnPoint>();
    private float _spawnTimer;


    private void Start ()
    {
	    PathCreator pathCreator = FindObjectOfType<PathCreator>();
	    
	    Vector2[] spawnPoints = pathCreator.path.CalculateEvenlySpacedPoints(spacing, resolution);

	    for (int i = 0; i < spawnPoints.Length; i++)
	    {
		    _spawnPoints.Add(new PathSpawnPoint(spawnPoints[i], null));
	    }
    }

    private void Update()
    {
	    _spawnTimer += Time.deltaTime;

	    if (_spawnTimer >= spawnTimer)
	    {
		    SpawnObject();
		    _spawnTimer = 0.0f;
	    }
    }

    private void SpawnObject()
    {
	    GameObject placeableObject = placeableObjects[Random.Range(0, placeableObjects.Length)];
	    PathSpawnPoint spawnPoint = GetNextSpawnPoint();

	    if (spawnPoint != null && placeableObject != null)
	    {
		    GameObject instance = Instantiate(placeableObject, spawnPoint.position, Quaternion.identity);
		    spawnPoint.gameObject = instance;
	    }
    }
    
    private PathSpawnPoint GetNextSpawnPoint()
    {
	    for (int i = 0; i < _spawnPoints.Count; i++)
	    {
		    if (_spawnPoints[i].gameObject != null)
			    continue;

		    return _spawnPoints[i];
	    }

	    return null;
    }



	[System.Serializable]
	public class PathSpawnPoint
	{
		public Vector2 position;
		public GameObject gameObject;

		public PathSpawnPoint(Vector2 position, GameObject gameObject)
		{
			this.position = position;
			this.gameObject = gameObject;
		}
	}
}
