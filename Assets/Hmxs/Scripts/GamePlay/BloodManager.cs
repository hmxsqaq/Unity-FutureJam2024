using System.Collections.Generic;
using Hmxs.Toolkit;
using UnityEngine;

namespace Hmxs.Scripts.GamePlay
{
    public class BloodManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> bloods;

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
        }
    }
}