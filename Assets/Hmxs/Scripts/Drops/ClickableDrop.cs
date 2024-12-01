using HighlightPlus2D;
using UnityEngine;

namespace Hmxs.Scripts.Drops
{
    public abstract class ClickableDrop : Drop
    {
        [SerializeField] private HighlightEffect2D effect2D;
        private bool _isTriggered;

        private void OnMouseEnter() => effect2D.highlighted = !_isTriggered;
        private void OnMouseExit() => effect2D.highlighted = false;

        private void OnMouseUp()
        {
            if (_isTriggered) return;
            _isTriggered = true;
            Trigger();
        }

        protected abstract void Trigger();
    }
}