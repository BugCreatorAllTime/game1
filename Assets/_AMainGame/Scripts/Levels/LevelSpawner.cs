using System.Collections;
using System.Collections.Generic;
using FMod;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public static LevelSpawner InstanceOfLevelSpawner;
    
    public event System.Action OnSpawnedLevelEntry;

    private int last_SpawnedGameCount = -1;
    private int last_SpawnedLevel = -1;

    public LevelEntry CurrentLevelEntry { get; private set; }

    public bool Is_RepeatedLevel { get; private set; }
    public bool Is_RepeatedLevel0 { get; private set; }
    public void Start()
    {
        ///
        SpawnLevelEntry();

        ///
        EntryInGame.Instance.gameStateManager.OnReset += GameStateManager_OnReset;
        EntryInGame.Instance.gameStateManager.OnStateChanged += GameStateManager_OnStateChanged;
    }

    private void GameStateManager_OnReset()
    {
        RespawnCurrentLevel();
    }

    public void RespawnCurrentLevel()
    {
        SpawnLevelEntry();
    }

    private void GameStateManager_OnStateChanged()
    {
        if (EntryInGame.Instance.gameStateManager.CurrentState == GameState.Beat)
        {
            ///
            var playerData = EntryInGame.Instance.playerData_Object.Data;

            ///
            if (last_SpawnedGameCount != playerData.GameCount || last_SpawnedLevel != playerData.Level)
            {
                SpawnLevelEntry();
            }
        }
    }
    Scene_Switcher scene_Switcher=new Scene_Switcher();
    [ContextMenu("SpawnLevelEntry")]
    
    public void SpawnLevelEntry()
    {
        ///
        var playerData = EntryInGame.Instance.playerData_Object.Data;
       
        ///
      
        last_SpawnedGameCount = playerData.GameCount;
        last_SpawnedLevel = playerData.Level;       
        PlayerPrefs.SetInt("playerData.Level", last_SpawnedLevel);
        PlayerPrefs.Save();

        ///
        bool isRepeatedLevel;
        var levelEntryPrototype = EntryInGame.Instance.levelGenerator.GetLevelEntryForCurrent_Level(out isRepeatedLevel);
        Is_RepeatedLevel = isRepeatedLevel;
        CurrentLevelEntry = levelEntryPrototype;    
        

        ///
        OnSpawnedLevelEntry?.Invoke();

        MyAnalytics.Firebase_Start_Level(CurrentLevelEntry.levelId);
    }
    
}
