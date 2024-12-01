// // -------------------------------------------------
// // Copyright@ LiJianhao
// // Author : LiJianhao
// // Date: 2024_12_1
// // License: MIT
// // -------------------------------------------------

using System.Collections.Generic;
using Pditine.Scripts.GamePlay;
using PurpleFlowerCore;
using UnityEngine;
using UnityEngine.UI;

namespace Pditine.Scripts.UI
{
    public partial class RankingListItem : UINode
    {
        [SerializeField] private Text TitleText;
        [SerializeField] private Text NameText;
        [SerializeField] private Text TimeText;
        [SerializeField] private Image NameInputImage;
        [SerializeField] private InputField NameInputInputField;
        [SerializeField] private Text PlaceholderText;
        
        [SerializeField] private int index;
        public void ShowInput(float time)
        {
            TimeText.text = time.ToString("F2");
            NameInputImage.gameObject.SetActive(true);
            NameText.gameObject.SetActive(false);
        }

        public void ShowInfo(PlayerInfo info)
        {
            NameText.text = info.name;
            TimeText.text = info.time.ToString("F2");
            NameInputImage.gameObject.SetActive(false);
            NameText.gameObject.SetActive(true);
        }
        
        public void Input(string playerName)
        {
            NameInputInputField.gameObject.SetActive(false);
            NameText.gameObject.SetActive(true);
            NameText.text = playerName;
            var infos = RankingList.LoadPlayerInfo();
            infos[index] = new PlayerInfo(playerName, Timer.Instance.Time);
            RankingList.SavePlayerInfo(infos);
        }
    }
}