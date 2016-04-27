

/// <summary>
/// This class is used to repressent a face of an editable mesh.
/// Each face of an editable mesh contains four vert gizmos.
/// </summary>
public class LeapEditableMesh_Face {

    private VertGizmo[] _vertGizmos = new VertGizmo[4];

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
    /// Initializes a new instance of the <see cref="LeapEditableMesh_Face"/> class.
    /// </summary>
    public LeapEditableMesh_Face() {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LeapEditableMesh_Face"/> class.
    /// </summary>
    /// <param name="v0">The first VertGizmo.</param>
    /// <param name="v1">The second VertGizmo.</param>
    /// <param name="v2">The third VertGizmo.</param>
    /// <param name="v3">The forth VertGizmo.</param>
    public LeapEditableMesh_Face(VertGizmo v0, VertGizmo v1, VertGizmo v2, VertGizmo v3)
    {
        _vertGizmos[0] = v0;
        _vertGizmos[1] = v1;
        _vertGizmos[2] = v2;
        _vertGizmos[3] = v3;
    }
}
