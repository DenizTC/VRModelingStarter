using UnityEngine;
using Leap.Unity;

[ExecuteBefore(typeof(LeapPinchToggle))]
public class BindActiveToLeapToggle : MonoBehaviour {

    [SerializeField]
    private LeapPinchToggle _toggleElement;

    private void Awake() {
        if (!_toggleElement)
        {
            Debug.LogWarning("No LeapToggleGroupElement found on " + transform.name + ". Disabling component.");
            this.enabled = false;
        }
    }

	void Start () {
        _toggleElement.ToggleEvent.AddListener(toggleActive);
        toggleActive();

    }

    private void toggleActive() {
        transform.gameObject.SetActive(_toggleElement.GetToggled());
    }
}
