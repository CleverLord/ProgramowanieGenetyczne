namespace SharpGP_Structures.Tree;

public abstract class Node {
	protected List<Node> children;
	protected int indend = 0;
	protected void UpdateIndent() => children.ForEach(n => n.indend = indend + 1);

	protected List<Node> GetNestedNodes()
	{
		var x = new List<Node>();
		x.Add(this);
		if (children != null) x.AddRange(children.SelectMany(n => n.GetNestedNodes()).ToList());
		return x;
	}
}