using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

/*Este script se encarga de crear un nuevo item en el AssetMenu de Unity para crear un tipo de archivo "DialogueNodeMap"
 * Este archivo almacena la lista de nodos que se va a cargar en el editor de nodos de forma serializada */
[CreateAssetMenu(fileName = "New Sound Map", menuName = "Sound Map")]
public class SoundNodeMap : ScriptableObject
{
    public List<SoundMapSerializedObject> nodes = new List<SoundMapSerializedObject>();

    [OnOpenAssetAttribute(1)]
    public static bool OpenWindow(int instanceID, int line)
    {
        var reference = EditorUtility.InstanceIDToObject(instanceID) as SoundNodeMap;
        if (reference == null) return false;

        //Abro la ventana de nodos
        EditorWindow.GetWindow<SoundEditor>().LoadAssetFile(reference);
        return true;
    }
}
