using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Events.Coroutine
{
    public class CoroutineGameEventListener : MonoBehaviour
    {
        [Serializable] public delegate IEnumerator GameEventCoroutineHandler();

        public CoroutineGameEvent Event;
        public event GameEventCoroutineHandler response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            if (response != null)
                StartCoroutine(response());
        }
    }
}