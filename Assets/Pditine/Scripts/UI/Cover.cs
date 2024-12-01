// // -------------------------------------------------
// // Copyright@ LiJianhao
// // Author : LiJianhao
// // Date: 2024_12_1
// // License: MIT
// // -------------------------------------------------

using System;
using PurpleFlowerCore;
using UnityEngine;

namespace Pditine.Scripts.UI
{
    public class Cover : MonoBehaviour
    {
        [SerializeField] private AudioClip bgm;
        private void Update()
        {
            AudioSystem.PlayBGM(bgm);
            if (Input.anyKeyDown)
            {
                SceneSystem.LoadScene(1);
            }
        }
    }
}