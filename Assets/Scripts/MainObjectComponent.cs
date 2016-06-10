using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class MainObjectComponent : MonoBehaviour {

    public event Action DestinationReached;
    public event Action CollidedWithAnotherObject;

    private Rigidbody objectRigidbody;
    private Vector3 destination;
    private bool isMoving = false;
    private Vector3 previousPosition = Vector3.zero;

    public void Awake() {
        this.objectRigidbody = this.GetComponent<Rigidbody>();
    }

    public void Update() {
        if (!this.isMoving) {
            return;
        }

        if (this.previousPosition == Vector3.zero) {
            this.previousPosition = this.transform.position;
            return;
        }

        if (this.WasDestinationReached()) {
            this.isMoving = false;
            if (this.DestinationReached != null) {
                this.DestinationReached();
            }
        }
    }

    private bool WasDestinationReached() {
        Vector3 currentPosition = this.transform.position;
        float previousPositionToDestinationDistance = Vector3.Distance(this.previousPosition, this.destination);
        float travelledDistance = Vector3.Distance(this.previousPosition, currentPosition);
        return previousPositionToDestinationDistance <= travelledDistance;
    }

    public void OnTriggerEnter() {
        if (this.CollidedWithAnotherObject != null) {
            this.CollidedWithAnotherObject();
        }
    }

    public void MoveObject(Vector3 destination, float speed) {
        if (speed <= 0) {
            throw new ArgumentOutOfRangeException("speed", "Cannot be zero or less.");
        }

        this.destination = destination;
        Vector3 direction = (destination - this.transform.position).normalized;
        this.objectRigidbody.velocity = direction * speed;
        this.previousPosition = Vector3.zero;
        this.isMoving = true;
    }
}