Easy Leap User Interface - Deniz Cetinalp

EasyLeapUI is a set of tools to quickly and easily create user interactions
using the LeapMotion controller. It contains Graphical User Interfaces (GUIs)
such as buttons, toggle groups, and sliders. Future releases will also 
contain Natural User Interfaces (NUIs). 

Before you begin:
   EasyLeapUI requires the Orion software update. It can be downloaded for
   free from: https://developer.leapmotion.com/orion
    
   The LeapPinchDetector.cs script (already included in this project) is 
   also required for detecting pinches. It can be downloaded for free with
   the Leap Pinch Utilities:
   https://developer.leapmotion.com/unity#modules
    
LeapPinchGUI:
   These are GUIs that you can interact with by pinching.
   - LeapPinchButton: 
      Allows actions to be invoked when the button is pinched.
   - LeapPinchToggle:
      A button with an 'on' and 'off' state. Can be used to perform any 
      two-state actions, such as showing and hiding an object.
   - LeapPinchToggle_Group:
      A group of toggles (LeapPinchToggle_GroupElement) in which only one 
      toggle can be 'on' at any time.
   - LeapPinchSlider:
      Allows choosing a floating point value between a specified lower and
      upper bound.

Examples:
   An example scene is available in the following directory:
   EasyLeapUI/Scenes/Example.unity

   The EasyLeapUI_Examples.cs script shows how methods are hooked to the GUIs
   and NUIs.
      
Contact:
   You can email me if you have any questions, come across any bugs, or have
   and future requests: 
   denizcetinalp@gmail.com
   https://github.com/DenizTC