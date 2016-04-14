using UnityEngine;
using UnityEditor;
using UnityEditor.Graphs;

public class GraphWindowTest : EditorWindow {
	
	[MenuItem("Window/GraphWindowTest")] public static void OpenWindow () {
		GetWindow<GraphWindowTest>();
	}

	
	[SerializeField] private GraphGUITest graphGUI;

	public void OnEnable () {
		if (graphGUI == null) {
			var graph = ScriptableObject.CreateInstance<GraphTest>();
			graph.Init();
			graphGUI = ScriptableObject.CreateInstance<GraphGUITest>();
			graphGUI.graph = graph;
		}
	}

	public void OnGUI () {
		graphGUI.BeginGraphGUI(this, new Rect(0, 0, position.width, position.height));
		graphGUI.OnGraphGUI();
		graphGUI.EndGraphGUI();
	}
}

public class GraphTest : Graph {
	public void Init () {
		// Create some dummy nodes, just to show off the api
		var node1 = Node.Instance<NodeTest>();
		node1.name = "Node 100";
		node1.position = new Rect(20, 20, 0, 0);
		node1.color = Styles.Color.Blue;
		var slot1 = node1.AddOutputSlot("Out");
		this.AddNode(node1);

		var node2 = Node.Instance<NodeTest>();
		node2.name = "Node 200";
		node2.position = new Rect(220, 40, 0, 0);
		var slot2 = node2.AddInputSlot("In");
		var s1 = new Slot();
		var s2 = new Slot();
		slot2.AddEdge(new Edge(s1, s2));
		node2.color = Styles.Color.Yellow;
		var slot3 = node2.AddInputSlot("In2");
		
		this.AddNode(node2);

		// this.Connect(slot1, slot2);
	}
}

public class GraphGUITest : GraphGUI {
	public override void OnGraphGUI () {
		base.OnGraphGUI();
	}
}

public class NodeTest : Node {
	public override void NodeUI (GraphGUI host) {
		// reserve some space for drawing
		GUILayoutUtility.GetRect (100, 0);
	}
}