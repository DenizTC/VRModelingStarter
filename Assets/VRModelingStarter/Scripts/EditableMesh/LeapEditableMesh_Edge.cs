

/// <summary>
/// This class is used to repressent an edge of an editable mesh.
/// Each edge of an editable mesh contains two vert gizmos.
/// </summary>
public class LeapEditableMesh_Edge {

    private VertGizmo[] _vertGizmos = new VertGizmo[2];

    public VertGizmo[] VertGizmos
    {
        get
        {
            return _vertGizmos;
        }

        set
        {
            _vertGizmos = value;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LeapEditableMesh_Edge"/> class.
    /// </summary>
    public LeapEditableMesh_Edge(){
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LeapEditableMesh_Edge"/> class.
    /// </summary>
    /// <param name="v0">The first VertGizmo.</param>
    /// <param name="v1">The second VertGizmo.</param>
    public LeapEditableMesh_Edge(VertGizmo v0, VertGizmo v1)
    {
        _vertGizmos[0] = v0;
        _vertGizmos[1] = v1;
    }
}
