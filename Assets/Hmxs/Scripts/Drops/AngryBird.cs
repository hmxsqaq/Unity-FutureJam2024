using System;
using UnityEngine;

namespace Hmxs.Scripts.Drops
{
    [RequireComponent(typeof(Collider2D))]
    public class AngryBird : Drop
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Drop drop))
            {

            }
        }

        private void Kill(GameObject obj)
        {

        }
    }
}