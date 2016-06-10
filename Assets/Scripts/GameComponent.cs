using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for execution of the game logic
/// </summary>
public class GameComponent : MonoBehaviour {

    [SerializeField]
    private MainObjectComponent mainObject;
    [SerializeField]
    private GameObject obstacleTemplate;
    [SerializeField]
    private CameraComponent cameraComponent;

    private GameConstants GameConstants { get { return GameSettingsManager.GetGameConstants(); } }
    private IList<GameObject> obstacleList;
    private int numberOfCompletedDestinations = 0;
    private int numberOfDestinations;

    public void Awake() {
        this.obstacleList = new List<GameObject>();
        this.InitializeObstacles();
    }

    public void Start() {
        this.numberOfDestinations = this.GameConstants.TargetDestinations.Count;
        if (this.numberOfDestinations == 0) {
            throw new InvalidOperationException("There should be at least one destination.");
        }

        this.mainObject.CollidedWithAnotherObject += OnCollisionHit;
        this.cameraComponent.TrackObject(this.mainObject.gameObject);
        this.MoveMainObject();
    }

    private void MoveMainObject() {
        Vector3 nextDestination = this.GameConstants.TargetDestinations[this.numberOfCompletedDestinations];
        this.mainObject.DestinationReached += OnDestinationReached;
        this.mainObject.MoveObject(nextDestination, GameConstants.MainObjectSpeed);
    }

    private void OnDestinationReached() {
        this.mainObject.DestinationReached -= OnDestinationReached;
        this.numberOfCompletedDestinations++;
        if (this.numberOfCompletedDestinations == this.numberOfDestinations) {
            this.FinishGame(true);
        }
        else {
            this.MoveMainObject();
        }
    }

    private void InitializeObstacles() {
        for (int i = 0; i < GameConstants.NumberOfObstacles; i++) {
            GameObject instantiatedObstacle = Instantiate(this.obstacleTemplate, this.GetRandomObstaclePosition(), Quaternion.identity) as GameObject;
            this.obstacleList.Add(instantiatedObstacle);
        }
    }

    private Vector3 GetRandomObstaclePosition() {
        return new Vector3(
            x: UnityEngine.Random.Range(-GameConstants.ObstacleMaxPlanePositionValue, GameConstants.ObstacleMaxPlanePositionValue),
            y: UnityEngine.Random.Range(-GameConstants.ObstacleMaxHeightPositionValue, GameConstants.ObstacleMaxHeightPositionValue),
            z: UnityEngine.Random.Range(-GameConstants.ObstacleMaxPlanePositionValue, GameConstants.ObstacleMaxPlanePositionValue));
    }

    private void OnCollisionHit() {
        this.mainObject.CollidedWithAnotherObject -= OnCollisionHit;
        this.mainObject.DestinationReached -= OnDestinationReached;
        this.FinishGame(false);
    }

    private void FinishGame(bool isVictory) {
        if (isVictory) {
            AudioManagerComponent.Instance.PlayWinSound();
        }
        else {
            AudioManagerComponent.Instance.PlayLoseSound();
        }

        this.cameraComponent.UntrackObject();
        Destroy(this.mainObject.gameObject);
    }
}