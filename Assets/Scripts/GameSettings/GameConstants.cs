using System.Collections.Generic;
using UnityEngine;

public class GameConstants : ScriptableObject {
    [SerializeField]
    private List<Vector3> targetDestinations;
    [SerializeField]
    private float mainObjectSpeed = 10;
    [SerializeField]
    private int numberOfObstacles = 100;
    [SerializeField]
    private float obstacleMaxPlanePositionValue = 50f;
    [SerializeField]
    private float obstacleMaxHeightPositionValue = 1f;

    public List<Vector3> TargetDestinations { get { return this.targetDestinations; } }
    public float MainObjectSpeed { get { return this.mainObjectSpeed; } }
    public int NumberOfObstacles { get { return this.numberOfObstacles; } }
    public float ObstacleMaxPlanePositionValue { get { return this.obstacleMaxPlanePositionValue; } }
    public float ObstacleMaxHeightPositionValue { get { return this.obstacleMaxHeightPositionValue; } }
}