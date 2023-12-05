using System.Collections;
using System.Collections.Generic;
using Lobby;
using UnityEngine;

public class SetupNextScene : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private NextScene nextScene;

    public void Placed()
    {
        nextScene.sceneName = sceneName;
    }

    public void Removed()
    {
        nextScene.sceneName = "";
    }
}
