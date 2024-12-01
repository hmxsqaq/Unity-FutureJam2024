using System;
using UnityEngine;

namespace Hmxs.Scripts.Drops
{
    public class BlackHole : Drop
    {
        [SerializeField] private int maxContainCount;
        [SerializeField] private float force;
        private int _containCount;

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.TryGetComponent(out Drop drop)) return;
            var direction = transform.position - other.transform.position;
            drop.Rb.AddForce(direction.normalized * force, ForceMode2D.Force);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.TryGetComponent(out Drop drop)) return;
            _containCount++;
            drop.DestroySelf();
            if (_containCount > maxContainCount) DestroySelf();
        }
    }
}