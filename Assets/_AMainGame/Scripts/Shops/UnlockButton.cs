using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnlockButton : MonoBehaviour
{
    [SerializeField]
    private int cost;
    [SerializeField]
    private UnityEngine.UI.Button button;
    [SerializeField]
    private UnifiedText costText;

    protected abstract void Unlock();
    
    public void OnEnable()
    {
        ///
        costText.Text = cost.ToString();

        ///
        button.interactable = EntryInGame.Instance.playerData_Object.Data.Coins >= cost;

        ///
        PlayerData.OnCoinsChanged += PlayerData_OnCoinsChanged;
    }

    public void HandleTap()
    {
        ///
        if (!isActiveAndEnabled)
        {
            return;
        }

        ///
        if (EntryInGame.Instance.playerData_Object.Data.SpendCoins(cost))
        {
            Unlock();
        }
    }

    private void PlayerData_OnCoinsChanged()
    {
        ///
        button.interactable = EntryInGame.Instance.playerData_Object.Data.Coins >= cost;
    }
}
