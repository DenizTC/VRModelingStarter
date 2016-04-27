using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EasyLeapUI_Examples : MonoBehaviour {

    public Light _pointLight;

    public Light _dirLight;

    public Renderer[] _cubes;

    public LeapPinchSlider _pointLightIntensityS;

    public LeapPinchSlider _sunRotSpeedS;

    public LeapPinchToggle _cubeToggler;

    private List<Material> _cubesMat = new List<Material>();

    private bool _sunAutoRotate = false;

    private float _sunRotSpeed = 1f;

	void Start () {
        foreach (var cube in _cubes)
        {
            _cubesMat.Add(cube.material);
        }
        _cubeToggler.ToggleEvent.AddListener(ToggleCubes);
    }
	
	void Update () {
        if(_sunAutoRotate)
	        _dirLight.transform.Rotate(Vector3.up, _sunRotSpeed / 10f, Space.World);
	}

    public void ToggleSunRotation() {
        _sunAutoRotate = !_sunAutoRotate;
    }

    public void UpdatePointLightIntensity() {
        _pointLight.intensity = _pointLightIntensityS.GetValue();
    }

    public void UpdateSunRotSpeed()
    {
        _sunRotSpeed = _sunRotSpeedS.GetValue();
    }

    public void ChangeCubeColor() {
        Color newCubeCol = new Color(Random.value, Random.value, Random.value, 1f);

        foreach (var cube in _cubesMat)
        {
            cube.color = newCubeCol;
        }
    }

    public void ToggleCubes() {
        _cubes[0].enabled = _cubeToggler.GetToggled();
    }

}
