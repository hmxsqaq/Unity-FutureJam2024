using System.Collections.Generic;
using System.Linq;
using PurpleFlowerCore;
using UnityEngine;

namespace Hmxs.Scripts.Drops
{
    public abstract class Bomb : Drop
    {
        [SerializeField] private GameObject explosionVfx;
        [SerializeField] private AudioClip explosionSound;

        private readonly List<Drop> _dropsInScope = new();

        protected Animator Ani;

        protected override void Start()
        {
            base.Start();
            Ani = GetComponent<Animator>();
        }

        protected bool HasExploded { get; private set; }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Drop drop) && !_dropsInScope.Contains(drop) && drop != this)
                _dropsInScope.Add(drop);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Drop drop) && _dropsInScope.Contains(drop) && drop != this)
                _dropsInScope.Remove(drop);
        }

        protected virtual void Trigger() => HasExploded = true;

        protected void Explode()
        {
            AudioSystem.PlayEffect(explosionSound, null);
            var toDestroy = _dropsInScope.Where(t => t != null).ToList();
            foreach (var drop in toDestroy)
            {
                if (drop.TryGetComponent(out Bomb bomb) && !bomb.HasExploded)
                    bomb.Trigger();
                else
                    drop.DestroySelf();
            }
            _dropsInScope.Clear();
            if (explosionVfx != null)
                Instantiate(explosionVfx, transform.position, Quaternion.identity);
            DestroySelf();
        }
    }
}