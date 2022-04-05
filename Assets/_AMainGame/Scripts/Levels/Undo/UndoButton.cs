using System.Collections;
using System.Collections.Generic;
using FMod;
using UnityEngine;

public class UndoButton : MonoBehaviour
{
    private const int WatchAdsGain = 5;

    [SerializeField]
    private UnifiedText countText;
    [SerializeField]
    private GameObject videoObject;

    public void OnEnable()
    {
        UpdateView();
    }

    public void HandleClick()
    {
        int count = EntryInGame.Instance.playerData_Object.Data.availableUndoCount;
        if (count > 0)
        {
            if (PerformUndo())
            {
                EntryInGame.Instance.playerData_Object.Data.availableUndoCount--;
                UpdateView();
                MyAnalytics.Firebase_Undo();
            }
        }
        else
        {
            Ads_Controller.Play_RewardedVideoAds((success) =>
            {
                MyAnalytics.Firebase_ShowRewardType("More_Undo");
                if (success)
                {
                    EntryInGame.Instance.playerData_Object.Data.availableUndoCount += WatchAdsGain;
                    UpdateView();
                    MyAnalytics.Firebase_CompletedRewardType("More_Undo");
                }
            });
        }
    }

    private void UpdateView()
    {
        int count = EntryInGame.Instance.playerData_Object.Data.availableUndoCount;
        countText.Text = count.ToString();
        videoObject.SetActive(count <= 0);
    }

    private bool PerformUndo()
    {
        ///
        if (TubeMovement.IsTransfering)
        {
            return false;
        }

        ///
        return EntryInGame.Instance.undoManager.TryPerformingUndo();
    }
}
