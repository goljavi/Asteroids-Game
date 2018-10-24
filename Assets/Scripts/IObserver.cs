using UnityEngine;
using System.Collections;

public interface IObserver<T> {
    void OnNotify(T data);
}
