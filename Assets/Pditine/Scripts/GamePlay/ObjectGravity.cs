// // -------------------------------------------------
// // Copyright@ LiJianhao
// // Author : LiJianhao
// // Date: 2024_11_30
// // License: MIT
// // -------------------------------------------------

using System;
using PurpleFlowerCore;
using UnityEngine;

namespace Pditine.Scripts.GamePlay
{
    public class ObjectGravity : MonoBehaviour, IHasGravity
    {
        public float gravity;
        private float _cd;
        
        private void Update()
        {
            _cd -= Time.deltaTime;
        }
        
        public (bool, float) GetGravityInfo(Balance balance)
        {
            bool isLeft = balance.transform.position.x > transform.position.x;
            float theForce = gravity * Math.Abs(balance.transform.position.x - transform.position.x);
            return (isLeft, theForce);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Balance"))
            {
                if(other.transform.position.y < transform.position.y && _cd <=0
                   && GetComponent<Rigidbody2D>().velocity.y < -0.4f)
                {
                    other.gameObject.GetComponentInParent<Balance>()
                        .AddForce(other.transform.position.x > transform.position.x, gravity * 10);
                    _cd = 1f;
                }
            }
        }
    }
}
