using UnityEngine;
using System.Collections;
using System;

public abstract class LeapPinchWidget : PinchSelectable {

    /// <summary>
    /// Sets the interactibility of the widget to true or false.
    /// </summary>
    public abstract void SetInteractable(bool value);

    /// <summary>
    /// Returns true if the widget is interactible, false otherwise.
    /// </summary>
    public abstract bool IsInteractable();

}
