// // -------------------------------------------------
// // Copyright@ LiJianhao
// // Author : LiJianhao
// // Date: 2024_11_30
// // License: MIT
// // -------------------------------------------------

using System;
using Pditine.Scripts.GamePlay;
using UnityEngine;

namespace Pditine.Scripts.Test
{
    public class TestObject : MonoBehaviour, IHasGravity
    {
        [SerializeField] private float gravity;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Balance"))
            {
                var balance = other.gameObject.GetComponentInParent<Balance>();
                balance.AddObject(this);
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Balance"))
            {
                var balance = other.gameObject.GetComponentInParent<Balance>();
                balance.RemoveObject(this);
            }
        }

        public (bool, float) GetGravityInfo(Balance balance)
        {
            bool isLeft = balance.transform.position.x > transform.position.x;
            float theForce = gravity * Math.Abs(balance.transform.position.x - transform.position.x);
            return (isLeft, theForce);
        }
    }
}
