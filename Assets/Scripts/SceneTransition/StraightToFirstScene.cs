using System;
using UnityEngine;

namespace SceneTransition
{
    public class StraightToFirstScene : MonoBehaviour
    {
        private void Start()
        {
            SceneTransitionManager.Instance.LoadScene("1 Start Scene");
        }
    }
}