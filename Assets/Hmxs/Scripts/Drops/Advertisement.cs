using UnityEngine;

namespace Hmxs.Scripts.Drops
{
    public class Advertisement : ClickableDrop
    {
        [SerializeField] private int reward;

        protected override void Trigger()
        {
            // todo: 回血
            DestroySelf();
        }
    }
}