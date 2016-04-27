using UnityEngine;
using Leap;
using Leap.Unity;
using UnityEngine.Events;

public class LeapHandStateDetector : MonoBehaviour {

    public UnityEvent OnThumbsUp = new UnityEvent();
    public UnityEvent OnFistClosed = new UnityEvent();

    [SerializeField]
    protected IHandModel _handModel;

    private bool isThumbsUp = false;
    protected bool isFistClosed = false;
    protected bool _didFistClosedChange = false;
    protected bool _didThumbsUpChange = false;

    public bool IsFistClosed
    {
        get
        {
            return isFistClosed;
        }
    }

    public bool IsThumbsUp
    {
        get
        {
            return isThumbsUp;
        }
    }

    public bool DidFistClosedChangeFromLastFrame
    {
        get
        {
            return _didFistClosedChange;
        }
    }

    public bool DidThumbsUpChangeFromLastFrame
    {
        get
        {
            return _didThumbsUpChange;
        }
    }

    protected virtual void OnValidate()
    {
        if (_handModel == null)
        {
            _handModel = GetComponentInParent<IHandModel>();
        }
    }

    protected virtual void Awake()
    {
        if (_handModel == null)
        {
            Debug.LogWarning("The HandModel field of LeapClosedFistDetector was unassigned and the detector has been disabled.");
            enabled = false;
        }
    }

    protected virtual void Update()
    {
        _didFistClosedChange = false;
        _didThumbsUpChange = false;

        Hand hand = _handModel.GetLeapHand();

        if (hand == null || !_handModel.IsTracked)
        {
            changeFistClosedState(false);
            changeThumbsUpState(false);
            return;
        }

        int extendedFingers = 0;
        for (int i = 0; i < hand.Fingers.Count; i++)
        {
            if (hand.Fingers[i].IsExtended)
                extendedFingers++;
        }
        if (extendedFingers == 0)
        {
            changeFistClosedState(true);
            changeThumbsUpState(false);
        }
        else if (extendedFingers == 1 && hand.Fingers[0].IsExtended)
        {
            changeFistClosedState(false);
            changeThumbsUpState(true);
        }
        else {
            changeFistClosedState(false);
            changeThumbsUpState(false);
        }

    }

    protected virtual void changeFistClosedState(bool shouldBeFistClosed)
    {
        if (isFistClosed != shouldBeFistClosed)
        {
            isFistClosed = shouldBeFistClosed;
            _didFistClosedChange = true;
            OnFistClosed.Invoke();
            
        }
    }

    protected virtual void changeThumbsUpState(bool shouldBeThumbsUp)
    {
        if (isThumbsUp != shouldBeThumbsUp)
        {
            isThumbsUp = shouldBeThumbsUp;
            _didThumbsUpChange = true;
            OnThumbsUp.Invoke();
        }
    }

}
