using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestLevelSetter : MonoBehaviour
{
    [SerializeField]
    private InputField inputField;

    public void SetLevel()
    {
        try
        {
            int level = int.Parse(inputField.text);
            EntryInGame.Instance.playerData_Object.Data.SetLevelForTest(level);
        }
        catch (System.Exception e)
        {
            throw e;
        }
    }
}
