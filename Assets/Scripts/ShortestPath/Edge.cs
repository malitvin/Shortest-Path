//Unity
using UnityEngine;

namespace ShortestPath
{
    /// <summary>
    /// Represents the line
    /// </summary>
    public class Edge : MonoBehaviour
    {
        private LineRenderer line;
        private LineRenderer Line
        {
            get { return line ?? (line = GetComponent<LineRenderer>()); }
        }


        private Node thisNode;
        private Node attachedNode;

        public void SetContent(Node thisNode,Node attachedNode,float edgeSize)
        {
            this.thisNode = thisNode;
            this.attachedNode = attachedNode;
            Line.startWidth = Line.endWidth = edgeSize;
        }

        private void Update()
        {
            Line.SetPosition(0, thisNode.transform.position);
            Line.SetPosition(1, attachedNode.transform.position);
        }
    }
}
