using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateSwitcher : MonoBehaviour
{
    public void SwitchToPrepare()
    {
        EntryInGame.Instance.gameStateManager.SwitchToPrepare();
    }

    public void SwitchToBeat()
    {
        EntryInGame.Instance.gameStateManager.SwitchToBeat();
    }
}
