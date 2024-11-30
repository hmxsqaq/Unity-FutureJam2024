using Hmxs.Toolkit;
using UnityEngine;

namespace Hmxs.Scripts
{
    public class Arrow : SingletonMono<Arrow>
    {
        [SerializeField] private Transform parent;
        [SerializeField] private GameObject arrowPrefab;

        private RectTransform _arrow;

        public void Generate(Vector2 position)
        {
            if (_arrow != null) Destroy(_arrow.gameObject);
            _arrow = Instantiate(arrowPrefab, position, Quaternion.identity, parent).GetComponent<RectTransform>();
            _arrow.gameObject.SetActive(false); // prevent flickering
        }

        public void Set(Vector2 worldPoint, Vector2 startPoint, Vector2 endPoint)
        {
            if (!_arrow.gameObject.activeSelf) _arrow.gameObject.SetActive(true); // prevent flickering
            var direction = endPoint - startPoint;
            var distance = direction.magnitude;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _arrow.position = worldPoint;
            _arrow.rotation = Quaternion.Euler(0, 0, angle);
            _arrow.sizeDelta = new Vector2(distance, _arrow.sizeDelta.y);
        }

        public void Clear()
        {
            if (_arrow != null) Destroy(_arrow.gameObject);
        }
    }
}