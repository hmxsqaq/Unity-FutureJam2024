using System;
using PurpleFlowerCore.Utility;
using UnityEngine;

namespace Hmxs.Scripts
{
    public class Utility : SingletonMono<Utility>
    {
        [SerializeField]private Canvas _mainCanvas;

        public Canvas MainCanvas => _mainCanvas;
        
        [SerializeField]private Camera _mainCamera;

        public Camera MainCamera => _mainCamera;

        public Vector2 GetMouseWorldPosition()
        {
            var mousePos = Input.mousePosition;
            mousePos.z = 10;
            return MainCamera.ScreenToWorldPoint(mousePos);
        }

        public Vector2 GetMouseCanvasPosition()
        {
            var mousePos = Input.mousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(MainCanvas.transform as RectTransform, mousePos, MainCamera, out var result);
            return result;
        }

        public Vector2 GetCanvasPosition(Vector2 worldPosition)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(MainCanvas.transform as RectTransform, MainCamera.WorldToScreenPoint(worldPosition), MainCamera, out var result);
            return result;
        }

        public Vector2 RotateVector(Vector2 originalVector, float angleInDegrees)
        {
            float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
            float cosTheta = Mathf.Cos(angleInRadians);
            float sinTheta = Mathf.Sin(angleInRadians);
            float newX = originalVector.x * cosTheta - originalVector.y * sinTheta;
            float newY = originalVector.x * sinTheta + originalVector.y * cosTheta;
            return new Vector2(newX, newY);
        }
    }
}