//Unity
using UnityEngine;

//App
using GameEngineering.Common;

namespace ShortestPath
{
    public class GraphGenerator : GenericPooler
    {
        [Header("Nodes to Spawn")]
        [Tooltip("How many Nodes Will Spawn")]
        public int spawnCount = 5;

        [Space(10)]

        [Header("Vertex and Edge Values")]
        [Tooltip("Variables for Vertices and Edges")]
        [Range(0.1f,5)]
        public float vertexScale = 0.5f;
        [Range(0.01f, 1)]
        public float edgeWidth = 0.25f;

        [Space(10)]

        private Node[] nodeContainer;

        public override void Init()
        {
            base.Init();
            nodeContainer = new Node[spawnCount]; //create array
            SpawnNodes();
        }

        private void SpawnNodes()
        {
            for(int i=0; i < spawnCount; i++)
            {
                Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(
                    Random.Range(0, Screen.width),
                    Random.Range(0, Screen.height),
                    2
                    ));
                nodeContainer[i]=GetPooledObject(screenPosition) as Node;
                nodeContainer[i].SetScale(vertexScale);
                nodeContainer[i].Edge.SetContent(nodeContainer[i], nodeContainer[Random.Range(0, i)],edgeWidth);
            }
        }
    }
}
