﻿using Hmxs.Toolkit;
using TMPro;
using UnityEngine;

namespace Hmxs.Scripts.Drops
{
    public class BombTime : Bomb
    {
        [SerializeField] private float explosionTime;

        protected override void Start()
        {
            base.Start();
            this.AttachTimer(
                duration: explosionTime,
                onComplete: Trigger
                );
        }

        protected override void Trigger()
        {
            base.Trigger();
            Explode();
        }
    }
}