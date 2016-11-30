//Unity
using UnityEngine;
using UnityEngine.Events;

//C#
using System.Collections.Generic;

namespace GameEngineering.Common
{
    public class GenericPooler : MonoBehaviour
    {
        /// <summary>
        /// Object Pool internal class 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        internal class ObjectPool<T> where T :new()
        {
            public delegate T0 UnityFunc<T0>();

            private readonly Stack<T> stack;
            private readonly UnityAction<T> onGet;
            private readonly UnityAction<T> onRemove;
            private readonly UnityFunc<T> onNew;

            public ObjectPool(int capacity, UnityFunc<T> actionNew, UnityAction<T> actionOnGet,UnityAction<T> actionOnRelease)
            {
                stack = new Stack<T>(capacity);
                onNew = actionNew;
                onGet = actionOnGet;
                onRemove = actionOnRelease;
            }

            public T Get()
            {
                T element;
                if (stack.Count == 0)
                {
                    if (onNew != null) element = onNew();
                    else element = new T();
                }
                else
                {
                    element = stack.Pop();
                }
                if (onGet != null)
                    onGet(element);
                return element;
            }

            public void Release(T element)
            {
                if (stack.Count > 0 && ReferenceEquals(stack.Peek(), element))
                   Debug.LogError("Internal error. Trying to destroy object that is already released to pool.");
                if (onRemove != null)
                    onRemove(element);
                stack.Push(element);
            }

            public void ClearPool()
            {

            }
        }

        [Header("Object Prefab")]
        public PooledObject prefab;

        [Header("Ideal Transform")]
        public Transform idealT;

        private int startingAllocatedAmount = 1;

        private ObjectPool<PooledObject> objectPool;

        private Vector3 spawnPosition;

        private void Awake()
        {
            Init();
        }

        public virtual void Init() {
            if(objectPool == null) objectPool = new ObjectPool<PooledObject>(startingAllocatedAmount, OnNew, OnGet, OnRemove);
        }

        private void Update()
        {
            OnUpdate();
        }

        public virtual void OnUpdate() { }

        public void SetIdealTransform(Transform t)
        {
            idealT = t;
        }

        public PooledObject GetPooledObject(Vector3 pos)
        {
            if (objectPool == null) Init();
            spawnPosition = pos;
            var p = objectPool.Get();
            return p;
        }

        public void RemovePooledObject(PooledObject o)
        {
            if (o == null) return;
            objectPool.Release(o);
        }

        private PooledObject OnNew()
        {
            if (idealT == null) idealT = transform;
            PooledObject p = Instantiate(prefab, idealT) as PooledObject;
            p.SetPooler(this);
            return p;
        }

        private void OnGet(PooledObject o)
        {
            o.Show();
            o.transform.position = spawnPosition;
        }

        private void OnRemove(PooledObject o)
        {
            if (o == null) return;
            o.Remove();
        }

    }
}
