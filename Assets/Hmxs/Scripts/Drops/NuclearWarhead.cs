using UnityEngine;

namespace Hmxs.Scripts.Drops
{
    public class NuclearWarhead : Drop
    {
        [SerializeField] private Transform dropsParent;

        private void DestroyEverything()
        {
            // todo: play nuclear sound
            var drops = dropsParent.GetComponentsInChildren<Drop>();
            foreach (var drop in drops)
                drop.DestroySelf();
        }
    }
}