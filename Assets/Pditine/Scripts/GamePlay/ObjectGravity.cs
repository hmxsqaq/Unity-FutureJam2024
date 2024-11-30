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
    public class ObjectGravity : MonoBehaviour, IHasGravity
    {
        [SerializeField] private float gravity;
        public (bool, float) GetGravityInfo(Balance balance)
        {
            bool isLeft = balance.transform.position.x > transform.position.x;
            float theForce = gravity * Math.Abs(balance.transform.position.x - transform.position.x);
            return (isLeft, theForce);
        }
    }
}
