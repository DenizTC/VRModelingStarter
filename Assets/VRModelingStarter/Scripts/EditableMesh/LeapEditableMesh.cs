using UnityEngine;
using Leap.PinchUtility;
using System.Collections.Generic;

/// <summary>
/// This class is used for creating procedural meshes. 
/// 
/// A vertex is drawn when a pinch is detected. Once four vertices are drawn, a quad 
/// face is rendered. Quads must be drawn in a clockwise direction for their normals 
/// to point outwards. Similarly, quads drawn in an anti clockwise direction will 
/// result in the normals facing in.
/// </summary>
public class LeapEditableMesh : MonoBehaviour {

    #region Fields

    [SerializeField]
    private LeapPinchSelector _pinchDetectorA;

    [SerializeField]
    private LeapPinchSelector _pinchDetectorB;

    /// <summary>
    /// The number of active vert gizmos.
    /// </summary>
    private int _numActiveVertGizmos = 0;
    
    /// <summary>
    /// The number of vertices for the current quad being drawn.
    /// </summary>
    private int _numVertsCur = 0;

    /// <summary>
    /// The number of quads in this mesh.
    /// </summary>
    private int _numOfQuads = 0;

    private Transform _vertGizmosGroup;
    private Vector3[] _curQuad = new Vector3[4];
    private VertGizmo[] _curVGS = new VertGizmo[4];
    private List<Vector3> _verts = new List<Vector3>();
    private List<int> _tris = new List<int>();
    private List<Vector2> _uvs = new List<Vector2>();

    private Mesh _mesh;
    private MeshFilter _meshFilter;
    private MeshCollider _meshCollider;

    private List<LeapEditableMesh_Face> _faces = new List<LeapEditableMesh_Face>();

    #endregion

    #region Properties

    public List<Vector3> Verts {
        get {
            return _verts;
        }
    }

    public int NumActiveVertGizmos
    {
        get
        {
            return _numActiveVertGizmos;
        }

        set
        {
            _numActiveVertGizmos = value;
        }
    }

    #endregion

    private void Awake()
    {
        if (!_pinchDetectorA || !_pinchDetectorB) {
            Debug.LogWarning("Both pinch detectors must be assigned for the LeapEditableMesh component. Disabling component.");
            enabled = false;
        }

        _vertGizmosGroup = transform.FindChild("VertGizmos").transform;
    }

	private void Start () {

        _meshFilter = transform.GetComponent<MeshFilter>();
        _mesh = new Mesh();
        _meshFilter.mesh = _mesh;
        _meshCollider = transform.GetComponent<MeshCollider>();
        _meshCollider.sharedMesh = _mesh;

        
    }
	
	private void Update () {

        if (!GameStates.DrawMode)
            return;

        LeapPinchSelector _pinchDetector = 
            (_pinchDetectorA.DidStartPinch) ? _pinchDetectorA :
            (_pinchDetectorB.DidStartPinch) ? _pinchDetectorB : null;
        //bool pinchStart = _pinchDetectorA.DidStartPinch;


        if (_pinchDetector)
        {
            if (!_pinchDetector.LastPinchable)
            {
                VertGizmo vg = drawVertGizmo(_pinchDetector.Position, _verts.Count + _numVertsCur);

                _curQuad[_numVertsCur] = _pinchDetector.Position;
                _curVGS[_numVertsCur] = vg;
                _numVertsCur++;
            }
            else if(_pinchDetector.LastPinchable.GetType() == typeof(VertGizmo))
            {
                VertGizmo vg = _pinchDetector.LastPinchable as VertGizmo;
                vg.IsReady = false;
                vg.AddVertIndex(_verts.Count + _numVertsCur);

                _curQuad[_numVertsCur] = vg.transform.position;
                _curVGS[_numVertsCur] = vg;
                _numVertsCur++;
                
            }
        }

        if (_numVertsCur == 4)
        {
            finalizeQuad();
            _numVertsCur = 0;
        }
           

	}

    /// <summary>
    /// Draws the vertex gizmo centered at the vertex position.
    /// </summary>
    /// <param name="position">The position of the vertex.</param>
    /// <param name="vertIndex">Index of the vertex.</param>
    /// <returns></returns>
    private VertGizmo drawVertGizmo(Vector3 position, int vertIndex) {
        GameObject vertGizmoGO = 
            Instantiate(Resources.Load("prefabs/VertGizmo"), position, Quaternion.identity) as GameObject;

        VertGizmo vertGizmo = vertGizmoGO.transform.GetComponent<VertGizmo>();
        vertGizmo.name = "vertGizmo" + vertIndex;
        vertGizmo.AddVertIndex(vertIndex);
        vertGizmo.EditableMesh = this;

        vertGizmoGO.transform.SetParent(_vertGizmosGroup);
        return vertGizmo;
    }

    /// <summary>
    /// Untested!
    /// Checks the format of the current completed quad. A properly formatted
    /// quad must have four unique vertices.
    /// </summary>
    private void checkQuad(out bool isProperFormat) {
        isProperFormat = true;

        for (int i = 0; i < 4; i++)
        {
            if (_curVGS[i].PinchCount > 1)
            {
                isProperFormat &= false;
            }
        }
    }

    /// <summary>
    /// Finalizes and render a new quad. 
    /// </summary>
    private void finalizeQuad() {
        calcVerts();
        calcTris();
        calcUV();
        RefreshMesh();
        readyVertGizmos();

        _numOfQuads++;
    }

    private void calcVerts() {
        // Already calculated, just add them to verts array.
        for (int i = 0; i < 4; i++)
        {
            _verts.Add(_curQuad[i]);
        }
    }

    private void calcTris() {
        int triIndex = 4 * (_numOfQuads + 1) - 1;

        // First triangle.
        _tris.Add(triIndex - 3);
        _tris.Add(triIndex - 2);
        _tris.Add(triIndex - 1);

        // Second triangle.
        _tris.Add(triIndex - 1);
        _tris.Add(triIndex);
        _tris.Add(triIndex - 3);

    }

    private void calcUV() {
        _uvs.Add(new Vector2(0, 0)); // Lower left vert.
        _uvs.Add(new Vector2(0, 1)); // Upper left vert.
        _uvs.Add(new Vector2(1, 1)); // Upper right vert.
        _uvs.Add(new Vector2(1, 0)); // Lower right vert.
    }

    /// <summary>
    /// Readies any newly created vert gizmo in the most recent quad drawn.
    /// </summary>
    private void readyVertGizmos() {
        _faces.Add(new LeapEditableMesh_Face());
        for (int i = 0; i < 4; i++)
        {
            _curVGS[i].IsReady = true;
            _curVGS[i].PinchCount = 0;
            _faces[_faces.Count - 1].VertGizmos[i] = _curVGS[i];
        }
    }

    /// <summary>
    /// Refreshes the mesh. Should be called when ever mesh vertices are added or moved.
    /// </summary>
    public void RefreshMesh() {
        _mesh.vertices = _verts.ToArray();
        _mesh.triangles = _tris.ToArray();
        _mesh.uv = _uvs.ToArray();
        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();
        _meshCollider.sharedMesh = _mesh;
    }

}
