﻿using Leap.Unity;
using UnityEngine;

namespace Leap.PinchUtility
{

    /// <summary>
    /// A basic utility class to aid in creating pinch based actions.  Once linked with an IHandModel, it can
    /// be used to detect pinch gestures that the hand makes.
    /// </summary>
    public class LeapPinchDetector : MonoBehaviour
    {
        protected const float MM_TO_M = 0.001f;

        [SerializeField]
        protected IHandModel _handModel;

        [SerializeField]
        protected float _activatePinchDist = 0.03f;

        [SerializeField]
        protected float _deactivatePinchDist = 0.04f;

        protected bool _isPinching = false;
        protected bool _didChange = false;

        protected float _lastPinchTime = 0.0f;
        protected float _lastUnpinchTime = 0.0f;

        protected Vector3 _pinchPos;
        protected Quaternion _pinchRotation;

        protected virtual void OnValidate()
        {
            if (_handModel == null)
            {
                _handModel = GetComponentInParent<IHandModel>();
            }

            _activatePinchDist = Mathf.Max(0, _activatePinchDist);
            _deactivatePinchDist = Mathf.Max(0, _deactivatePinchDist);

            //Activate distance cannot be greater than deactivate distance
            if (_activatePinchDist > _deactivatePinchDist)
            {
                _deactivatePinchDist = _activatePinchDist;
            }
        }

        protected virtual void Awake()
        {
            if (_handModel == null)
            {
                Debug.LogWarning("The HandModel field of LeapPinchDetector was unassigned and the detector has been disabled.");
                enabled = false;
            }
        }

        /// <summary>
        /// Returns whether or not the dectector is currently detecting a pinch.
        /// </summary>
        public bool IsPinching
        {
            get
            {
                return _isPinching;
            }
        }

        /// <summary>
        /// Returns whether or not the value of IsPinching is different than the value reported during
        /// the previous frame.
        /// </summary>
        public bool DidChangeFromLastFrame
        {
            get
            {
                return _didChange;
            }
        }

        /// <summary>
        /// Returns whether or not the value of IsPinching changed to true between this frame and the previous.
        /// </summary>
        public bool DidStartPinch
        {
            get
            {
                return DidChangeFromLastFrame && IsPinching;
            }
        }

        /// <summary>
        /// Returns whether or not the value of IsPinching changed to false between this frame and the previous.
        /// </summary>
        public bool DidEndPinch
        {
            get
            {
                return DidChangeFromLastFrame && !IsPinching;
            }
        }

        /// <summary>
        /// Returns the value of Time.time during the most recent pinch event.
        /// </summary>
        public float LastPinchTime
        {
            get
            {
                return _lastPinchTime;
            }
        }

        /// <summary>
        /// Returns the value of Time.time during the most recent unpinch event.
        /// </summary>
        public float LastUnpinchTime
        {
            get
            {
                return _lastUnpinchTime;
            }
        }

        /// <summary>
        /// Returns the position value of the detected pinch.  If a pinch is not currently being
        /// detected, returns the most recent pinch position value.
        /// </summary>
        public Vector3 Position
        {
            get
            {
                return _pinchPos;
            }
        }

        /// <summary>
        /// Returns the rotation value of the detected pinch.  If a pinch is not currently being
        /// detected, returns the most recent pinch rotation value.
        /// </summary>
        public Quaternion Rotation
        {
            get
            {
                return _pinchRotation;
            }
        }

        protected virtual void Update()
        {
            _didChange = false;

            Hand hand = _handModel.GetLeapHand();

            if (hand == null || !_handModel.IsTracked)
            {
                changePinchState(false);
                return;
            }

            float pinchDistance = hand.PinchDistance * MM_TO_M;
            transform.rotation = hand.Basis.Rotation();

            var fingers = hand.Fingers;
            transform.position = Vector3.zero;
            for (int i = 0; i < fingers.Count; i++)
            {
                Finger finger = fingers[i];
                if (finger.Type == Finger.FingerType.TYPE_INDEX ||
                    finger.Type == Finger.FingerType.TYPE_THUMB)
                {
                    transform.position += finger.Bone(Bone.BoneType.TYPE_DISTAL).NextJoint.ToVector3();
                }
            }
            transform.position /= 2.0f;

            if (_isPinching)
            {
                if (pinchDistance > _deactivatePinchDist)
                {
                    changePinchState(false);
                    return;
                }
            }
            else
            {
                if (pinchDistance < _activatePinchDist)
                {
                    changePinchState(true);
                }
            }

            if (_isPinching)
            {
                _pinchPos = transform.position;
                _pinchRotation = transform.rotation;
            }
        }

        protected virtual void changePinchState(bool shouldBePinching)
        {
            if (_isPinching != shouldBePinching)
            {
                _isPinching = shouldBePinching;

                if (_isPinching)
                {
                    _lastPinchTime = Time.time;
                }
                else
                {
                    _lastUnpinchTime = Time.time;
                }

                _didChange = true;
            }
        }
    }
}
