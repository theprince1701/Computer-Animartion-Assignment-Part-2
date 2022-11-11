using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PathPlacer : MonoBehaviour
{
	public static PathPlacer Instance;

	[SerializeField] private float spacing = .1f;
    [SerializeField] private float resolution = 1;
    [SerializeField] private float spawnTimer = 10f;
    [SerializeField] private GameObject[] placeableObjects;

    private List<PathSpawnPoint> _spawnPoints = new List<PathSpawnPoint>();
    private float _spawnTimer;

    
    [HideInInspector]
    public List<Powerup> oilSpills = new List<Powerup>();

    [HideInInspector]
    public List<Powerup> powerUps = new List<Powerup>();

    private void Awake()
    {
	    Instance = this;
    }

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
		    Powerup instance = Instantiate(placeableObject, spawnPoint.position, Quaternion.identity)
			    .GetComponent<Powerup>();
		    
		    switch (instance.CollisionType)
		    {
			    case CollisionTypes.Powerup:
				    powerUps.Add(instance);
				    break;
			    
			    case CollisionTypes.OilSpill:
				    oilSpills.Add(instance);
				    break;
		    }
		    
		    spawnPoint.powerUp = instance;
	    }
    }

    public void RemovePowerUp(Powerup powerUp)
    {
	    switch (powerUp.CollisionType)
	    {
		    case CollisionTypes.Powerup:
			    powerUps.Remove(powerUp);
			    break;
			    
		    case CollisionTypes.OilSpill:
			    oilSpills.Remove(powerUp);
			    break;
	    }
	    
	    Destroy(powerUp.gameObject);
    }
    
    private PathSpawnPoint GetNextSpawnPoint()
    {
	    List<PathSpawnPoint> avaliableSpawnPoints = new List<PathSpawnPoint>();

	    for (int i = 0; i < _spawnPoints.Count; i++)
	    {
		    if (_spawnPoints[i].powerUp != null)
			    continue;
		    
		    avaliableSpawnPoints.Add(_spawnPoints[i]);
	    }

	    return avaliableSpawnPoints[Random.Range(0, avaliableSpawnPoints.Count)];
    }



	[System.Serializable]
	public class PathSpawnPoint
	{
		public Vector2 position;
		public Powerup powerUp;

		public PathSpawnPoint(Vector2 position, Powerup powerUp)
		{
			this.position = position;
			this.powerUp = powerUp;
		}
	}
}
