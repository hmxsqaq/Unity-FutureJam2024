// // -------------------------------------------------
// // Copyright@ LiJianhao
// // Author : LiJianhao
// // Date: 2024_12_1
// // License: MIT
// // -------------------------------------------------

using System;
using PurpleFlowerCore;
using UnityEngine;

namespace Pditine.Scripts.GamePlay
{
    public class MouseMoveBalance : MonoBehaviour
    {
        [SerializeField] private Transform root;
        private void OnMouseDrag()
        {
            root.position += new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
        }

        private void OnMouseDown()
        {
            PFCLog.Info(111);
        }
    }
}