// // -------------------------------------------------
// // Copyright@ LiJianhao
// // Author : LiJianhao
// // Date: 2024_12_1
// // License: MIT
// // -------------------------------------------------

using System;
using PurpleFlowerCore;
using PurpleFlowerCore.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Pditine.Scripts.UI
{
    public partial class Cover : UINode
    {
        [SerializeField] private Image CoverImage;
        [SerializeField] private Button PlayButton;
        [SerializeField] private Image PlayImage;
        [SerializeField] private Button QuitButton;
        [SerializeField] private Image QuitImage;
        [SerializeField] private Image BlackPanelImage;
        protected override void InitEvent()
        {
            PlayButton.onClick.AddListener(PlayButtonClick);
            QuitButton.onClick.AddListener(QuitButtonClick);
        }
        
        [SerializeField] private AudioClip bgm;

        private void Start()
        {
            AudioSystem.PlayBGM(bgm);
            FadeUtility.FadeOut(BlackPanelImage, 80);
        }
        
    
        #region UI Event
        private void PlayButtonClick()
        {
            FadeUtility.FadeInAndStay(BlackPanelImage, 80, () =>
            {
                SceneSystem.LoadScene(1);
            });
        }
        private void QuitButtonClick()
        {
            Application.Quit();
        }
        #endregion
    }
}