//Unity
using UnityEngine;

//App
using GameEngineering.Common.Utils;
using GameEngineering.Common;

namespace ShortestPath
{
    /// <summary>
    /// Represents a vertex
    /// </summary>
    public class Node : PooledObject
    {
        private WrapAroundCameraBounds wrapMode;
        private WrapAroundCameraBounds WrapMode
        {
            get { return wrapMode ?? (wrapMode = gameObject.AddComponent<WrapAroundCameraBounds>()); }
        }

        private Edge edge;
        public Edge Edge
        {
            get { return edge ?? (edge = GetComponent<Edge>()); }
        }

        private SpriteRenderer sprite;
        private SpriteRenderer Sprite
        {
            get { return sprite ?? (sprite = GetComponent<SpriteRenderer>()); }
        }

        private Rigidbody2D rigidBody;
        private Rigidbody2D _RigidBody
        {
            get { return rigidBody ?? (rigidBody = GetComponent<Rigidbody2D>()); }
        }

        private const float REPEL_FORCE = 55;

        private void Update()
        {
            WrapMode.Wrap(_RigidBody,REPEL_FORCE);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Vector3 forceVector = (other.transform.position - transform.position).normalized * REPEL_FORCE;
            other.transform.GetComponent<Rigidbody2D>().AddForce(forceVector, ForceMode2D.Force);
            other.transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        }

        private void AddForce(Vector3 forceVector)
        {

        }
    }

}