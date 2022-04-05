using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ColorView : MonoBehaviour
{
    [SerializeField]
    private ColorSource colorSource;

    [Space]
    [SerializeField]
    private bool useStaticColor;
    [SerializeField]
    private GameColor gameColor;

    public int ColorId
    {
        get
        {
            if (useStaticColor)
            {
                return (int)gameColor;
            }
            else
            {
                switch (colorSource)
                {
                    case ColorSource.Player:
                        return EntryInGame.Instance.colorManager.Player_ColorId;
                    case ColorSource.RoadBall:
                        return EntryInGame.Instance.colorManager.RoadBall_ColorId;
                    case ColorSource.Obstacle:
                        return EntryInGame.Instance.colorManager.Obstacle_ColorId;
                    default:
                        throw new System.NotImplementedException();
                }
            }
        }
    }

    protected abstract void SetColor(Color color);

    private enum ColorSource
    {
        RoadBall,
        Player,
        Obstacle
    }

    public void OnEnable()
    {
        ///
        SetColor();

        ///
        EntryInGame.Instance.colorManager.OnGotNewColors += ColorManager_OnGotNewColors;
    }

    public void OnDisable()
    {
        ///
        EntryInGame.Instance.colorManager.OnGotNewColors -= ColorManager_OnGotNewColors;
    }

    private void ColorManager_OnGotNewColors()
    {
        SetColor();
    }

    [ContextMenu("SetColor")]
    public void SetColor()
    {
        var color = EntryInGame.Instance.colorManager.GetColor(ColorId);
        SetColor(color);
    }
}
