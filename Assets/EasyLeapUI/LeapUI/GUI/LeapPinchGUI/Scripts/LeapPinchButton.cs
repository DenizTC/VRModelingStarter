using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Leap.PinchUtility;

public class LeapPinchButton : LeapPinchWidget
{

    public UnityEvent OnButtonDown = new UnityEvent();

    [SerializeField]
    private Sprite _buttonRaised;

    [SerializeField]
    private Sprite _buttonDown;

    [SerializeField]
    private string _raisedText = "Raised";

    [SerializeField]
    private string _pressedText = "Pressed";

    [SerializeField]
    private Text _buttonText;

    private Color _buttonTextColor;
    private Color _buttonTextColorNotInteractable;

    private Image _buttonImage;
    private bool _interactable = true;

    private void Awake()
    {
        _buttonImage = transform.GetComponent<Image>();
        if (_buttonText)
        {
            _buttonTextColor = _buttonText.color;
            _buttonTextColorNotInteractable = new Color(_buttonText.color.r, _buttonText.color.b, _buttonText.color.g, 0.3f);
        }
        
    }

    private void Start()
    {
        SetInteractable(true);
        buttonUp();
    }

    /// <summary>
    /// Invoked when a new pinch is detected on this transform.
    /// </summary>
    /// <param name="pinchDetector">The pinch detector.</param>
    public override void OnPinch(LeapPinchDetector pinchDetector, ref RaycastHit hit)
    {
        buttonDown();
    }

    /// <summary>
    /// Invoked when the detected pinch is released.
    /// </summary>
    public override void OnPinchRelease()
    {
        buttonUp();
    }

    /// <summary>
    /// Sets the interactibility of the button to true or false.
    /// </summary>
    public override void SetInteractable(bool value)
    {
        _interactable = value;
        if (value)
        {
            if (_buttonText)
                _buttonText.color = _buttonTextColor;
            _buttonImage.color = new Color(_buttonImage.color.r, _buttonImage.color.g, _buttonImage.color.b, 1f);
        }
        else
        {
            if (_buttonText)
                _buttonText.color = _buttonTextColorNotInteractable;
            _buttonImage.color = new Color(_buttonImage.color.r, _buttonImage.color.g, _buttonImage.color.b, 0.3f);
        }
    }

    /// <summary>
    /// Returns true if the button is interactible, false otherwise.
    /// </summary>
    public override bool IsInteractable()
    {
        return _interactable;
    }

    private void buttonDown()
    {
        if (!IsInteractable())
            return;

        _buttonImage.sprite = _buttonDown;
        _buttonText.text = _pressedText;
        OnButtonDown.Invoke();
    }

    private void buttonUp() {
        _buttonImage.sprite = _buttonRaised;
        _buttonText.text = _raisedText;
    }

}
