using HighlightPlus2D;
using Hmxs.Toolkit;
using UnityEngine;

namespace Hmxs.Scripts.Drops
{
    public class Stopwatch : ClickableDrop
    {
        [SerializeField] private float timeStopDuration;
        [SerializeField] private float timeScale;

        protected override void Trigger()
        {
            Rb.gravityScale = 0;
            Rb.velocity = Vector2.zero;
            GetComponent<SpriteRenderer>().enabled = false;
            // todo: stop time vfx
            Time.timeScale = timeScale;
            this.AttachTimer(
                duration: timeStopDuration,
                onComplete: () =>
                {
                    Time.timeScale = 1;
                    // todo: resume time vfx
                    DestroySelf();
                },
                useRealTime: true);
        }
    }
}