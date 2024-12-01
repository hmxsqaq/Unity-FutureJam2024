using Hmxs.Scripts.GamePlay;
using Hmxs.Toolkit;
using UnityEngine;

namespace Hmxs.Scripts.Drops
{
    public class Advertisement : ClickableDrop
    {
        [SerializeField] private int reward;

        protected override void Trigger()
        {
            if (PlayerData.Blood + reward <= PlayerData.MaxBlood) PlayerData.Blood += reward;
            Events.Trigger("BloodChange", PlayerData.Blood);
            DestroySelf();
        }
    }
}