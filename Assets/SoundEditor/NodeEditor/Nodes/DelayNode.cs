using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DelayNode : BaseNode {
	public float num;
    

	public override string GetNodeType { get { return "Delay"; } }

	public override void DrawNode() {
        EditorGUILayout.LabelField("Seconds:");
        var numVal = EditorGUILayout.FloatField(num);
		if (numVal != num)
		{
            num = numVal;
			reference.NotifyChangesWereMade();
		}
	}

	public override Color GetBackgroundColor() {

        defaultColor = Color.red;
        return color;
    }


    public override string GetNodeData() {
		return num.ToString();
	}

	public override BaseNode SetNodeData(string data) {
		num = float.Parse(data);
		return this;
	}

    public override void DrawConnection()
    {
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
		List<string> types = new List<string> { "Sound" };

		return types.Contains(node.GetNodeType);
	}
}
