using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraComponent : MonoBehaviour {

    private Camera levelRunCamera;
    private GameObject trackedObject;
    private Vector3 lastPositionOfTrackedObject;

    public void Awake() {
        this.levelRunCamera = this.GetComponent<Camera>();
    }

    public void TrackObject(GameObject gameObject) {
        if (gameObject == null) {
            throw new ArgumentNullException("gameObject");
        }

        this.trackedObject = gameObject;
        this.lastPositionOfTrackedObject = Vector3.zero;
        this.transform.position = this.gameObject.transform.position + Constants.CameraStartingOffsetFromTarget;
    }

    public void UntrackObject() {
        this.trackedObject = null;
    }

    public void LateUpdate() {
        if (trackedObject == null) {
            return;
        }

        if (this.lastPositionOfTrackedObject == Vector3.zero) {
            this.lastPositionOfTrackedObject = this.trackedObject.transform.position;
            return;
        }

        Vector3 ofsetFromPreviousPosition = this.trackedObject.transform.position - this.lastPositionOfTrackedObject;
        this.levelRunCamera.transform.position += ofsetFromPreviousPosition;
        this.lastPositionOfTrackedObject = this.trackedObject.transform.position;
    }
}
