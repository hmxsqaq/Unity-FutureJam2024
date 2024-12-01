using Hmxs.Toolkit;
using PurpleFlowerCore;
using UnityEngine;

namespace Hmxs.Scripts.Drops
{
    public class BombCollision : Bomb
    {
        [SerializeField] private float triggerTime;
        [SerializeField] private AudioClip triggerSound;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Drop _) && !HasExploded)
                Trigger();
        }

        protected override void Trigger()
        {
            base.Trigger();
            AudioSystem.PlayEffect(triggerSound, null);
            Ani.Play($"Triggering");
            this.AttachTimer(duration: triggerTime, onComplete: Explode);
        }
    }
}