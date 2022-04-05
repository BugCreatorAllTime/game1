using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataSaver : MonoBehaviour
{
    public static event System.Action OnBeforeSave = delegate { };

    [SerializeField]
    float mobile_Interval = 300;
    [SerializeField]
    float standAloneInterval = 10.0f;

    float interval;

    public bool SaveThisFrame { get; set; } = false;

    public void Start()
    {
        StartCoroutine(SaveLoop());
    }

    public void LateUpdate()
    {
        if (SaveThisFrame)
        {
            ///
            Save(EntryInGame.Instance.playerData_Object);

            ///
            SaveThisFrame = false;
        }
    }

    IEnumerator SaveLoop()
    {
        ///
#if UNITY_STANDALONE
        interval = standAloneInterval;
#else
        interval = mobile_Interval;
#endif

        ///
        var playerData_Object = EntryInGame.Instance.playerData_Object;

        ///
        while (true)
        {
            ///
            yield return new WaitForSecondsRealtime(interval);

            ///
            Add_TimeSpentInGame(playerData_Object);
            Save(playerData_Object);
        }
    }

    void Add_TimeSpentInGame(PlayerDataObject playerData_Object)
    {
        playerData_Object.Data.Add_TimeSpentInGame(interval);
    }

    static void Save(PlayerDataObject playerData_Object)
    {
        ///
        OnBeforeSave();

        ///
        playerData_Object.Data.LastTimeSaved = System.DateTime.Now.Ticks;
        playerData_Object.SaveData();
    }

    public void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            Save(EntryInGame.Instance.playerData_Object);
        }
    }

    public void OnApplicationQuit()
    {
        Save(EntryInGame.Instance.playerData_Object);
    }
}
