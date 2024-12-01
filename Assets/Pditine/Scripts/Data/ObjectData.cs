// // -------------------------------------------------
// // Copyright@ LiJianhao
// // Author : LiJianhao
// // Date: 2024_12_1
// // License: MIT
// // -------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using Pditine.Scripts.GamePlay;
using PurpleFlowerCore.Utility;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pditine.Scripts.Data
{
    [Configurable("ObjectData")]
    public class ObjectData : ScriptableObject
    {
        [SerializeField]private List<ObjectInfo> smallObjects = new();
        [SerializeField]private List<ObjectInfo> mediumObjects = new();
        [SerializeField]private List<ObjectInfo> bigObjects = new();
        [SerializeField]private List<ObjectInfo> specialObjects = new();
        public List<ObjectInfo> SmallObjects => smallObjects;
        public List<ObjectInfo> MediumObjects => mediumObjects;
        public List<ObjectInfo> BigObjects => bigObjects;
        public List<ObjectInfo> SpecialObjects => specialObjects;
        public List<ObjectInfo> AllObjects
        {
            get
            {
                var allObjects = new List<ObjectInfo>();
                allObjects.AddRange(smallObjects);
                allObjects.AddRange(mediumObjects);
                allObjects.AddRange(bigObjects);
                allObjects.AddRange(specialObjects);
                return allObjects;
            }
        }
        
        public ObjectInfo GetObjectInfo(int id)
        {
            foreach (var objectInfo in AllObjects)
            {
                if (objectInfo.id == id)
                {
                    return objectInfo;
                }
            }
            return null;
        }
        
        [Button]
        public void SpawnObject(int id)
        {
            ObjectSpawner.Instance.Spawn(id);
        }
        
        [Button]
        public void AddSmallObject(string path)
        {
            int index = 0;
            int id = 0;
            var objectPaths = Directory.GetFiles(path, "*.prefab");
            foreach (var objectPath in objectPaths)
            {
                var theObject = AssetDatabase.LoadAssetAtPath<GameObject>(objectPath);
                smallObjects.Add(theObject);
                smallObjects[index].id = id;
                index++;
                id++;
            }
        }
        
        [Button]
        public void AddMediumObject(string path)
        {
            int index = 0;
            int id = 11;
            var objectPaths = Directory.GetFiles(path, "*.prefab");
            foreach (var objectPath in objectPaths)
            {
                var theObject = AssetDatabase.LoadAssetAtPath<GameObject>(objectPath);
                mediumObjects.Add(theObject);
                mediumObjects[index].id = id;
                id++;
                index++;
            }
        }
        
        [Button]
        public void AddBigObject(string path)
        {
            int index = 0;
            int id = 20;
            var objectPaths = Directory.GetFiles(path, "*.prefab");
            foreach (var objectPath in objectPaths)
            {
                var theObject = AssetDatabase.LoadAssetAtPath<GameObject>(objectPath);
                bigObjects.Add(theObject);
                bigObjects[index].id = id;
                id++;
                index++;
            }
        }
        
        [Button]
        public void AddSpecialObject(string path)
        {
            int index = 0;
            int id = 30;
            var objectPaths = Directory.GetFiles(path, "*.prefab");
            foreach (var objectPath in objectPaths)
            {
                var theObject = AssetDatabase.LoadAssetAtPath<GameObject>(objectPath);
                specialObjects.Add(theObject);
                specialObjects[index].id = id;
                id++;
                index++;
            }
        }
    }

    [Serializable]
    public class ObjectInfo
    {
        public int id;
        public float gravity;
        public float mass;
        public GameObject prefab;

        public static implicit operator ObjectInfo(GameObject obj)
        {
            return new ObjectInfo
            {
                prefab = obj,
                gravity = 0,
                mass = obj.GetComponent<Rigidbody2D>().mass
            };
        }
    }
}