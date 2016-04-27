using UnityEngine;
using Leap.PinchUtility;

/// <summary>
/// A class that allows objects to perform different actions once the object
/// is pinched. All gameObjects with the PinchSelectable component must have
/// a collider component.
/// </summary>
public abstract class PinchSelectable : MonoBehaviour {

    /// <summary>
    /// Invoked when a new pinch is detected on this transform.
    /// </summary>
    /// <param name="pinchDetector">The pinch detector.</param>
    public abstract void OnPinch(LeapPinchDetector pinchDetector, ref RaycastHit hit);

    /// <summary>
    /// Invoked when the detected pinch is released.
    /// </summary>
    public abstract void OnPinchRelease();

}
