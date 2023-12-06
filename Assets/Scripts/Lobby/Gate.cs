using System;
using System.Collections;
using System.Collections.Generic;
using Lobby;
using SceneTransition;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Gate : MonoBehaviour
{
    [SerializeField] private SceneInfo sceneInfo;
    private bool _collided;
    private bool _active;
    private void OnCollisionEnter(Collision other)
    {
        if (_collided || !_active) return;
        _collided = true;
        SceneTransitionManager.Instance.LoadScene(sceneInfo.sceneName);
    }

    public void ActivateGate()
    {
        _active = true;
    }

    public void DeactivateGate()
    {
        _active = false;
    }
}
