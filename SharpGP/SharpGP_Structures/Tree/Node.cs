namespace SharpGP_Structures.Tree;

public abstract class Node {
	protected List<Node> children;
	protected int indend = 0;
	protected void UpdateIndent() => children.ForEach(n => n.indend = indend + 1);

	protected List<Node> GetNestedNodes()
	{
		var x = new List<Node>();
		x.Add(this);
		x.AddRange(children.SelectMany(n => n.GetNestedNodes()).ToList());
		return x;
	}

	public virtual void Grow(Program ctx){} //not every node can grow
}