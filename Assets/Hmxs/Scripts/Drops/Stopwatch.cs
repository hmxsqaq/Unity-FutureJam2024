using HighlightPlus2D;
using Hmxs.Toolkit;
using UnityEngine;
using UnityEngine.Rendering;

namespace Hmxs.Scripts.Drops
{
    public class Stopwatch : ClickableDrop
    {
        [SerializeField] private float timeStopDuration;
        [SerializeField] private float timeScale;
        [SerializeField] private AudioClip timeStopSound;
        [SerializeField] private VolumeProfile normalVolume;
        [SerializeField] private VolumeProfile timeStopVolume;
        private Volume _volume;

        protected override void Start()
        {
            base.Start();
            _volume = FindObjectOfType<Volume>();
            if (_volume == null) Debug.LogError("Volume not found in the scene.");
        }

        protected override void Trigger()
        {
            Rb.gravityScale = 0;
            Rb.velocity = Vector2.zero;
            GetComponent<SpriteRenderer>().enabled = false;
            _volume.profile = timeStopVolume;
            Time.timeScale = timeScale;
            this.AttachTimer(
                duration: timeStopDuration,
                onComplete: () =>
                {
                    Time.timeScale = 1;
                    _volume.profile = normalVolume;
                    DestroySelf();
                },
                useRealTime: true);
        }
    }
}