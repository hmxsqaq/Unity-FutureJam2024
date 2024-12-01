// // -------------------------------------------------
// // Copyright@ LiJianhao
// // Author : LiJianhao
// // Date: 2024_11_30
// // License: MIT
// // -------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pditine.Scripts.GamePlay
{
    /// <summary>
    /// 用于检测物体是否在接触天平，包括间接接触的物体,并向天平注册物体
    /// 所有可以为天平施加重力的物体都应该有IHasGravity和ObjectGravity组件，Tag为Object
    /// </summary>
    public class ObjectTouchingProxy : MonoBehaviour
    {
        [SerializeField]private Balance balance;
        [SerializeField]private Collider2D theCollider2D;
        private readonly HashSet<Collider2D> _objectBuffers = new();
        public HashSet<Collider2D> ObjectBuffers => _objectBuffers;
        private readonly ContactFilter2D _contactFilter2D = new();
        private void Update()
        {
            CheckTouching();
        }
        /// <summary>
        /// 从直接接触的物体开始,检测所有接触的物体，并缓存
        /// </summary>
        private void CheckTouching()
        {
            List<Collider2D> visitedObjects = new List<Collider2D>();
            List<Collider2D> suspiciousObjects = new List<Collider2D>();
            theCollider2D.OverlapCollider(_contactFilter2D, suspiciousObjects);
            suspiciousObjects = suspiciousObjects.Where(collider2D => collider2D.transform.position.y > theCollider2D.transform.position.y).ToList();
            while (suspiciousObjects.Count > 0)
            {
                Collider2D collider = suspiciousObjects[0];
                suspiciousObjects.RemoveAt(0);
                if(visitedObjects.Contains(collider)) continue;
                visitedObjects.Add(collider);
                if (!collider.gameObject.CompareTag("Object")) continue;
                List<Collider2D> subObjects = new List<Collider2D>();
                collider.OverlapCollider(_contactFilter2D, subObjects);
                foreach (var subObject in subObjects)
                {
                    if (!visitedObjects.Contains(subObject) 
                        && !suspiciousObjects.Contains(subObject) 
                        && subObject.transform.position.y > collider.transform.position.y)
                    {
                        suspiciousObjects.Add(subObject);
                    }
                }
                if (_objectBuffers.Contains(collider))
                { 
                    continue;
                }
                _objectBuffers.Add(collider);
                IHasGravity hasGravity = collider.GetComponent<IHasGravity>();
                if (hasGravity != null)
                {
                    balance.AddObject(hasGravity);
                }
                else
                    throw new Exception("ObjectTouchingProxy: Object has no IHasGravity component");
            }

            // 移除不再接触的物体
            List<Collider2D> objectsToRemove = new List<Collider2D>();
            foreach (var theObject in _objectBuffers)
            {
                if(visitedObjects.Contains(theObject)) continue;
                objectsToRemove.Add(theObject);
            }
            
            foreach (var theObject in objectsToRemove)
            {
                if(!theObject) continue;
                IHasGravity hasGravity = theObject.GetComponent<IHasGravity>();
                if (hasGravity != null)
                {
                    balance.RemoveObject(hasGravity);
                }
                else
                    throw new Exception("ObjectTouchingProxy: Object has no IHasGravity component");
                _objectBuffers.Remove(theObject);
            }
        }
    }
}