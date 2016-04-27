using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Leap.PinchUtility;

public class LeapPinchToggle : LeapPinchWidget
{

    public UnityEvent ToggleEvent;

    [SerializeField]
    private Sprite _toggledSprite;

    [SerializeField]
    private Sprite _untoggledSprite;

    [SerializeField]
    private string _toggledText = "ON";

    [SerializeField]
    private string _untoggledText = "OFF";

    [SerializeField]
    private Text _toggleText;

    [SerializeField]
    private bool _toggledOnStart = true;

    private Color _toggleTextColor;
    private Color _toggleTextColorNotInteractable;

    private Image _toggleImage;

    private bool _toggled = false;
    private bool _interactable = true;

    /// <summary>
    /// Gets the toggled state.
    /// </summary>
    public bool GetToggled() {
        return _toggled;
    }

    /// <summary>
    /// Sets the toggled state.
    /// </summary>
    public virtual void SetToggled(bool value) {
        if (!_interactable)
            return;

        _toggled = value;
        if (_toggled)
        {
            _toggleImage.sprite = _toggledSprite;
            _toggleText.text = _toggledText;
        }
        else
        {
            _toggleImage.sprite = _untoggledSprite;
            _toggleText.text = _untoggledText;
        }
        ToggleEvent.Invoke();
    }

    /// <summary>
    /// Sets the interactibility of the toggle to true or false.
    /// </summary>
    public override void SetInteractable(bool value) {
        _interactable = value;
        if (value)
        {
            if(_toggleText)
                _toggleText.color = _toggleTextColor;
            _toggleImage.color = new Color(_toggleImage.color.r, _toggleImage.color.g, _toggleImage.color.b, 1f);
        }
        else
        {
            if (_toggleText)
                _toggleText.color = _toggleTextColorNotInteractable;
            _toggleImage.color = new Color(_toggleImage.color.r, _toggleImage.color.g, _toggleImage.color.b, 0.3f);
        }
    }

    /// <summary>
    /// Returns true if the toggle is interactible, false otherwise.
    /// </summary>
    public override bool IsInteractable()
    {
        return _interactable;
    }

    public virtual void Awake() {
        _toggleImage = transform.GetComponent<Image>();
        if (_toggleText)
        {
            _toggleTextColor = _toggleText.color;
            _toggleTextColorNotInteractable = new Color(_toggleText.color.r, _toggleText.color.b, _toggleText.color.g, 0.3f);
        }
    }

    public virtual void Start()
    {
        SetInteractable(true);
        SetToggled(_toggledOnStart);
    }

    /// <summary>
    /// Invoked when a new pinch is detected on this transform.
    /// </summary>
    /// <param name="pinchDetector">The pinch detector.</param>
    public override void OnPinch(LeapPinchDetector pinchDetector, ref RaycastHit hit)
    {
        SetToggled(!_toggled);
    }

    /// <summary>
    /// Invoked when the detected pinch is released.
    /// </summary>
    public override void OnPinchRelease()
    {
    }

}
