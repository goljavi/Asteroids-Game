using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController {
    UIManager _reference;

    public UIController(UIManager reference)
    {
        _reference = reference;
    }

	public void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) _reference.TogglePauseMenu();
    }
}
