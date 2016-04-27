using UnityEngine;

/// <summary>
/// This class locks the global scale of the transform, taking into account any
/// modifications to the scale of the transform's parent (if any).
/// </summary>
public class LockScale : MonoBehaviour {

    /// <summary>
    /// The initial scale of the transform.
    /// </summary>
    private float _initScale;

	private void Start () {

        float parentScale = transform.parent.lossyScale.x;
        _initScale = transform.localScale.x * parentScale;
    }
	
	private void Update () {
        float scale = _initScale / transform.parent.lossyScale.x;
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
