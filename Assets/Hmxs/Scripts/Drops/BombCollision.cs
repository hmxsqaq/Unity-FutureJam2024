using System.Collections;
using UnityEngine;

namespace Hmxs.Scripts.Drops
{
    public class BombCollision : Bomb
    {
        [SerializeField] private float triggerTime;

        private float _timeCounter;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Drop _) && !HasExploded)
                Trigger();
        }

        protected override void Trigger()
        {
            base.Trigger();
            StartCoroutine(TriggerCoroutine());
        }

        private IEnumerator TriggerCoroutine()
        {
            // todo: play trigger sound
            while (_timeCounter < triggerTime)
            {
                // todo: play explosion animation
                _timeCounter += Time.deltaTime;
                yield return null;
            }
            Explode();
        }
    }
}