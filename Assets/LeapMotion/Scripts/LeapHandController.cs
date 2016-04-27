﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;

namespace Leap.Unity {
  /**
   * LeapHandController uses a Factory to create and updata HandRepresentations based on Frame's received from a Provider  */
  public class LeapHandController : MonoBehaviour {
    /** The scale factors for hand movement. Set greater than 1 to give the hands a greater range of motion. */
    public Vector3 handMovementScale = Vector3.one;

    public LeapProvider Provider { get; set; }
    public HandFactory Factory { get; set; }

    public Dictionary<int, HandRepresentation> graphicsReps = new Dictionary<int, HandRepresentation>();
    public Dictionary<int, HandRepresentation> physicsReps = new Dictionary<int, HandRepresentation>();

    // Reference distance from thumb base to pinky base in mm.
    protected const float GIZMO_SCALE = 5.0f;
    /** Conversion factor for millimeters to meters. */
    protected const float MM_TO_M = 1e-3f;
    /** Conversion factor for nanoseconds to seconds. */
    protected const float NS_TO_S = 1e-6f;
    /** Conversion factor for seconds to nanoseconds. */
    protected const float S_TO_NS = 1e6f;
    /** How much smoothing to use when calculating the FixedUpdate offset. */
    protected const float FIXED_UPDATE_OFFSET_SMOOTHING_DELAY = 0.1f;

    protected bool graphicsEnabled = true;
    protected bool physicsEnabled = true;

    public bool GraphicsEnabled {
      get {
        return graphicsEnabled;
      }
      set {
        graphicsEnabled = value;
        if (!graphicsEnabled) {
          //DestroyGraphicsHands();
        }
      }
    }

    public bool PhysicsEnabled {
      get {
        return physicsEnabled;
      }
      set {
        physicsEnabled = value;
        if (!physicsEnabled) {
          //DestroyPhysicsHands();
        }
      }
    }
    private long prev_graphics_id_ = 0;
    private long prev_physics_id_ = 0;

    /** Draws the Leap Motion gizmo when in the Unity editor. */
    void OnDrawGizmos() {
      Gizmos.matrix = Matrix4x4.Scale(GIZMO_SCALE * Vector3.one);
      Gizmos.DrawIcon(transform.position, "leap_motion.png");
    }

    protected virtual void Start() {
      Provider = GetComponent<LeapProvider>();
      Factory = GetComponent<HandFactory>();
    }

    /** Updates the graphics HandRepresentations. */
    protected virtual void Update() {
      Frame frame = Provider.CurrentFrame;
      if (frame.Id != prev_graphics_id_ && graphicsEnabled) {
        UpdateHandRepresentations(graphicsReps, ModelType.Graphics, frame);
        prev_graphics_id_ = frame.Id;

      }
    }

    /** Updates the physics HandRepresentations. */
    protected virtual void FixedUpdate() {
      Frame fixedFrame = Provider.CurrentFixedFrame;

      if (fixedFrame.Id != prev_physics_id_ && physicsEnabled) {
        UpdateHandRepresentations(physicsReps, ModelType.Physics, fixedFrame);
        prev_physics_id_ = fixedFrame.Id;
      }
    }

    /** 
    * Updates HandRepresentations based in the specified HandRepresentation Dictionary.
    * Active HandRepresentation instances are updated if the hand they represent is still
    * present in the Provider's CurrentFrame; otherwise, the HandRepresentation is removed. If new
    * Leap Hand objects are present in the Leap HandRepresentation Dictionary, new HandRepresentations are 
    * created and added to the dictionary. 
    * @param all_hand_reps = A dictionary of Leap Hand ID's with a paired HandRepresentation
    * @param modelType Filters for a type of hand model, for example, physics or graphics hands.
    * @param frame The Leap Frame containing Leap Hand data for each currently tracked hand
    */
    void UpdateHandRepresentations(Dictionary<int, HandRepresentation> all_hand_reps, ModelType modelType, Frame frame) {
      foreach (Leap.Hand curHand in frame.Hands) {
        HandRepresentation rep;
        if (!all_hand_reps.TryGetValue(curHand.Id, out rep)) {
          rep = Factory.MakeHandRepresentation(curHand, modelType);
          if (rep != null) {
            all_hand_reps.Add(curHand.Id, rep);
          }
        }
        if (rep != null) {
          rep.IsMarked = true;
          rep.UpdateRepresentation(curHand, modelType);
          rep.LastUpdatedTime = (int)frame.Timestamp;
        }
      }

      /** Mark-and-sweep to finish unused HandRepresentations */
      HandRepresentation toBeDeleted = null;
      foreach (KeyValuePair<int, HandRepresentation> r in all_hand_reps) {
        if (r.Value != null) {
          if (r.Value.IsMarked) {
            r.Value.IsMarked = false;
          }
          else {
            /** Initialize toBeDeleted with a value to be deleted */
            //Debug.Log("Finishing");
            toBeDeleted = r.Value;
          }
        }
      }
      /**Inform the representation that we will no longer be giving it any hand updates 
       * because the corresponding hand has gone away */
      if (toBeDeleted != null) {
        all_hand_reps.Remove(toBeDeleted.HandID);
        toBeDeleted.Finish();
      }
    }
  }
}
