using UnityEngine;
using Leap.PinchUtility;
using UnityEngine.Events;

public class LeapPinchSlider : PinchSelectable {

    public UnityEvent _OnValueChanged = new UnityEvent();

    [SerializeField]
    private RectTransform _sliderRamp;

    [SerializeField]
    private float _lowerBound = 0;

    [SerializeField]
    private float _upperBound = 1;

    [SerializeField]
    private bool _IsVertical = true;

    private LeapPinchDetector _pinchDetector;

    private RectTransform _sliderHandle;

    private bool _pinchingHandle = false;

    /// <summary>
    /// Upper bound minus the lower bound.
    /// </summary>
    private float _sliderRange;

    //public UnityEvent OnValueChanged
    //{
    //    get
    //    {
    //        return _onValueChanged;
    //    }
    //}

    private void Awake() {
        _sliderHandle = transform.GetComponent<RectTransform>();
        _sliderRange = _upperBound - _lowerBound;

    }

    private void Update()
    {
        if (_pinchingHandle) {
            transform.position = (_IsVertical) ?
                new Vector3(transform.position.x, _pinchDetector.Position.y, transform.position.z) :
                new Vector3(_pinchDetector.Position.x, transform.position.y, transform.position.z);
            _OnValueChanged.Invoke();
        }
    }

    /// <summary>
    /// Invoked when a new pinch is detected on this transform.
    /// </summary>
    /// <param name="pinchDetector">The pinch detector.</param>
    public override void OnPinch(LeapPinchDetector pinchDetector, ref RaycastHit hit)
    {
        if (_pinchingHandle)
            return;

        _pinchDetector = pinchDetector;
        _pinchingHandle = true;
    }

    /// <summary>
    /// Invoked when the detected pinch is released.
    /// </summary>
    public override void OnPinchRelease()
    {
        _pinchingHandle = false;

        _sliderHandle.anchoredPosition = new Vector2(_sliderHandle.anchoredPosition.x, clampedHandlePosition());

    }

    /// <summary>
    /// Gets the current value of the slider.
    /// </summary>
    /// <returns></returns>
    public float GetValue() {
        float scaleFactor = (clampedHandlePosition() - (_sliderRamp.sizeDelta.y / 2f)) / -_sliderRamp.sizeDelta.y;
        return scaleFactor * _sliderRange + _lowerBound;
    }

    /// <summary>
    /// Clamps the handle position to the min and max bounds.
    /// </summary>
    /// <returns>The clamped position if outside the bounds, otherwise
    /// the unchanged position.</returns>
    private float clampedHandlePosition() {

        var handlePos = _sliderHandle.anchoredPosition.y;
        var halfRampSize = _sliderRamp.sizeDelta.y / 2f;

        if (handlePos - halfRampSize > 0)
            return halfRampSize;
        else if (handlePos - halfRampSize < -2*halfRampSize)
            return -halfRampSize;
        else
            return handlePos;
    }

}
