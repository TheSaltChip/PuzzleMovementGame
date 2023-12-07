using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    public class GameEventListenerExtender : MonoBehaviour
    {
        [SerializeField] private GameEventListener gameEventListener;
        public List<GameEvent> additionalGameEvents;

        private void OnEnable()
        {
            foreach (var gameEvent in additionalGameEvents)
            {
                gameEvent.RegisterListener(gameEventListener);
            }
        }

        private void OnDisable()
        {
            foreach (var gameEvent in additionalGameEvents)
            {
                gameEvent.UnregisterListener(gameEventListener);
            }
        }
    }
}