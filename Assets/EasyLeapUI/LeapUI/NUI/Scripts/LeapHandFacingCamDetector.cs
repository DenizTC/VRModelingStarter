using UnityEngine;
using UnityEngine.Events;
using Leap;
using Leap.PinchUtility;
using Leap.Unity;

public class LeapHandFacingCamDetector : MonoBehaviour {

    [SerializeField]
    protected IHandModel _handModel;

    [SerializeField]
    private LeapPinchDetector _pinchDetector;

    public UnityEvent HandFacingCamEvent;

    private Hand _lastHand = null;

    private bool isFacingCam = false;

    private bool facingCamLastFrame = false;

    private bool didChanged = false;

    private float _angleThreshold = 45f;

    public bool DidChanged
    {
        get
        {
            return didChanged;
        }
    }

    public bool IsFacingCam
    {
        get
        {
            return isFacingCam;
        }
    }

    void Update()
    {
        _lastHand = _handModel.GetLeapHand();
        if (_lastHand == null || !_handModel.IsTracked)
        {
            return;
        }

        if (_pinchDetector.IsPinching)
            return;

        Vector3 palmNorm = _lastHand.PalmNormal.ToVector3();

        if(Vector3.Angle(palmNorm, Camera.main.transform.position - _lastHand.PalmPosition.ToVector3()) < _angleThreshold)
            isFacingCam = true;
        else
            isFacingCam = false;

        didChanged = (facingCamLastFrame == IsFacingCam) ? false : true;
        if (didChanged)
            HandFacingCamEvent.Invoke();
        facingCamLastFrame = IsFacingCam;
    }

    public Hand GetLeapHand()
    {
        return _lastHand;
    }

}
