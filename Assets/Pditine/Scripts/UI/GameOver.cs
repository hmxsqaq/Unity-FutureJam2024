// // -------------------------------------------------
// // Copyright@ LiJianhao
// // Author : LiJianhao
// // Date: 2024_12_1
// // License: MIT
// // -------------------------------------------------

using System;
using Hmxs.Scripts.GamePlay;
using Pditine.Scripts.GamePlay;
using PurpleFlowerCore;
using PurpleFlowerCore.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Pditine.Scripts.UI
{
    public partial class GameOver : UINode
    {
        [SerializeField] private Image GameOverUIImage;
        [SerializeField] private RankingList RankingList;

        private bool _isGameOver;
        private void Start()
        {
            BloodManager.Instance.OnBloodChangeEvent += CheckGameOver;
        }
        
        private void CheckGameOver(int blood)
        {
            if (blood > 0 || _isGameOver) return;
            _isGameOver = true;
            Trigger();
        }

        private void Trigger()
        {
            Timer.Instance.isOn = false;
            GameOverUIImage.gameObject.SetActive(true);
            ScaleUtility.Lerp(new Vector3(1.1f,1.1f,1.1f), GameOverUIImage.transform, 100,()=>
            {
                DelayUtility.Delay(3, () =>
                {
                    RankingList.Refresh();
                    RankingList.gameObject.SetActive(true);
                });
            });
        }
        
    }
}