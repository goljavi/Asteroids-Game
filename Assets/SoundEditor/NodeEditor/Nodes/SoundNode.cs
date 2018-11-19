using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SoundNode : BaseNode {
    public int idValue;
    public int stopValue;

    public override string GetNodeType { get { return "Sound"; } }

    public override void DrawNode()
    {
        EditorGUILayout.LabelField("ID:");
        var iValue = EditorGUILayout.IntField(idValue);

        EditorGUILayout.LabelField("Stop At (seconds):");
        var sValue = EditorGUILayout.IntField(stopValue);
        EditorGUILayout.LabelField("(0 = AUTO)");

        if (iValue != idValue || sValue != stopValue)
		{
            idValue = iValue;
            stopValue = sValue;
			reference.NotifyChangesWereMade();
		}
	}

	public override Color GetBackgroundColor() {

        defaultColor = Color.green;
        return color;
    }


    public override string GetNodeData() {
		return idValue.ToString() + '|' + stopValue.ToString();
	}

	public override BaseNode SetNodeData(string data) {
        var dataArr = data.Split('|');
        idValue = int.Parse(dataArr[0]);
        stopValue = int.Parse(dataArr[1]);
        return this;
	}

	public override void DrawConnection() {
		if (parents.Count > 0)
		{
			foreach (var parent in parents)
			{
                if (parent == null) continue;
                SoundEditor.DrawNodeConnection(parent.windowRect, windowRect, true, Color.white);
			}
		}
	}

	public override bool CanTransitionTo(BaseNode node) {
		List<string> types = new List<string> { "Delay", "End" };

		return types.Contains(node.GetNodeType);
	}
}
