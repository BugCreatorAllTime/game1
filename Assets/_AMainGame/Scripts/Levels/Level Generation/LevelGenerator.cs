using EasyExcelGenerated;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private List<int> colorCountByLevels = new List<int>();

    [Header("Test")]
    [SerializeField]
    private bool useTest;
    [SerializeField]
    private bool useTestLevelEntry;
    [SerializeField]
    private int testLevelCount;
    [SerializeField]
    private LevelEntry testLevelEntry;

    public LevelEntry GetLevelEntryForCurrent_Level(out bool is_RepeatedLevel)
    {

        var level = EntryInGame.Instance.playerData_Object.Data.Level;
        return Get_LevelEntry(level, true, out is_RepeatedLevel);
    }
    
    

    //change level to levelInGame
    private LevelEntry Get_LevelEntry(int levelInGame, bool tryLastLoadedLevel, out bool is_RepeatedLevel)
    {
        ///
#if UNITY_EDITOR
        if (useTest)
        {
            is_RepeatedLevel = true;
            
            if (!useTestLevelEntry)
            {
                return new LevelEntry(levelInGame, testLevelCount);
            }
            else
            {
                return testLevelEntry;
            }
        }
#endif

        int colorCount = GetColorCount(levelInGame);
        is_RepeatedLevel = false;
        return new LevelEntry(levelInGame, GetColorCount(levelInGame));
    }
    

    private int GetColorCount(int level)
    {
        if (level >= colorCountByLevels.Count)
        {
            return colorCountByLevels[colorCountByLevels.Count - 1];
        }
        else
        {
            return colorCountByLevels[level];
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Editor_UpdateColorCountByLevel")]
    private void Editor_UpdateColorCountByLevel()
    {
        ///
        UnityEditor.Undo.RecordObject(this, "UpdateColorCountByLevel");

        ///
        var data = Resources.Load<LevelDefitions_WaterDefition_Sheet>("EasyExcelGeneratedAsset/LevelDefitions_WaterDefition_Sheet");

        ///
        colorCountByLevels = new List<int>();

        ///
        for (int i = 0; i < data.GetDataCount(); i++)
        {
            var levelData = data.GetData(i) as WaterDefition;
            colorCountByLevels.Add(levelData.colorCount);
        }
    }
#endif
}
