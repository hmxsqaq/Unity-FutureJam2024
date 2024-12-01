// // -------------------------------------------------
// // Copyright@ LiJianhao
// // Author : LiJianhao
// // Date: 2024_12_1
// // License: MIT
// // -------------------------------------------------

using System.Collections;
using Pditine.Scripts.Data;
using PurpleFlowerCore;
using PurpleFlowerCore.Utility;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Pditine.Scripts.GamePlay
{
    public class ObjectSpawner : SingletonMono<ObjectSpawner>
    {
        [SerializeField] private ObjectData objectData;
        [SerializeField] private Transform heightPoint;
        [SerializeField] private Transform LeftPoint;
        [SerializeField] private Transform RightPoint;
        [SerializeField] private Timer timer;
        [SerializeField] private float maxSpawnInterval;
        [SerializeField] private float minSpawnInterval;
        [SerializeField] private Transform objectParent;
        private void Start()
        {
            StartCoroutine(DoSpawn());
            DebugSystem.AddCommand("ObjectSpawner/RandomSpawn", () =>
            {
                Spawn();
            });
            
            DebugSystem.AddCommand("ObjectSpawner/Spawn", (int id) =>
            {
                Spawn(id);
            });
            
            DebugSystem.AddCommand("ObjectSpawner/Clear", () =>
            {
                Clear();
            });
        }

        public void Spawn()
        {
            int id = 0;
            float random = Random.Range(0, 1);
            if (timer.Time < 60)
            {
                if (random < 0.7f)
                {
                    id = Random.Range(0, objectData.SmallObjects.Count);
                }
                else
                {
                    id = Random.Range(0, objectData.MediumObjects.Count);
                }
            }
            else if (timer.Time < 120)
            {
                if (random < 0.5f)
                {
                    id = Random.Range(0, objectData.SmallObjects.Count);
                }
                else if (random < 0.8f)
                {
                    id = Random.Range(0, objectData.MediumObjects.Count);
                }
                else
                {
                    id = Random.Range(0, objectData.BigObjects.Count);
                }
            }
            else
            {
                if (random < 0.3f)
                {
                    id = Random.Range(0, objectData.SmallObjects.Count);
                }
                else if (random < 0.6f)
                {
                    id = Random.Range(0, objectData.MediumObjects.Count);
                }
                else
                {
                    id = Random.Range(0, objectData.BigObjects.Count);
                }
            }
            Spawn(id);
        }
        
        public void Clear()
        { 
            var allObj =  objectParent.GetComponentsInChildren<ObjectGravity>();
            foreach (var obj in allObj)
            {
                Destroy(obj.gameObject);
            }
        }
        
        public void Spawn(int id)
        {
            var objectInfo = objectData.GetObjectInfo(id);
            var obj = Instantiate(objectInfo.prefab, heightPoint.position, Quaternion.identity, objectParent);
            
            var rigi = obj.GetComponent<Rigidbody2D>();
            rigi.useAutoMass = false;
            rigi.mass = objectInfo.mass;
            var objectGravity = obj.GetComponent<ObjectGravity>();
            if (objectGravity == null)
            {
                objectGravity = obj.AddComponent<ObjectGravity>();
            }
            objectGravity.gravity = objectInfo.gravity <=0? rigi.mass * 5: objectInfo.gravity;
            obj.transform.position = new Vector3(Random.Range(LeftPoint.position.x, RightPoint.position.x), obj.transform.position.y,0);
            obj.tag = "Object";
            obj.layer = LayerMask.NameToLayer("Object");
        }

        private IEnumerator DoSpawn()
        {
            while (true)
            {
                if(timer.isOn)
                    Spawn();
                yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
            }
        }
    }
}