// // -------------------------------------------------
// // Copyright@ LiJianhao
// // Author : LiJianhao
// // Date: 2024_11_30
// // License: MIT
// // -------------------------------------------------

using Hmxs.Scripts;
using UnityEngine;

namespace Pditine.Scripts.GamePlay
{
    public class MouseDragBalance : MonoBehaviour, IHasGravity
    {
        [SerializeField] private float mouseForce;
        [SerializeField] private Balance balance;
        [SerializeField] private Transform ctrlPoint;
        [SerializeField] private ObjectTouchingProxy objectTouchingProxy;
        [SerializeField] private float moveLimit;
        private Vector3 _endPos;
        private bool _isDragging;
        private bool _isMove;
        private Vector2 StartCanvasPoint => Utility.GetCanvasPosition(transform.position) + _offsetCanvas;
        private Vector2 _offsetWorld;
        private Vector2 _offsetCanvas;
        private Vector2 _mouseBuffer;
        
        private void Start()
        {
            balance.AddObject(this);
        }

        private void Update()
        {
            Debug.DrawLine(ctrlPoint.position, _endPos, Color.red);
            Debug.DrawLine(new Vector3(-moveLimit, 0, 0), new Vector3(moveLimit, 0, 0), Color.green);
        }

        public (bool, float) GetGravityInfo(Balance balance)
        {
            if (!_isDragging) return (false, 0);
            Vector3 force = _endPos - ctrlPoint.position;
            float forceValue = -Vector3.Dot(force, balance.Normal) * mouseForce;
            float dic = transform.position.x - ctrlPoint.position.x;
            forceValue *= Mathf.Abs(dic);
            return (dic > 0, forceValue);
        }

        private void OnMouseDrag()
        {
            if(_isDragging)
            {
                _endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Arrow.Instance.Set(ctrlPoint.position,
                    StartCanvasPoint, Utility.GetMouseCanvasPosition());
            }
            else if (_isMove)
            {
                var offset = Utility.GetMouseWorldPosition() - _mouseBuffer;
                transform.position+= new Vector3(offset.x, 0, 0);
                if (Mathf.Abs(transform.position.x) > moveLimit)
                {
                    _mouseBuffer = Utility.GetMouseWorldPosition();
                    var position = transform.position;
                    position = new Vector3(moveLimit * Mathf.Sign(position.x), position.y, position.z);
                    transform.position = position;
                    return;
                }
                foreach(var obj in objectTouchingProxy.ObjectBuffers)
                {
                    if (obj == null) continue;
                    if (obj.gameObject.CompareTag("Object"))
                    {
                        obj.transform.position += new Vector3(offset.x, 0, 0);
                    }
                }
                _mouseBuffer = Utility.GetMouseWorldPosition();
            }
        }

        private void OnMouseDown()
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Physics2D.OverlapPoint(mousePos, 1 << LayerMask.NameToLayer("Board")))
            {
                _isDragging = true;
                Arrow.Instance.Generate(Utility.GetMouseWorldPosition());
                _offsetCanvas = Utility.GetMouseCanvasPosition() - Utility.GetCanvasPosition(transform.position);
                ctrlPoint.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }else if (Physics2D.OverlapPoint(mousePos, 1 << LayerMask.NameToLayer("Trunk")))
            {
                _mouseBuffer = Utility.GetMouseWorldPosition();
                _isMove = true;
            }
        }
        
        private void OnMouseUp()
        {
            _isDragging = false;
            _isMove = false;
            Arrow.Instance.Clear();
        }
    }
}