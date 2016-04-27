using UnityEngine;
using System.Collections;
using Leap.Unity;

[ExecuteAfter(typeof(LeapPinchToggle))]
[ExecuteAfter(typeof(LeapPinchToggle_GroupElement))]
public class LeapPinchToggle_Group : MonoBehaviour {

    public LeapPinchToggle_GroupElement[] ToggleElements;

    /// <summary>
    /// Index of the last element toggled for the ToggleElements[].
    /// </summary>
    private int _lastElementToggled = 0;

    /// <summary>
    /// True when initialized, false once the user toggles the first element.
    /// </summary>
    private bool _freshToggle = true;

	private void Awake () {
        for (int i = 0; i < ToggleElements.Length; i++)
        {
            ToggleElements[i].Id = i;
            ToggleElements[i]._OnValueChanged.AddListener(elementToggled);
            ToggleElements[i].SetToggledQuiet(false);
        }

        // First element toggled by default.
        ToggleElements[0].SetToggled(true);
    }

    /// <summary>
    /// Invoked after a new element has been toggled. The last toggled element is untoggled.
    /// </summary>
    /// <param name="index">Id and index of the most recent element toggled.</param>
    private void elementToggled(int index) {
        if (ToggleElements[index].GetToggled())
        {
            if (_freshToggle)
            {
                // Last element toggled doesn't exist.
                _freshToggle = false;
            }
            else
            {
                // Untoggle the last toggled element.
                ToggleElements[_lastElementToggled].SetToggledQuiet(false);
            }

            _lastElementToggled = index;
        }
    }

}
