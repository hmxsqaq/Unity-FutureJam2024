using Hmxs.Toolkit;
using MoreMountains.Feedbacks;
using Pditine.Scripts.GamePlay;
using PurpleFlowerCore;
using UnityEngine;

namespace Hmxs.Scripts.Drops
{
    public class NuclearWarhead : Bomb
    {
        [SerializeField] private float triggerTime;
        [SerializeField] private AudioClip nuclearSound;
        [SerializeField] private MMF_Player sceneShake;

        protected override void Trigger()
        {
            base.Trigger();
            Ani.Play($"Boom");
            this.AttachTimer(duration: triggerTime, onComplete: DestroyEverything);
        }

        private void DestroyEverything()
        {
            Debug.Log("Nuclear Warhead Exploded");
            AudioSystem.PlayEffect(nuclearSound, null);
            ObjectSpawner.Instance.Clear();
            DestroySelf();
        }

        public void SceneShake() => sceneShake.PlayFeedbacks();
    }
}