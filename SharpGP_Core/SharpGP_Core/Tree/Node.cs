namespace SharpGP_Core.Tree;

public abstract class Node {
	public List<Node> children = new List<Node>();
	public int indend = 0;
	protected void UpdateIndent() => children.ForEach(n => n.indend = indend + 1);
	public List<Node> getNestedNodes()
	{
		var x = new List<Node>();
		x.Add(this);
		x.AddRange(children.SelectMany(n => n.getNestedNodes()).ToList());
		return x;
	}
}