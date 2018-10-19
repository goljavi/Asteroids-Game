using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {
    ShipMotor shipMotor;
   
	void Start () {
        shipMotor = GetComponent<ShipMotor>();
    }
	
	void Update () {
        shipMotor.Move(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        if (Input.GetButton("Jump")) shipMotor.Shoot();
    }
}
