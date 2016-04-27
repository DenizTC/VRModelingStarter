using UnityEngine;
using System.Collections;

public class GameStates : MonoBehaviour {

    public static bool DrawMode = false;

    [SerializeField]
    private LeapHandStateDetector _handStateA;

    [SerializeField]
    private LeapHandStateDetector _handStateB;

    [SerializeField]
    private LeapHandFacingCamDetector _handFacingCamA;

    [SerializeField]
    private LeapHandFacingCamDetector _handFacingCamB;

    [SerializeField]
    private Renderer _pinchPosGizmoA;

    [SerializeField]
    private Renderer _pinchPosGizmoB;

    private Light _pinchPosLightA;
    private Light _pinchPosLightB;

    public Color _DrawModeColor;
    public Color _ManipModeColor;

    private void Start() {
        _pinchPosLightA = _pinchPosGizmoA.transform.GetChild(0).transform.GetComponent<Light>();
        _pinchPosLightB = _pinchPosGizmoB.transform.GetChild(0).transform.GetComponent<Light>();

        _handFacingCamA.HandFacingCamEvent.AddListener(ToggleDrawMode);
        _handFacingCamB.HandFacingCamEvent.AddListener(ToggleDrawMode);
        _handStateA.OnThumbsUp.AddListener(ToggleDrawMode);
        _handStateB.OnThumbsUp.AddListener(ToggleDrawMode);

        updatePinchPosGizmoColor();
    }



    public void ToggleDrawMode() {
        if (_handFacingCamA.IsFacingCam && _handFacingCamB.IsFacingCam)
        {
            DrawMode = !DrawMode;
            updatePinchPosGizmoColor();
        }


    }

    private void updatePinchPosGizmoColor() {
        if (DrawMode)
        {
            _pinchPosGizmoA.material.color = _DrawModeColor;
            _pinchPosGizmoB.material.color = _DrawModeColor;

            _pinchPosGizmoA.material.SetColor("_EmissionColor", _DrawModeColor);
            _pinchPosGizmoB.material.SetColor("_EmissionColor", _DrawModeColor);

            _pinchPosLightA.color = _DrawModeColor;
            _pinchPosLightB.color = _DrawModeColor;
        }
        else
        {
            _pinchPosGizmoA.material.color = _ManipModeColor;
            _pinchPosGizmoB.material.color = _ManipModeColor;

            _pinchPosGizmoA.material.SetColor("_EmissionColor", _ManipModeColor);
            _pinchPosGizmoB.material.SetColor("_EmissionColor", _ManipModeColor);

            _pinchPosLightA.color = _ManipModeColor;
            _pinchPosLightB.color = _ManipModeColor;
        }
    }

}
