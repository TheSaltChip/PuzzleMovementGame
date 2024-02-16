using Autohand;
using UnityEngine;

namespace Options
{
    public class SetOptionsOnXRRig : MonoBehaviour
    {
        [SerializeField] private GameOptions gameOptions;
        [SerializeField] private AutoHandPlayer autoHandPlayer;

        public void Set()
        {
            autoHandPlayer.rotationType = (RotationType)gameOptions.RotationType;
            autoHandPlayer.snapTurnAngle = gameOptions.SnapTurnAngle;
            autoHandPlayer.smoothTurnSpeed = gameOptions.SmoothTurnSpeed;
        }
    }
}