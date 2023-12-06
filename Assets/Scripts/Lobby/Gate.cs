using System;
using System.Collections;
using System.Collections.Generic;
using Lobby;
using SceneTransition;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    [SerializeField] private NextScene nextScene;
    private bool _collided;
    private void OnCollisionEnter(Collision other)
    {
        if (_collided || nextScene.sceneName == "") return;
        _collided = true;
        SceneTransitionManager.Instance.LoadScene(nextScene.sceneName);
    }
}
