using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StartNode : BaseNode {
	public override string GetNodeType { get { return "Start"; } }

	public override bool CanTransitionTo(BaseNode node) {
		List<string> types = new List<string> { "Sound", "Delay" };

		return types.Contains(node.GetNodeType);
	}
}
