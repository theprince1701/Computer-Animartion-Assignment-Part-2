using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedControlledPathFollower : MonoBehaviour
{
    [SerializeField] private float speed = 17.5f;
    [SerializeField] private float sampleRate = 16;
    
    private PathCreator _pathCreator;
    
    
    List<List<SamplePoint>> table = new List<List<SamplePoint>>();

    private float _distance;
    private float _accumDistance;
    private int _currentIndex;
    private int _currentSample;

    private Vector2 _p0;
    private Vector2 _p1;
    private Vector2 _p2;
    private Vector2 _p3;

    private SteeringBehaviour _owner;

    
    [System.Serializable]
    private class SamplePoint
    {
        public float samplePosition;
        public float accumulatedDistance;

        public SamplePoint(float samplePosition, float distanceCovered)
        {
            this.samplePosition = samplePosition;
            this.accumulatedDistance = distanceCovered;
        }
    }

    private void Awake()
    {
        _pathCreator = FindObjectOfType<PathCreator>();
    }


    public void Init(SteeringBehaviour owner)
    {
        _owner = owner;
    }
    
    private void Start()
    {
        int size = _pathCreator.path.NumSegments;


        SetPoints(0);
        Vector2 previousPos = Utility.GetCurvePosition(_p0, _p3, _p1, _p2, 0.0f);
        for (int i = 0; i < size; i++)
        {
            List<SamplePoint> segment = new List<SamplePoint>();
            
            segment.Add(new SamplePoint(0.0f, _accumDistance));

            SetPoints(i);
            for (int sample = 1; sample <= sampleRate; ++sample)
            {
                float t = sample / sampleRate;
                Vector2 currentPos = Utility.GetCurvePosition(_p0, _p3, _p1, _p2, t);

                float dist = (previousPos - currentPos).magnitude;
                _accumDistance += dist;
                previousPos = currentPos;
                
                segment.Add(new SamplePoint(t, _accumDistance));
            }
            
            table.Add(segment);
        }
    }


    private void Update()
    {
        float dist = (transform.position - _owner.transform.position).magnitude;
        
        if (dist >= 10.0f)
            return;
        
        _distance += speed * Time.deltaTime;

        int sampleCount = table[_currentIndex].Count;
        while (_distance > table[_currentIndex][_currentSample + 1].accumulatedDistance)
        {
            _currentSample++;

            if (_currentSample >= sampleCount - 1)
            {
                _currentIndex++;
                _currentIndex %= table.Count;
                _currentSample = 0;
                _distance = table[_currentIndex][_currentSample + 1].accumulatedDistance;
            }
        }


        SetPoints(_currentIndex);
        
        transform.position = Utility.GetCurvePosition(_p0, _p3, _p1, _p2, GetAdjustedT());
    }


    private void SetPoints(int i)
    {
        Vector2[] points = _pathCreator.path.GetPointsInSegment(i);

        _p0 = points[0];
        _p1 = points[1];
        _p2 = points[2];
        _p3 = points[3];
    }
    
    float GetAdjustedT()
    {
        SamplePoint current = table[_currentIndex][_currentSample];
        SamplePoint next = table[_currentIndex][_currentSample + 1];

        return Mathf.Lerp(current.samplePosition, next.samplePosition,
            (_distance - current.accumulatedDistance) / (next.accumulatedDistance - current.accumulatedDistance)
        );
    }
}
