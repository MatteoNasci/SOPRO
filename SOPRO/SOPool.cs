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
        [Tooltip("Pool prefab instance")]
        public GameObject Prefab;
        /// <summary>
        /// True if you want the pool to check whenever pooled objects have been destroyed for a scene change or for other reasons
        /// </summary>
        [Tooltip("True checks whenever pooled objects have been destroyed for a scene change or for other reasons")]
        public bool PersistentPoolInScenes = true;

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
        /// Recycles the given instance. The object will not be disabled
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
        /// <param name="nullObjsRemoved">Number of objs removed when PersistentPoolInScenes is true</param>
        /// <returns>the requested element instance</returns>
        public GameObject Get(Action<GameObject> onGet, out int nullObjsRemoved)
        {
            nullObjsRemoved = 0;
            GameObject res = elements.Count == 0 ? GameObject.Instantiate(Prefab) : (PersistentPoolInScenes ? GetRemoveNullRefs(out nullObjsRemoved) : elements.Dequeue());
            onGet(res);
            res.SetActive(true);
            return res;
        }
        /// <summary>
        /// Requests an element from the pool.
        /// </summary>
        /// <param name="nullObjsRemoved">Number of objs removed when PersistentPoolInScenes is true</param>
        /// <returns>the requested element instance</returns>
        public GameObject Get(out int nullObjsRemoved)
        {
            nullObjsRemoved = 0;
            GameObject res = elements.Count == 0 ? GameObject.Instantiate(Prefab) : (PersistentPoolInScenes ? GetRemoveNullRefs(out nullObjsRemoved) : elements.Dequeue());
            res.SetActive(true);
            return res;
        }
        /// <summary>
        /// Requests an element from the pool. The object will not be enabled
        /// </summary>
        /// <param name="nullObjsRemoved">Number of objs removed when PersistentPoolInScenes is true</param>
        /// <returns>the requested element instance</returns>
        public GameObject DirectGet(out int nullObjsRemoved)
        {
            nullObjsRemoved = 0;
            return elements.Count == 0 ? GameObject.Instantiate(Prefab) : (PersistentPoolInScenes ? GetRemoveNullRefs(out nullObjsRemoved) : elements.Dequeue());
        }
        /// <summary>
        /// Requests an element from the pool. The object will not be enabled
        /// </summary>
        /// <param name="parent">transform to use as the requested element parent.</param>
        /// <param name="nullObjsRemoved">Number of objs removed when PersistentPoolInScenes is true</param>
        /// <param name="hasBeenParented">true if obj was instantiated and parent setted, false otherwise</param>
        /// <returns>the requested element instance</returns>
        public GameObject DirectGet(Transform parent, out int nullObjsRemoved, out bool hasBeenParented)
        {
            nullObjsRemoved = 0;
            if (elements.Count == 0)
            {
                hasBeenParented = true;
                return GameObject.Instantiate(Prefab, parent);
            }
            else
            {
                hasBeenParented = PersistentPoolInScenes;
                return PersistentPoolInScenes ? GetRemoveNullRefs(out nullObjsRemoved, parent) : elements.Dequeue();
            }
        }
        /// <summary>
        /// Requests an element from the pool. The object will not be enabled
        /// </summary>
        /// <param name="parent">transform to use as the requested element parent.</param>
        /// <param name="position">object position</param>
        /// <param name="rotation">object rotation</param>
        /// <param name="nullObjsRemoved">Number of objs removed when PersistentPoolInScenes is true</param>
        /// <param name="hasBeenParentedAndPositioned">true if obj was instantiated and parent and pos/rot have been setted, false otherwise</param>
        /// <returns>the requested element instance</returns>
        public GameObject DirectGet(Transform parent, Vector3 position, Quaternion rotation, out int nullObjsRemoved, out bool hasBeenParentedAndPositioned)
        {
            nullObjsRemoved = 0;
            if (elements.Count == 0)
            {
                hasBeenParentedAndPositioned = true;
                return GameObject.Instantiate(Prefab, position, rotation, parent);
            }
            else
            {
                hasBeenParentedAndPositioned = PersistentPoolInScenes;
                return PersistentPoolInScenes ? GetRemoveNullRefs(out nullObjsRemoved, parent, position, rotation) : elements.Dequeue();
            }
        }
        /// <summary>
        /// Requests an element from the pool.
        /// </summary>
        /// <param name="parent">transform to use as the requested element parent.</param>
        /// <param name="onGet">action called on element before activation</param>
        /// <param name="nullObjsRemoved">Number of objs removed when PersistentPoolInScenes is true</param>
        /// <param name="parentAlways">false to set parent only when instantiating obj, true to set parent always</param>
        /// <returns>the requested element instance</returns>
        public GameObject Get(Transform parent, Action<GameObject> onGet, out int nullObjsRemoved, bool parentAlways = false)
        {
            nullObjsRemoved = 0;
            GameObject res;
            if (elements.Count == 0)
            {
                res = GameObject.Instantiate(Prefab, parent);
            }
            else
            {
                res = PersistentPoolInScenes ? GetRemoveNullRefs(out nullObjsRemoved, parent) : elements.Dequeue();
                if (!PersistentPoolInScenes && parentAlways)
                    res.transform.parent = parent;
            }
            onGet(res);
            res.SetActive(true);
            return res;
        }
        /// <summary>
        /// Requests an element from the pool.
        /// </summary>
        /// <param name="parent">transform to use as the requested element parent.</param>
        /// <param name="nullObjsRemoved">Number of objs removed when PersistentPoolInScenes is true</param>
        /// <param name="parentAlways">false to set parent only when instantiating obj, true to set parent always</param>
        /// <returns>the requested element instance</returns>
        public GameObject Get(Transform parent, out int nullObjsRemoved, bool parentAlways = false)
        {
            nullObjsRemoved = 0;
            GameObject res;
            if (elements.Count == 0)
            {
                res = GameObject.Instantiate(Prefab, parent);
            }
            else
            {
                res = PersistentPoolInScenes ? GetRemoveNullRefs(out nullObjsRemoved, parent) : elements.Dequeue();
                if (!PersistentPoolInScenes && parentAlways)
                    res.transform.parent = parent;
            }
            res.SetActive(true);
            return res;
        }
        /// <summary>
        /// Requests an element from the pool.
        /// </summary>
        /// <param name="parent">transform to use as the requested element parent.</param>
        /// <param name="position">object position</param>
        /// <param name="rotation">object rotation</param>
        /// <param name="onGet">action called on element before activation</param>
        /// <param name="nullObjsRemoved">Number of objs removed when PersistentPoolInScenes is true</param>
        /// <param name="parentAlways">false to set parent only when instantiating obj, true to set parent always</param>
        /// <returns>the requested element instance</returns>
        public GameObject Get(Transform parent, Vector3 position, Quaternion rotation, Action<GameObject> onGet, out int nullObjsRemoved, bool parentAlways = false)
        {
            nullObjsRemoved = 0;
            GameObject res = null;
            if (elements.Count == 0)
            {
                res = GameObject.Instantiate(Prefab, position, rotation, parent);
            }
            else
            {
                res = PersistentPoolInScenes ? GetRemoveNullRefs(out nullObjsRemoved, parent, position, rotation) : elements.Dequeue();
                if (!PersistentPoolInScenes)
                {
                    res.transform.SetPositionAndRotation(position, rotation);
                    if (parentAlways)
                        res.transform.parent = parent;
                }
            }
            onGet(res);
            res.SetActive(true);
            return res;
        }
        /// <summary>
        /// Requests an element from the pool.
        /// </summary>
        /// <param name="parent">transform to use as the requested element parent.</param>
        /// <param name="position">object position</param>
        /// <param name="rotation">object rotation</param>
        /// <param name="nullObjsRemoved">Number of objs removed when PersistentPoolInScenes is true</param>
        /// <param name="parentAlways">false to set parent only when instantiating obj, true to set parent always</param>
        /// <returns>the requested element instance</returns>
        public GameObject Get(Transform parent, Vector3 position, Quaternion rotation, out int nullObjsRemoved, bool parentAlways = false)
        {
            nullObjsRemoved = 0;
            GameObject res = null;
            if (elements.Count == 0)
            {
                res = GameObject.Instantiate(Prefab, position, rotation, parent);
            }
            else
            {
                res = PersistentPoolInScenes ? GetRemoveNullRefs(out nullObjsRemoved, parent, position, rotation) : elements.Dequeue();
                if (!PersistentPoolInScenes)
                {
                    res.transform.SetPositionAndRotation(position, rotation);
                    if (parentAlways)
                        res.transform.parent = parent;
                }
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
                if (obj)
                {
                    onDestroy(obj);
                    GameObject.Destroy(obj);
                }
            }
        }
        /// <summary>
        /// Clears the pool
        /// </summary>
        public void Clear()
        {
            while (elements.Count != 0)
            {
                GameObject obj = elements.Dequeue();
                if (obj)
                    GameObject.Destroy(obj);
            }
        }
        /// <summary>
        /// Resizes the pool to the given length, invoking an action on each destroyed element (if there are any) and each created element (if there are any)
        /// </summary>
        /// <param name="onDestroy">action invoked on each destroyed element in the pool</param>
        /// <param name="parent">transform to use as the requested element parent.</param>
        /// <param name="position">object position</param>
        /// <param name="rotation">object rotation</param>
        /// <param name="onRecycle">action called on element after deactivation</param>
        /// <param name="value">target length</param>
        public void ReSize(uint value, Action<GameObject> onDestroy = null, Transform parent = null, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion), Action<GameObject> onRecycle = null)
        {
            while (elements.Count > value)
            {
                GameObject obj = elements.Dequeue();
                if (obj)
                {
                    onDestroy?.Invoke(obj);
                    GameObject.Destroy(obj);
                }
            }
            while (elements.Count < value)
            {
                GameObject obj = GameObject.Instantiate(Prefab, position, rotation, parent);
                obj.SetActive(false);
                onRecycle?.Invoke(obj);
                elements.Enqueue(obj);
            }
        }

        private GameObject GetRemoveNullRefs(out int nullObjsRemoved, Transform parent = null, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion))
        {
            nullObjsRemoved = -1;
            //Check needed to remove null objects. Objects will be automatically destroyed when changing scenes
            GameObject obj = null;
            while (!obj && elements.Count > 0)
            {
                obj = elements.Dequeue();
                nullObjsRemoved++;
            }
            if (!obj)
                obj = GameObject.Instantiate(Prefab, position, rotation, parent);
            return obj;
        }
    }
}