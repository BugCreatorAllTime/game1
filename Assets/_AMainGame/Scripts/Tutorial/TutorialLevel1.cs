using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevel1 : MonoBehaviour
{
    void Start()
    {
        EntryInGame.Instance.gameStateManager.OnStateChanged += GameStateManager_OnStateChanged;
    }

    private void GameStateManager_OnStateChanged()
    {
        var gameStateMan = EntryInGame.Instance.gameStateManager;
        if (gameStateMan.CurrentState == GameState.Over && gameStateMan.Won_Flag)
        {
            EntryInGame.Instance.gameStateManager.OnStateChanged -= GameStateManager_OnStateChanged;
            gameObject.SetActive(false);
        }
    }
}
