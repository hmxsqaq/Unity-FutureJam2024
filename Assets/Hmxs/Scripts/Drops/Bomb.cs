using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hmxs.Scripts.Drops
{
    public abstract class Bomb : Drop
    {
        private readonly List<Drop> _dropsInScope = new();

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
            // todo: play explosion sound
            var toDestroy = _dropsInScope.Where(t => t != null).ToList();
            foreach (var drop in toDestroy)
            {
                if (drop.TryGetComponent(out Bomb bomb) && !bomb.HasExploded)
                    bomb.Trigger();
                else
                    drop.DestroySelf();
            }
            _dropsInScope.Clear();
            // todo: play explosion vfx
            DestroySelf();
        }
    }
}