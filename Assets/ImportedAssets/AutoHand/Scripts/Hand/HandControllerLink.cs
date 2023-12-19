using UnityEngine;

namespace Autohand {
    public class HandControllerLink : MonoBehaviour {
        public static HandControllerLink handLeft, handRight;

        public Hand hand;

        public virtual void TryHapticImpulse(float duration, float amp, float freq = 10f) {

        }
    }
}