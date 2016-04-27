using UnityEngine;
using System.Collections;
using Leap.Unity;

[ExecuteAfter(typeof(LeapPinchToggle))]
[RequireComponent(typeof(LeapPinchWidget))]
public class BindInteractableToLeapToggle : MonoBehaviour {

    [SerializeField]
    private LeapPinchToggle _interactableController;

    private LeapPinchWidget _toggle;

    private void Awake() {

        if (!_interactableController)
        {
            Debug.LogWarning("No LeapPinchToggle found on " + transform.name + ". Disabling component.");
            this.enabled = false;
            return;
        }

        _toggle = transform.GetComponent<LeapPinchWidget>();
        _interactableController.ToggleEvent.AddListener(toggleInteractable);
    }

    private void toggleInteractable() {
        _toggle.SetInteractable(_interactableController.GetToggled());
    }

	
}
