using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayInMap : MonoBehaviour
{
    public static PlayInMap InstanceOfPlayInMap;
    
    //private Text PassedInMap;
    //public int passedLevel;
    public Text ResourceLevel;
    // Start is called before the first frame update
    void Awake(){
      // ResourceLevel = GetComponent<Text>();
      // int.TryParse(ResourceLevel.text, out a);

            
    }
    void Start()
    {
        
       
    }
    
   
    public void LoadPassedLevel(){
        var playerData2 = EntryInGame.Instance.playerData_Object.Data;
        Debug.Log(ResourceLevel.text);
        //playerData2.SetCurrentLevel(DataLevel.a);
       // SceneManager.LoadScene("Main");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

    
    

    
    
    
    

