// // -------------------------------------------------
// // Copyright@ LiJianhao
// // Author : LiJianhao
// // Date: 2024_12_2
// // License: MIT
// // -------------------------------------------------

using System;
using PurpleFlowerCore.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Pditine.Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Image blackPanel;
        private void Start()
        {
            FadeUtility.FadeOut(blackPanel, 80);
        }
    }
}