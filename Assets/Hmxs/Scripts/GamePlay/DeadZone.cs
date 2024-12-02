using Hmxs.Scripts.Drops;
using Hmxs.Toolkit;
using Hmxs.Toolkit.Plugins.Fungus.FungusTools;
using PurpleFlowerCore;
using UnityEngine;
using UnityEngine.UI;

namespace Hmxs.Scripts.GamePlay
{
    [RequireComponent(typeof(Collider2D))]
    public class DeadZone : MonoBehaviour
    {
        [SerializeField] private Image hitUI;
        [SerializeField] private AudioClip hitSound;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out Drop drop)) return;
            switch (drop.dropName)
            {
                case "Shark": FlowchartManager.ExecuteBlock("Fall_Shark"); break;
                case "Car": FlowchartManager.ExecuteBlock("Fall_Car"); break;
                case "Earth": FlowchartManager.ExecuteBlock("Fall_Earth"); break;
                case "Train": FlowchartManager.ExecuteBlock("Fall_Train"); break;
                case "Rocket": FlowchartManager.ExecuteBlock("Fall_Rocket"); break;
                case "Titanic": FlowchartManager.ExecuteBlock("Fall_Titanic"); break;
                case "Brick": FlowchartManager.ExecuteBlock("Fall_Brick"); break;
                case "Basketball": FlowchartManager.ExecuteBlock("Fall_Basketball"); break;
                case "Skull": FlowchartManager.ExecuteBlock("Fall_Skull"); break;
                case "Armet": FlowchartManager.ExecuteBlock("Fall_Armet"); break;
                case "Heart": FlowchartManager.ExecuteBlock("Fall_Heart"); break;
                case "Money": FlowchartManager.ExecuteBlock("Fall_Money"); break;
            }

            drop.DestroySelf();
            PlayerData.Blood--;
            FlowchartManager.ExecuteBlock("ShowHitUI");
            AudioSystem.PlayEffect(hitSound, null);
            Events.Trigger("BloodChange", PlayerData.Blood);
        }
    }
}