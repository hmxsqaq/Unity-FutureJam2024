using System.Collections;
using Hmxs.Toolkit;
using Pditine.Scripts.GamePlay;
using UnityEngine;

namespace Hmxs.Scripts.Drops
{
    public class NuclearWarhead : Bomb
    {
        [SerializeField] private Transform dropsParent;
        [SerializeField] private float triggerTime;

        protected override void Trigger()
        {
            base.Trigger();
            Ani.Play($"Boom");
            this.AttachTimer(duration: triggerTime, onComplete: DestroyEverything);
        }

        private void DestroyEverything()
        {
            // todo: play nuclear sound
            // var drops = dropsParent.GetComponentsInChildren<Drop>();
            //
            // foreach (var drop in drops)
            //     if (drop != this) drop.DestroySelf();
            ObjectSpawner.Instance.Clear();
            DestroySelf();
        }
    }
}