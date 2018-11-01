using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundariesChecker {
    Transform _reference;
    float _xBoundary;
    float _yBoundary;

	public BoundariesChecker(Transform reference, float xBoundary, float yBoundary)
    {
        _reference = reference;
        _xBoundary = xBoundary;
        _yBoundary = yBoundary;
    }
	
	public void Update () {
        var transf = _reference.position;
        if (_reference.position.y > _yBoundary) transf.y = -_yBoundary;
        else if (_reference.position.y < -_yBoundary) transf.y = _yBoundary;
        else if (_reference.position.x > _xBoundary) transf.x = -_xBoundary;
        else if (_reference.position.x < -_xBoundary) transf.x = _xBoundary;
        _reference.position = transf;
    }
}
