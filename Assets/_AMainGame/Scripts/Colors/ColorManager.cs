using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public event System.Action OnGotNewColors;

    [SerializeField]
    private Color obstacle_Color;

    [Space]
    [SerializeField]
    private List<ColorInfo> colors;

    [Header("Test")]
    [SerializeField]
    private bool useTest;
    [SerializeField]
    private int testPlayer_ColorId;
    [SerializeField]
    private int testRoadBall_ColorId;

    [System.Serializable]
    public struct ColorInfo
    {
        public Color normalColor;
        public Color obstacle_Highlighted;
        public Color player_Highlighted;
    }

    public Color ObstacleColor => obstacle_Color;
    public int Obstacle_ColorId => -1;
    public int Player_ColorId { get; private set; }
    public int RoadBall_ColorId { get; private set; }

    private int lastGameCountToGetNewColors = -1;

    public void Awake()
    {
        ///
        TryGetNewColors();

        ///
        EntryInGame.Instance.gameStateManager.OnBeforeBeat += GameStateManager_OnBeforeBeat;
    }

    private void GameStateManager_OnBeforeBeat()
    {
        TryGetNewColors();
    }

    public Color GetColor(int id)
    {
        ///
        if (id == Obstacle_ColorId)
        {
            return ObstacleColor;
        }

        ///
        return colors[id].normalColor;
    }

    public int GetRandomColorId()
    {
        return Random.Range(0, colors.Count);
    }

    public int GetNewRandomColor(int currentColorId)
    {
        ///
        int newColorId = GetRandomColorId();
        while (newColorId == currentColorId)
        {
            newColorId = GetRandomColorId();
        }

        ///
        return newColorId;
    }

    public void TryGetNewColors()
    {
        ///
        int currentGameCount = EntryInGame.Instance.playerData_Object.Data.GameCount;

        ///
        if (currentGameCount == lastGameCountToGetNewColors)
        {
            return;
        }

        ///
        lastGameCountToGetNewColors = currentGameCount;

        ///
        Player_ColorId = GetNewRandomColor(Player_ColorId);
        RoadBall_ColorId = GetNewRandomColor(Player_ColorId);

        ///
#if UNITY_EDITOR
        if (useTest)
        {
            Player_ColorId = testPlayer_ColorId;
            RoadBall_ColorId = testRoadBall_ColorId;
        }
#endif

        ///
        OnGotNewColors?.Invoke();
    }
}
