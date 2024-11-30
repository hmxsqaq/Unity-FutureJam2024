using System;
using UnityEngine;

namespace Hmxs.Scripts
{
    public class Test : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(Utility.GetMouseCanvasPosition());
            }
        }
    }
}