using System;
using System.Collections.Generic;
using System.Linq;
using Events.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Audio
{
    public class DistributeUIAudio : MonoBehaviour
    {
        [SerializeField] private EventAudio eventAudio;
        [SerializeField] private AudioGameEvent audioGameEvent;
        private void Awake() {
            var gameObjects = new List<GameObject>();
            gameObjects.AddRange(gameObject.GetComponentsInChildren<Button>().Select(x => x.gameObject));
            gameObjects.AddRange(gameObject.GetComponentsInChildren<Slider>().Select(x => x.gameObject));
            gameObjects.AddRange(gameObject.GetComponentsInChildren<Dropdown>().Select(x => x.gameObject));
 
            foreach (var item in gameObjects) {
                var trigger = item.AddComponent<UIAudio>();
                trigger.EventAudio = eventAudio;
                trigger.AudioGameEvent = audioGameEvent;
            }
        }
    }
}