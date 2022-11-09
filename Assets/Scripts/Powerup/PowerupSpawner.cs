using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class PowerupSpawner : MonoBehaviour
{
    [SerializeField] private float spacing = .1f;
    [SerializeField] private float resolution = 1;
    [SerializeField] private float spawnTimer = 10f;
    [SerializeField] private GameObject powerUpPrefab;

    public static PowerupSpawner Instance;

    [HideInInspector] public UnityEvent<Powerup> onPowerUpSpawned; 
    [HideInInspector] public UnityEvent<Powerup> onPowerUpDestroyed;

    private Vector2[] _spawnPoints;

    private float _spawnTimer;

    private void Awake()
    {
        Instance = this;
        Debug.Log(Instance);
    }
    
    private void Start ()
    {
        PathCreator pathCreator = FindObjectOfType<PathCreator>();
        _spawnPoints = pathCreator.path.CalculateEvenlySpacedPoints(spacing, resolution);
    }

    private void Update()
    {
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= spawnTimer)
        {
            SpawnPowerUp();
            _spawnTimer = 0.0f;
        }
    }

    private void SpawnPowerUp()
    {
        Vector2 spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];

        Powerup powerUp = Instantiate(powerUpPrefab, spawnPoint, Quaternion.identity).GetComponent<Powerup>();
        
        onPowerUpSpawned?.Invoke(powerUp);
    }
}
