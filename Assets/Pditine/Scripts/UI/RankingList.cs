// // -------------------------------------------------
// // Copyright@ LiJianhao
// // Author : LiJianhao
// // Date: 2024_12_1
// // License: MIT
// // -------------------------------------------------

using System;
using System.Collections.Generic;
using Pditine.Scripts.GamePlay;
using PurpleFlowerCore;
using PurpleFlowerCore.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Pditine.Scripts.UI
{
    public partial class RankingList : UINode
    {
        [SerializeField] private Image BackgroundImage;
        [SerializeField] private Button InputButton;
        [SerializeField] private Image InputImage;
        [SerializeField] private Button RestartButton;
        [SerializeField] private Image RestartImage;
        [SerializeField] private RankingListItem Item_0;
        [SerializeField] private RankingListItem Item_1;
        [SerializeField] private RankingListItem Item_2;

        [SerializeField] private Image blackPanel;
        protected override void InitEvent()
        {
            InputButton.onClick.AddListener(InputButtonClick);
            RestartButton.onClick.AddListener(RestartButtonClick);
        }
        
        public const string FileName = "RankingList";
        [SerializeField] private List<RankingListItem> items = new();

        private void Start()
        {
            DebugSystem.AddCommand("RankingList.Refresh", Refresh);
        }

        public void Refresh()
        {
            var infos = LoadPlayerInfo();
            for (int i = 0; i < items.Count; i++)
            {
                if (i < infos.Count)
                {
                    items[i].ShowInfo(infos[i]);
                }
                else
                {
                    items[i].ShowInput(Timer.Instance.Time);
                }
            }
            if (infos[2].time > Timer.Instance.Time)
            {
                InputButton.gameObject.SetActive(false);
                RestartButton.transform.localPosition = new Vector3(0, RestartButton.transform.localPosition.y , 0);
            }
        }

        public static List<PlayerInfo> LoadPlayerInfo()
        {
            var info = SaveSystem.Load<RankingListInfo>(FileName);
            if (info == null)
            {
                info = new RankingListInfo();
                info.playerInfos = new List<PlayerInfo>();
                for (int i = 0; i < 3; i++)
                {
                    info.playerInfos.Add(new PlayerInfo());
                }
            }
            return info.playerInfos;
        }
        
        public static void SavePlayerInfo(List<PlayerInfo> playerInfos)
        {
            var info = new RankingListInfo()
            {
                playerInfos = playerInfos
            };
            SaveSystem.Save(FileName, info);
        }
    
        #region UI Event
        private void InputButtonClick()
        {
            InputButton.interactable = false;
            var infos = LoadPlayerInfo();
            int index = 4;
            for (int i = 0; i < infos.Count; i++)
            {
                if(infos[i].time < Timer.Instance.Time)
                {
                    index = i;
                    break;
                }
            }

            if (index >= 3) return;
            infos.Insert(index, new PlayerInfo()
            {
                name = "Nameless One",
                time = Timer.Instance.Time
            });
            infos.RemoveAt(3);
            SavePlayerInfo(infos);
            Refresh();
            items[index].ShowInput(Timer.Instance.Time);
        }
        private void RestartButtonClick()
        {
            FadeUtility.FadeInAndStay(blackPanel, 80, () =>
            {
                SceneSystem.LoadScene(0);
            });
        }
        #endregion
    }
    
    [Serializable]
    public struct PlayerInfo
    {
        public string name;
        public float time;
        
        public PlayerInfo(string name, float time)
        {
            this.name = name;
            this.time = time;
        }
    }
        
    public class RankingListInfo
    {
        public List<PlayerInfo> playerInfos;
    }
}