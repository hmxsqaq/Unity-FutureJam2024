// // -------------------------------------------------
// // Copyright@ LiJianhao
// // Author : LiJianhao
// // Date: 2024_11_30
// // License: MIT
// // -------------------------------------------------

using System;
using Pditine.Scripts.GamePlay;
using PurpleFlowerCore;
using UnityEngine;

namespace Pditine.Scripts.Test
{
    public class GlobalGravity : MonoBehaviour, IHasGravity
    {
        [SerializeField] private Balance balance;
        [SerializeField]private bool isLeft;
        [SerializeField]private float gravity;
        
        private void Start()
        {
            balance.AddObject(this);
            DebugSystem.AddCommand("Test/AddForce", () =>
            {
                balance.AddForce(true,30);
            });
        }

        public (bool, float) GetGravityInfo(Balance _) => (isLeft, gravity);
    }
}