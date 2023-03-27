using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public InputDeviceCharacteristics _controllerCharacteristics;
    public List<GameObject> controllerPrefabs;
    InputDevice _targetDevice;
    GameObject _spawnedController;
    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> _devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(_controllerCharacteristics, _devices);

        foreach (var item in _devices)
        {
            Debug.Log(item.name + item.characteristics);
        }
        if (_devices.Count > 0)
        {
            _targetDevice = _devices[0];
            GameObject _prefab = controllerPrefabs.Find(controller => controller.name == _targetDevice.name);
            if (_prefab)
            {
                _spawnedController = Instantiate(_prefab, transform);
            }
            else
            {
                Debug.Log("Did not find correspinding controller model");
                _spawnedController = Instantiate(controllerPrefabs[0], transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool _primaryButtonValue) && _primaryButtonValue)
        {
            Debug.Log("Pressing Primary Button");
        }

        if (_targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 _primary2DAxisValue) && _primary2DAxisValue != Vector2.zero)
        {
            Debug.Log("Primary Touchpad " + _primary2DAxisValue);
        }
    }
}
