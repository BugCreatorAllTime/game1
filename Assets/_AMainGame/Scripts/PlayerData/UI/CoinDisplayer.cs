using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDisplayer : ValueDisplayerUnified<double>
{
    protected override string GetString(double value)
    {
        return value.ToLargeNumberString();
    }

    protected override double GetCurrentValue()
    {
        return EntryInGame.Instance.playerData_Object.Data.Coins;
    }
}
