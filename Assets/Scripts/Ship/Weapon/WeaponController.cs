using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController {
    ShipWeapon _reference;
    Dictionary<KeyCode, string> _controls;
	
    public WeaponController(ShipWeapon reference)
    {
        _reference = reference;
        _controls = new Dictionary<KeyCode, string>
        {
            { KeyCode.Alpha1, BulletBehavior.Normal },
            { KeyCode.Alpha2, BulletBehavior.Lazer },
            { KeyCode.Alpha3, BulletBehavior.Bomb }
        };
    }

    public void Update()
    {
        foreach (var control in _controls)
        {
            if (Input.GetKeyDown(control.Key))
            {
                _reference.weapon = control.Value;
            }
        }
    }
}
