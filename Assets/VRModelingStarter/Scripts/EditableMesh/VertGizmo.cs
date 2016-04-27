using UnityEngine;
using System.Collections.Generic;
using Leap.PinchUtility;

/// <summary>
/// This class allows the position of the vertices (of an EditableMesh) to be
/// modified. When the position of the transform is modified the corresponding
/// vertices are updated.
/// </summary>
public class VertGizmo : PinchSelectable {

    #region Fields

    private LeapEditableMesh _editableMesh;
    
    private List<int> _vertIndices = new List<int>();
    private LeapPinchDetector _pinchDetector;
    private Material _material;

    private bool _isReady = false;
    private bool _isColliding = false;
    private bool _isPinched = false;

    private int _pinchCount = 0;
    private float _lastPinchTime;
    private float _unfreezeTime = 0.5f;

    #endregion

    #region Properties

    public LeapEditableMesh EditableMesh
    {
        get
        {
            return _editableMesh;
        }

        set
        {
            _editableMesh = value;
        }
    }

    public int PinchCount
    {
        get
        {
            return _pinchCount;
        }

        set
        {
            _pinchCount = value;
        }
    }

    public bool IsReady
    {
        get
        {
            return _isReady;
        }

        set
        {
            _isReady = value;
        }
    }
    
    #endregion

    private void Start()
    {
        _material = transform.GetComponent<Renderer>().material;
    }

    private void Update() {
        if (_isPinched)
        {
            transform.position = _pinchDetector.Position;
            repositionVerts();
        }
        else
        {
            repositionVerts();
        }
    }

    /// <summary>
    /// Invoked when a new pinch is detected on this vert gizmo.
    /// </summary>
    /// <param name="pinchDetector">The pinch detector.</param>
    /// <param name="hit"></param>
    public override void OnPinch(LeapPinchDetector pinchDetector, ref RaycastHit hit)
    {
        var curPinchTime = Time.time;
        if (!(curPinchTime - _lastPinchTime < _unfreezeTime))
        {
            _lastPinchTime = curPinchTime;
            return; // Double pinch unsuccessful.
        }

        // Double pinch successful. 
        _pinchDetector = pinchDetector;
        _isPinched = _isColliding;
        if (_isPinched) {
            EditableMesh.NumActiveVertGizmos++;
        }
        updateMaterial();
    }

    /// <summary>
    /// Invoked when the detected pinch is released.
    /// </summary>
    public override void OnPinchRelease()
    {
        if (_isPinched)
        {
            EditableMesh.NumActiveVertGizmos--;
        }

        _isPinched = false;
        updateMaterial();
    }

    private void OnTriggerEnter(Collider other) {
        if (_isColliding)
            return;

        if (other.tag == "PinchPosGizmo") {
            _isColliding = true;
            updateMaterial();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PinchPosGizmo")
        {
            _isColliding = false;
            updateMaterial();
        }
    }

    /// <summary>
    /// Adds the index of the vertex that this VertGizmo controls.
    /// </summary>
    /// <param name="index">The index of the vertex.</param>
    public void AddVertIndex(int index) {
        _vertIndices.Add(index);
    }

    /// <summary>
    /// Updates the material of this VertGizmo. The material renders a different
    /// color depending on the state (not-colliding, colliding, colliding && pinching)
    /// of this VertGizmo. 
    /// </summary>
    private void updateMaterial()
    {
        _material.SetFloat("_IsPinchReady", Helpers.BoolToFloat(_isColliding));
        _material.SetFloat("_IsPinched", Helpers.BoolToFloat(_isPinched));
    }

    /// <summary>
    /// Repositions the verts in the editable mesh (that this VertGizmo controls)
    /// to correspond with the position of this VertGizmo.
    /// </summary>
    private void repositionVerts()
    {
        foreach (var vertInd in _vertIndices)
        {
            if (vertInd < EditableMesh.Verts.Count)
            {
                EditableMesh.Verts[vertInd] = transform.position;
                EditableMesh.RefreshMesh();
            }
        }
    }


}
