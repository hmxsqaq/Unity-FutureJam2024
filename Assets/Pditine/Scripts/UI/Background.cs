// // -------------------------------------------------
// // Copyright@ LiJianhao
// // Author : LiJianhao
// // Date: 2024_12_1
// // License: MIT
// // -------------------------------------------------

using UnityEngine;

namespace Pditine.Scripts.UI
{
    public class Background : MonoBehaviour
    {
        [SerializeField] private Transform top;
        [SerializeField] private Transform middle;
        [SerializeField] private Transform bottom;
        [SerializeField] private float topOffset;
        [SerializeField] private float middleOffset;
        [SerializeField] private float bottomOffset;

        private void Update()
        {
            UpdateBackground();
        }

        private void UpdateBackground()
        {
            Vector2 mouseOffset = (Vector2)Input.mousePosition - new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);
            mouseOffset /= new Vector2(Screen.width, Screen.height);
            top.localPosition = new Vector3(mouseOffset.x * topOffset, mouseOffset.y * topOffset, 0);
            middle.localPosition = new Vector3(mouseOffset.x * middleOffset, mouseOffset.y * middleOffset, 0);
            bottom.localPosition = new Vector3(mouseOffset.x * bottomOffset, mouseOffset.y * bottomOffset, 0);
        }
    }
}