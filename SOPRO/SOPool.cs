using System;
using System.Collections.Generic;
using UnityEngine;
namespace SOPRO
{
    /// <summary>
    /// Gameobject pool
    /// </summary>
    [CreateAssetMenu(fileName = "SOPool", menuName = "SOPRO/Pool")]
    public class SOPool : ScriptableObject
    {
        /// <summary>
        /// Number of elements stored in the pool
        /// </summary>
        public int ElementsStored { get { return elements.Count; } }

        /// <summary>
        /// Prefab instance used by this pool
        /// </summary>
        public GameObject Prefab;

        private Queue<GameObject> elements = new Queue<GameObject>();

        /// <summary>
        /// Recycles the given instance
        /// </summary>
        /// <param name="toRecycle">object to recycle</param>
        public void Recycle(GameObject toRecycle)
        {
            elements.Enqueue(toRecycle);
            toRecycle.SetActive(false);
        }
        /// <summary>
        /// Recycles the given instance. No further action will be performed on the object
        /// </summary>
        /// <param name="toRecycle">object to recycle</param>
        public void DirectRecycle(GameObject toRecycle)
        {
            elements.Enqueue(toRecycle);
        }
        /// <summary>
        /// Recycles the given instance
        /// </summary>
        /// <param name="toRecycle">object to recycle</param>
        /// <param name="onRecycle">action called on element after deactivation</param>
        public void Recycle(GameObject toRecycle, Action<GameObject> onRecycle)
        {
            elements.Enqueue(toRecycle);
            toRecycle.SetActive(false);

            onRecycle(toRecycle);
        }
        /// <summary>
        /// Requests an element from the pool.
        /// </summary>
        /// <param name="onGet">action called on element before activation</param>
        /// <returns>the requested element instance</returns>
        public GameObject Get(Action<GameObject> onGet)
        {
            GameObject res = elements.Count == 0 ? GameObject.Instantiate(Prefab) : elements.Dequeue();
            onGet(res);
            res.SetActive(true);
            return res;
        }
        /// <summary>
        /// Requests an element from the pool.
        /// </summary>
        /// <returns>the requested element instance</returns>
        public GameObject Get()
        {
            GameObject res = elements.Count == 0 ? GameObject.Instantiate(Prefab) : elements.Dequeue();
            res.SetActive(true);
            return res;
        }
        /// <summary>
        /// Requests an element from the pool. No further action will be performed on the object
        /// </summary>
        /// <returns>the requested element instance</returns>
        public GameObject DirectGet()
        {
            return elements.Count == 0 ? GameObject.Instantiate(Prefab) : elements.Dequeue();
        }
        /// <summary>
        /// Requests an element from the pool.
        /// </summary>
        /// <param name="parent">transform to use as the requested element parent. Used only on instantiated elements</param>
        /// <param name="onGet">action called on element before activation</param>
        /// <returns>the requested element instance</returns>
        public GameObject Get(Transform parent, Action<GameObject> onGet)
        {
            GameObject res = elements.Count == 0 ? GameObject.Instantiate(Prefab, parent) : elements.Dequeue();
            onGet(res);
            res.SetActive(true);
            return res;
        }
        /// <summary>
        /// Requests an element from the pool.
        /// </summary>
        /// <param name="parent">transform to use as the requested element parent. Used only on instantiated elements</param>
        /// <returns>the requested element instance</returns>
        public GameObject Get(Transform parent)
        {
            GameObject res = elements.Count == 0 ? GameObject.Instantiate(Prefab, parent) : elements.Dequeue();
            res.SetActive(true);
            return res;
        }
        /// <summary>
        /// Requests an element from the pool.
        /// </summary>
        /// <param name="parent">transform to use as the requested element parent. Used only on instantiated elements</param>
        /// <param name="position">object position</param>
        /// <param name="rotation">object rotation</param>
        /// <param name="onGet">action called on element before activation</param>
        /// <returns>the requested element instance</returns>
        public GameObject Get(Transform parent, Vector3 position, Quaternion rotation, Action<GameObject> onGet)
        {
            GameObject res = null;
            if (elements.Count == 0)
            {
                res = GameObject.Instantiate(Prefab, position, rotation, parent);
            }
            else
            {
                res = elements.Dequeue();
                res.transform.SetPositionAndRotation(position, rotation);
            }
            onGet(res);
            res.SetActive(true);
            return res;
        }
        /// <summary>
        /// Requests an element from the pool.
        /// </summary>
        /// <param name="parent">transform to use as the requested element parent. Used only on instantiated elements</param>
        /// <param name="position">object position</param>
        /// <param name="rotation">object rotation</param>
        /// <returns>the requested element instance</returns>
        public GameObject Get(Transform parent, Vector3 position, Quaternion rotation)
        {
            GameObject res = null;
            if (elements.Count == 0)
            {
                res = GameObject.Instantiate(Prefab, position, rotation, parent);
            }
            else
            {
                res = elements.Dequeue();
                res.transform.SetPositionAndRotation(position, rotation);
            }
            res.SetActive(true);
            return res;
        }
        /// <summary>
        /// Clears the pool invoking an action on each element
        /// </summary>
        /// <param name="onDestroy">action invoked on each element in the pool</param>
        public void Clear(Action<GameObject> onDestroy)
        {
            while (elements.Count != 0)
            {
                GameObject obj = elements.Dequeue();
                onDestroy(obj);
                GameObject.Destroy(obj);
            }
        }
        /// <summary>
        /// Clears the pool invoking an action on each element
        /// </summary>
        public void Clear()
        {
            while (elements.Count != 0)
            {
                GameObject obj = elements.Dequeue();
                GameObject.Destroy(obj);
            }
        }
        /// <summary>
        /// Resizes the pool to the given length, invoking an action on each destroyed element (if there are any) and each created element (if there are any)
        /// </summary>
        /// <param name="onDestroy">action invoked on each destroyed element in the pool</param>
        /// <param name="parent">transform to use as the requested element parent. Used only on instantiated elements</param>
        /// <param name="position">object position</param>
        /// <param name="rotation">object rotation</param>
        /// <param name="onRecycle">action called on element after deactivation</param>
        /// <param name="value">target length</param>
        public void ReSize(uint value, Action<GameObject> onDestroy = null, Transform parent = null, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion), Action<GameObject> onRecycle = null)
        {
            while (elements.Count > value)
            {
                GameObject obj = elements.Dequeue();
                if (onDestroy != null)
                    onDestroy(obj);
                GameObject.Destroy(obj);
            }
            while (elements.Count < value)
            {
                GameObject obj = GameObject.Instantiate(Prefab, position, rotation, parent);
                obj.SetActive(false);
                if (onRecycle != null)
                    onRecycle(obj);
                elements.Enqueue(obj);
            }
        }
    }
}