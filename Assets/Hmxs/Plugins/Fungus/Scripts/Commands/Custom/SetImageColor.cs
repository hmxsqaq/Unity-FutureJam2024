using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus.Custom
{
    [CommandInfo("UI",
        "Set Image Color",
        "Changes the color property of a list of UI Images.")]
    [AddComponentMenu("")]
    public class SetImageColor : Command
    {
        [Tooltip("List of UI Images to set the source image property on")]
        [SerializeField] protected List<Image> images = new();

        [SerializeField] protected Color color;

        public override void OnEnter()
        {
            for (int i = 0; i < images.Count; i++)
            {
                var image = images[i];
                image.color = color;
            }

            Continue();
        }

        public override Color GetButtonColor()
        {
            return new Color32(235, 191, 217, 255);
        }

        public override bool IsReorderableArray(string propertyName)
        {
            if (propertyName == "images")
            {
                return true;
            }

            return false;
        }

        public override void OnCommandAdded(Block parentBlock)
        {
            // Add a default empty entry
            images.Add(null);
        }
    }
}