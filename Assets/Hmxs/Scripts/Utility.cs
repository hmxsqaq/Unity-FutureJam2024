using System;
using UnityEngine;

namespace Hmxs.Scripts
{
    public static class Utility
    {
        private static Canvas _mainCanvas;

        public static Canvas MainCanvas
        {
            get
            {
                if (_mainCanvas == null) _mainCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
                return _mainCanvas;
            }
        }

        public static Camera MainCamera
        {
            get
            {
                if (Camera.main == null) throw new NullReferenceException("Camera.main is null");
                return Camera.main;
            }
        }

        public static Vector2 GetMouseWorldPosition()
        {
            var mousePos = Input.mousePosition;
            mousePos.z = 10;
            return MainCamera.ScreenToWorldPoint(mousePos);
        }

        public static Vector2 GetMouseCanvasPosition()
        {
            var mousePos = Input.mousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(MainCanvas.transform as RectTransform, mousePos, MainCamera, out var result);
            return result;
        }

        public static Vector2 GetCanvasPosition(Vector2 worldPosition)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(MainCanvas.transform as RectTransform, MainCamera.WorldToScreenPoint(worldPosition), MainCamera, out var result);
            return result;
        }

        public static Vector2 RotateVector(Vector2 originalVector, float angleInDegrees)
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