using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Undo_Step
{
    public Tube gived_Tube;
    public Tube received_Tube;
    public int Amount;
}
