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
                nodeContainer[i].Edge.SetContent(nodeContainer[i], nodeContainer[Random.Range(0, i)]);
            }
        }
    }
}
