/**
* @classdesc PathNode
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/

public class PathNode : INode<PathNode>
{
    public short x;
    public short y;
    public short z;
    public short w;
    public short g;
    public PathNode Next { get; set; }
    public PathNode Previous { get; set; }
    public NodeList<PathNode> List { get; set; }
}
