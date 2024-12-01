using System;
using System.Collections.Generic;
using Hmxs.Toolkit;
using PurpleFlowerCore;
using UnityEngine;

namespace Hmxs.Scripts.GamePlay
{
    public class BloodManager : SingletonMono<BloodManager>
    {
        [SerializeField] private List<GameObject> bloods;

        private void Start()
        {
            DebugSystem.AddCommand("BloodManager/SetBlood", (int blood) => OnBloodChange(blood));
        }

        public event System.Action<int> OnBloodChangeEvent;

        private void OnEnable() => Events.AddListener<int>("BloodChange", OnBloodChange);

        private void OnDisable() => Events.RemoveListener<int>("BloodChange", OnBloodChange);

        private void OnBloodChange(int blood)
        {
            if (blood > PlayerData.MaxBlood)
                return;
            for (var i = 0; i < bloods.Count; i++)
            {
                bloods[i].SetActive(i < blood);
            }
            OnBloodChangeEvent?.Invoke(blood);
        }
    }
}