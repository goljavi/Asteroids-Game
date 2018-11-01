using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBulletBehaviour {
    void Init();
    void Reset();
    void Update();
    void OnTriggerEnter2D(Collider2D other);
}
