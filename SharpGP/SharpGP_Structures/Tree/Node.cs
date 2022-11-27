namespace SharpGP_Structures.Tree;

public abstract class Node
{
    protected Node parent;
    protected List<Node>? children;
    protected int indend = 0;
    protected void UpdateIndent() => children?.ForEach(n => n.indend = indend + 1);
    public void UpdateParents() =>
        children?.ForEach(n =>
        {
            n.parent = this;
            n.UpdateParents();
        });
    protected List<Node> GetNestedNodes()
    {
        var x = new List<Node>();
        x.Add(this);
        x.AddRange(children?.SelectMany(n => n.GetNestedNodes()).ToList() ?? new List<Node>());
        return x;
    }
    public int GetDepth()
    {
        if (children != null && children.Count > 0)
            return children.Max(n => n.GetDepth()) + 1;
        return 1;
    }
    public Node Clone()
    {
        Node newnode = (Node)MemberwiseClone(); // shallow copy of indent and Inheritance stuff (like value of the Constant, name of the Variable, etc.)
        // new node has only copied references to children (and the parent btw), so we need to clone them too
        newnode.children = children?.Select(n => n.Clone()).ToList();
        newnode.UpdateParents(); // fix parent references
        return newnode;
    }
    public static void CrossNodes(Node n1, Node n2)
    {
        //this checks interhitance, so we can't cross nodes of different types
        if (n1.GetType() != n2.GetType())
            return; // can't cross different types
        //get parent nodes
        Node p1 = n1.parent;
        Node p2 = n2.parent;
        //get index of self in parents
        int i1 = p1.children.IndexOf(n1);
        int i2 = p2.children.IndexOf(n2);
        //swap children
        p1.children[i1] = n2;
        p2.children[i2] = n1;
        //fix parents
        n1.parent = p2;
        n2.parent = p1;
    }
}