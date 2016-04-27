using UnityEngine;
using System.Collections;
using Leap.PinchUtility;
using Leap.Unity;

[ExecuteAfter(typeof(LeapPinchSelector))]
public class MeshManipulation : MonoBehaviour {

    [SerializeField]
    private LeapPinchDetector _pinchDetectorA;

    [SerializeField]
    private LeapPinchDetector _pinchDetectorB;

    [SerializeField]
    private LeapEditableMesh _editableMesh;

    [SerializeField]
    private Transform _vertGizmos;

    private Transform _vGAnchor;

    private void Awake()
    {
        if (_pinchDetectorA == null || _pinchDetectorB == null)
        {
            Debug.LogWarning("Both Pinch Detectors of the LeapRTS component must be assigned. This component has been disabled.");
            enabled = false;
        }


    }

    private void Start()
    {
        GameObject pinchControl = new GameObject("VertGizmoAnchor");
        _vGAnchor = pinchControl.transform;
        _vGAnchor.transform.parent = _vertGizmos.parent;
        _vertGizmos.parent = _vGAnchor;
    }

    private void Update () {

        if (GameStates.DrawMode)
            return;

        if (_editableMesh.NumActiveVertGizmos > 0)
            return;

        bool didUpdate = false;
        didUpdate |= _pinchDetectorA.DidChangeFromLastFrame;
        didUpdate |= _pinchDetectorB.DidChangeFromLastFrame;

        if (didUpdate)
        {
            _vertGizmos.SetParent(null, true);
        }

        if (_pinchDetectorA.IsPinching && _pinchDetectorB.IsPinching)
        {
            transformDoubleAnchor();
        }
        else if (_pinchDetectorA.IsPinching)
        {
            transformSingleAnchor(_pinchDetectorA);
        }
        else if (_pinchDetectorB.IsPinching)
        {
            transformSingleAnchor(_pinchDetectorB);
        }

        if (didUpdate)
        {
            _vertGizmos.SetParent(_vGAnchor, true);
        }
    }

    private void transformDoubleAnchor()
    {
        _vGAnchor.position = (_pinchDetectorA.Position + _pinchDetectorB.Position) / 2.0f;
        Quaternion pp = Quaternion.Lerp(_pinchDetectorA.Rotation, _pinchDetectorB.Rotation, 0.5f);
        Vector3 u = pp * Vector3.up;
        _vGAnchor.LookAt(_pinchDetectorA.Position, u);

        _vGAnchor.localScale = Vector3.one * Vector3.Distance(_pinchDetectorA.Position, _pinchDetectorB.Position);

    }

    private void transformSingleAnchor(LeapPinchDetector singlePinch)
    {
        _vGAnchor.position = singlePinch.Position;

        _vGAnchor.localScale = Vector3.one;
    }

}
