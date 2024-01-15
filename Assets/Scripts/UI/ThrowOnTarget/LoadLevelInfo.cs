using System.IO;
using ThrowingOnTargets.Saveable;
using ThrowingOnTargets.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using Util;
using Variables;

namespace UI.ThrowOnTarget
{
    public class LoadLevelInfo : MonoBehaviour
    {
        [SerializeField] private SelectedLevelReference selectedLevelReference;
        [SerializeField] private ThrowLevelSO selectedLevel;
        [SerializeField] private IntVariable currentStageVariable;

        [SerializeField] private GameObject stageInfoPrefab;

        private ObjectPool<GameObject> stageInfoPool;

        public UnityEvent clearLevelInfo;

        private void Awake()
        {
            stageInfoPool = new ObjectPool<GameObject>(
                () =>
                {
                    var go = Instantiate(stageInfoPrefab, transform);
                    var rtp = go.GetComponent<ReturnToPool>();
                    rtp.pool = stageInfoPool;

                    return go;
                },
                g => g.SetActive(true),
                g => g.SetActive(false),
                Destroy);
        }

        public void LoadLevel()
        {
            if (selectedLevelReference == null) return;

            var levelPath = selectedLevelReference.referenceToLevelFile.FilePath;

            Directory.CreateDirectory(Path.GetDirectoryName(levelPath)!);

            var stagesSaveable = new StagesSaveable
            {
                stages = new Stage[5]
            };

            if (!File.Exists(levelPath))
            {
                Debug.LogError("File not found");
                return;
            }

            using var filestream = File.OpenRead(levelPath);
            using var fileReader = new StreamReader(filestream);

            JsonUtility.FromJsonOverwrite(fileReader.ReadToEnd(), stagesSaveable);

            selectedLevel.stages = stagesSaveable.stages;
            selectedLevel.levelName = stagesSaveable.name;

            currentStageVariable.value = 0;

            DisplayLevelInfo();
        }

        private void DisplayLevelInfo()
        {
            clearLevelInfo.Invoke();
            
            var numStages = selectedLevel.stages.Length;

            for (var i = 0; i < numStages; i++)
            {
                var stageInfo = stageInfoPool.Get();

                stageInfo.GetComponent<TMP_Text>().text =
                    $"Stage {i}: {selectedLevel.stages[i].posRots.Length} targets";
            }
        }
    }
}