using Hmxs.Toolkit;
using UnityEngine;

namespace Hmxs.Scripts.Drops
{
    public class BombCollision : Bomb
    {
        [SerializeField] private float triggerTime;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Drop _) && !HasExploded)
                Trigger();
        }

        protected override void Trigger()
        {
            base.Trigger();
            // todo: play trigger sound
            Ani.Play($"Triggering");
            this.AttachTimer(duration: triggerTime, onComplete: Explode);
        }
    }
}