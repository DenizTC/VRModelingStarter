using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class LeapButtonSimple : MonoBehaviour {

    public UnityEvent OnButtonDown = new UnityEvent();

    public float ButtonDepth = 0.01f;
    public Transform ButtonFace;

    private BoxCollider boxCollider;
    private bool isColliding = false;

    private Vector3 buttonRaisedPosition;
    
    private void Start()
    {
        ButtonFace.localPosition = new Vector3(0, ButtonDepth, 0);
        buttonRaisedPosition = ButtonFace.localPosition;

        boxCollider = transform.GetComponent<BoxCollider>();
        boxCollider.center += new Vector3(0, ButtonDepth, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (isColliding || other.transform.parent.tag != "LeapFinger")
        //    return;

        ButtonDown();
    }

    private void OnTriggerExit(Collider other) {
        ButtonUp();
    }

    private void ButtonDown()
    {
        isColliding = true;
        
        ButtonFace.localPosition = Vector3.zero;

        OnButtonDown.Invoke();
    }

    private void ButtonHeld()
    {
    }

    private void ButtonUp()
    {
        isColliding = false;

        ButtonFace.localPosition = buttonRaisedPosition;
    }
}
