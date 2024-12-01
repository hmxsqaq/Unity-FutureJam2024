using Hmxs.Toolkit;
using TMPro;
using UnityEngine;

namespace Hmxs.Scripts.Drops
{
    public class BombTime : Bomb
    {
        [SerializeField] private float explosionTime;
        [SerializeField] private TextMeshProUGUI timerText;

        protected override void Start()
        {
            base.Start();
            timerText.text = $"{explosionTime:00.00}";
            this.AttachTimer(
                duration: explosionTime,
                onComplete: Trigger,
                onUpdate: t =>
                {
                    timerText.text = $"{(explosionTime - t):00.00}";
                });
        }

        protected override void Trigger()
        {
            base.Trigger();
            Explode();
        }
    }
}