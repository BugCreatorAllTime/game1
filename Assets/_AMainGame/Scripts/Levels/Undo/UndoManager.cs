using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UndoManager : MonoBehaviour
{
    private List<Undo_Step> undo_Steps = new List<Undo_Step>();

    public void Awake()
    {
        EntryInGame.Instance.levelSpawner.OnSpawnedLevelEntry += LevelSpawner_OnSpawnedLevelEntry;
    }

    public void RegisterStep(Undo_Step undo_Step)
    {
        undo_Steps.Add(undo_Step);
    }

    private void LevelSpawner_OnSpawnedLevelEntry()
    {
        undo_Steps.Clear();
    }

    public bool TryPerformingUndo()
    {
        ///
        if (undo_Steps.Count == 0)
        {
            return false;
        }

        ///
        var undoStep = undo_Steps[undo_Steps.Count - 1];
        undo_Steps.RemoveAt(undo_Steps.Count - 1);

        ///
        var colorId = undoStep.received_Tube.TopColorId;
        undoStep.received_Tube.tubeView.RemoveWater(undoStep.Amount);
        undoStep.gived_Tube.tubeView.AddWater(colorId, undoStep.Amount);

        ///
        undoStep.received_Tube.CurrentWaterHeight -= undoStep.Amount;
        undoStep.gived_Tube.CurrentWaterHeight += undoStep.Amount;

        ///
        undoStep.received_Tube.tubeView.RoundLastWaterSegment();
        undoStep.gived_Tube.tubeView.RoundLastWaterSegment();

        ///
        undoStep.received_Tube.tubeMovement.UpdateCompleteStatus();
        undoStep.gived_Tube.tubeMovement.UpdateCompleteStatus();

        ///
        return true;
    }
}
