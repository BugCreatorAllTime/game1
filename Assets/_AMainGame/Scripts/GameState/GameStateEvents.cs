using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateEvents : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onStateChanged;

    [Space]
    [SerializeField]
    private UnityEvent onPrepare;
    [SerializeField]
    private UnityEvent onBeat;
    [SerializeField]
    private UnityEvent onOver;

    [Space]
    [SerializeField]
    private UnityEvent onWon;
    [SerializeField]
    private UnityEvent onLost;

    public void OnEnable()
    {
        if (EntryInGame.Instance != null)
        {
            EntryInGame.Instance.gameStateManager.OnStateChanged += GameStateManager_OnStateChanged;
        }
        else
        {
            enabled = false;
        }
    }

    public void OnDisable()
    {
        if (EntryInGame.Instance != null)
        {
            EntryInGame.Instance.gameStateManager.OnStateChanged -= GameStateManager_OnStateChanged;
        }
    }

    private void GameStateManager_OnStateChanged()
    {
        ///
        onStateChanged?.Invoke();

        ///
        switch (EntryInGame.Instance.gameStateManager.CurrentState)
        {
            case GameState.Prepare:
                onPrepare?.Invoke();
                break;
            case GameState.Beat:
                onBeat?.Invoke();
                break;
            case GameState.Over:
                onOver?.Invoke();
                if (EntryInGame.Instance.gameStateManager.Won_Flag)
                {
                    onWon?.Invoke();
                }
                else
                {
                    onLost?.Invoke();
                }
                break;
            default:
                throw new System.NotImplementedException();
        }
    }
}
