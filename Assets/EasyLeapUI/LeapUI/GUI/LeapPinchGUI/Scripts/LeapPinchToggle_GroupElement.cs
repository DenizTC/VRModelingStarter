using UnityEngine.Events;
using Leap.Unity;

[ExecuteAfter(typeof(LeapPinchToggle))]
public class LeapPinchToggle_GroupElement : LeapPinchToggle {

    public class OnValueChanged : UnityEvent<int> { };
    public OnValueChanged _OnValueChanged = new OnValueChanged();
    
    /// <summary>
    /// The unique integer for this toggle element that serves as the index for
    /// the toggle group.
    /// </summary>
    private int _id;


    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public int Id
    {
        get
        {
            return _id;
        }

        set
        {
            _id = value;
        }
    }

    /// <summary>
    /// Sets the toggled state.
    /// </summary>
    /// <param name="value"></param>
    public override void SetToggled(bool value)
    {
        if (GetToggled())
            return;
        base.SetToggled(value);
        _OnValueChanged.Invoke(Id);
    }

    /// <summary>
    /// Sets the toggled state without invoking the OnValueChanged event.
    /// </summary>
    /// <param name="value"></param>
    public void SetToggledQuiet(bool value) {
        base.SetToggled(value);
    }

}
