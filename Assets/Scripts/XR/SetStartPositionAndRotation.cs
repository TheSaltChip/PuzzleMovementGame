using System;
using System.Collections;
using Autohand;
using UnityEngine;

namespace XR
{
    public class SetStartPositionAndRotation : MonoBehaviour
    {
        [SerializeField] private AutoHandPlayer player;

        private IEnumerator Start()
        {
            yield return null;
            
            var g = GameObject.FindWithTag("SpawnPoint");
            player.SetPosition(g.transform.position, g.transform.rotation);
        }
    }
}