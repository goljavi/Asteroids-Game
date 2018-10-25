using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController {
    ShipMotor _shipMotorInstance;
   
	public ShipController(ShipMotor shipMotorInstance) {
        _shipMotorInstance = shipMotorInstance;
    }
	
	public void Update () {
        _shipMotorInstance.Move(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        if (Input.GetButton("Jump")) _shipMotorInstance.Shoot();
    }
}
