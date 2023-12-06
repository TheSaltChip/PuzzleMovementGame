using System.Collections;
using System.Collections.Generic;
using System.IO;
using Lobby;
using TMPro;
using UnityEditor;
using UnityEngine;

public class SelectedLevelSetup : MonoBehaviour
{
    [SerializeField] private SceneInfo sceneInfo;
    private string _name;
    private string _description;

    // Sets scene information on interactive UI button
    public void SetInfo()
    {
        _name = gameObject.GetComponentInChildren<TMP_Text>().text;
        sceneInfo.sceneName = _name;

        var levelPath = Application.dataPath + "/Scenes/SceneInfo/" + _name + ".txt";

        Directory.CreateDirectory(Path.GetDirectoryName(levelPath)!);
        
        if (!File.Exists(levelPath))
        {
            Debug.LogError("File not found");
            return;
        }

        var sceneSaveable = gameObject.AddComponent<sceneSaveable>();

        using var filestream = File.OpenRead(levelPath);
        using var fileReader = new StreamReader(filestream);

        JsonUtility.FromJsonOverwrite(fileReader.ReadToEnd(), sceneSaveable);
            
        _description = sceneSaveable.description;
        sceneInfo.sceneDescription = _description;
        print(_description);
    }

}
