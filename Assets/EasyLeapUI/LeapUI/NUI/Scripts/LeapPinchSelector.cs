using Leap.Unity;
using UnityEngine;

namespace Leap.PinchUtility
{

    public class LeapPinchSelector : LeapPinchDetector
    {

        [SerializeField]
        private int _layerMask;

        [SerializeField]
        private Transform _pinchPosGizmo;

        private int _pinchingFinger = 1;

        private PinchSelectable _lastPinchable = null;

        private bool _lastPinchHit = false;

        public PinchSelectable LastPinchable
        {
            get
            {
                return _lastPinchable;
            }
        }

        protected override void Update()
        {
            base.Update();

            Hand hand = _handModel.GetLeapHand();
            if (hand == null || !_handModel.IsTracked)
            {
                changePinchState(false);
                return;
            }

            var fingers = hand.Fingers;
            Vector3 thumbTip = fingers[0].TipPosition.ToVector3();
            Vector3 indexTip = fingers[1].TipPosition.ToVector3();
            _pinchPosGizmo.position = (thumbTip + indexTip) / 2f;

            if (_isPinching)
            {
         
                if (_didChange)
                {

                    var screenPoint = Camera.main.WorldToScreenPoint(_pinchPos);
                    var ray = Camera.main.ScreenPointToRay(screenPoint);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 10, _layerMask))
                    {
                        //Debug.Log(hit.transform.name);
                        _lastPinchHit = true;
                        _lastPinchable = hit.transform.GetComponent<PinchSelectable>();
                        //Debug.Log("LeapPinchSelector:: " + _lastPinchable.name + " detected");
                        _lastPinchable.OnPinch(this, ref hit);
                        
                    }
                    else
                    {
                        _lastPinchHit = false;
                    }

                }// changed from !pinching to pinching
            }// isPinching
            else
            {
                if (_didChange && _lastPinchHit) {
                    _lastPinchable.OnPinchRelease();
                    _lastPinchable = null;
                }// changed from pinching to !pinching
                _lastPinchHit = false;
            }// not pinching

        }// update


    }

}