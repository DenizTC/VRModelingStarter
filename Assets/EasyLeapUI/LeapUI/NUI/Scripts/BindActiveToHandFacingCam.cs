using UnityEngine;
using System.Collections;

public class BindActiveToHandFacingCam : MonoBehaviour {

    [SerializeField]
    private LeapHandFacingCamDetector _handFacingCamDetector;

	private void Start () {
        _handFacingCamDetector.HandFacingCamEvent.AddListener(ToggleActive);
	}
	
	private void ToggleActive () {
        transform.gameObject.SetActive(_handFacingCamDetector.IsFacingCam);
	}
}
