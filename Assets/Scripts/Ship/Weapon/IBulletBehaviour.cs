using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBulletBehaviour {
    void Update();
    void OnTriggerEnter2D(Collider2D other);
}
