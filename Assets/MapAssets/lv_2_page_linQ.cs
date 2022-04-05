using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class lv_2_page_linQ : MonoBehaviour
{
    //public GameObject pnMap;
    public Button pMbtnBackHome;
    public Button pMbtnBack;
    public Button pMbtnPlay2;
    public Button pMbtnNext;
    public ScrollRect pMscrMap;
    public GameObject btnLvPass;
    public GameObject btnLvUnPass;
    public GameObject btnLvNow;
    public Text cPage;
    int pageCurrent = 1;
    List<item> items = new List<item>();
    int pageLimit = 100;
    int AllLv = 100;

    void Start()
    {
        pMbtnBack.onClick.AddListener(() => btnNext(-1));
        pMbtnNext.onClick.AddListener(() => btnNext(1));
        CreateAllLevvel();
        btnNext(0);
        //GetPagedItems(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    class item
    {
        public GameObject ObjLevel;
        //public bool StatusLock;
    }

    public void CreateAllLevvel()
    {
        var playerData2 = EntryInGame.Instance.playerData_Object.Data;
        NextLevelButton nextLevelButton=new NextLevelButton();
        int nunUnlock = playerData2.MaxLevelReached+1; // PlayerPrefs.GetInt("MaxLevel");
        for (int i = 1; i <= AllLv; i++)
        {
            
            item newItem = new item();
            if (i == nunUnlock)
            {
                GameObject newbt = Instantiate(btnLvNow, pMscrMap.transform.GetChild(0).GetChild(0).transform, true) as GameObject;
                newbt.transform.GetChild(0).GetComponent<Text>().text = i.ToString();
                newbt.transform.name = "level_" + i;
                
                newbt.GetComponent<Button>().onClick.AddListener(async delegate
                {
                    
                });
                newItem.ObjLevel = newbt;
                //newItem.StatusLock = true;
            }
            else if (i < nunUnlock)
            {
                GameObject newbt = Instantiate(btnLvPass, pMscrMap.transform.GetChild(0).GetChild(0).transform, true) as GameObject;
                newbt.transform.GetChild(0).GetComponent<Text>().text = i.ToString();
                newbt.transform.name = "level_" + i;
                
                newbt.GetComponent<Button>().onClick.AddListener(delegate
                {
                    int selectLv = int.Parse(newbt.transform.GetChild(0).GetComponent<Text>().text);
                    Debug.Log(selectLv.ToString());
                    //LevelSpawner.InstanceOfLevelSpawner.SpawnLevelEntry2(selectLv);
                    var playerData10 = EntryInGame.Instance.playerData_Object.Data;
                    playerData10.SetCurrentLevel(selectLv-1);
                    SceneManager.LoadScene("Main");
                    
                    
                       
                });
                newItem.ObjLevel = newbt;
                //newItem.StatusLock = true;
            }
            else if (i > nunUnlock)
            {
                GameObject newbt = Instantiate(btnLvUnPass, pMscrMap.transform.GetChild(0).GetChild(0).transform, true) as GameObject;
                newbt.transform.GetChild(0).GetComponent<Text>().text = i.ToString();
                newbt.transform.name = "level_" + i;
                newbt.GetComponent<Button>().onClick.AddListener(delegate
                {
                    //Debug.Log(newbt.transform.GetChild(0).GetComponent<Text>().text);
                    //int selectLv = int.Parse(newbt.transform.GetChild(0).GetComponent<Text>().text);
                    //PlayerPrefs.SetInt("Level", selectLv - 1);
                    //PlayerPrefs.Save();
                    //SceneManager.LoadScene("LoadGame");
                });
                newItem.ObjLevel = newbt;
                //newItem.StatusLock = false;
            }
            items.Add(newItem);
        }
        //xóa đối tượng mẫu
        Destroy(btnLvPass);
        Destroy(btnLvUnPass);
        Destroy(btnLvNow);
    }

    void btnNext(int K)
    {
        pageCurrent += K; // 100/12
        if ((pageCurrent > (AllLv / pageLimit) + 1) || (pageCurrent < 1))
            pageCurrent = 1;
        cPage.text = "Page " + pageCurrent;

        var lstItems = GetPagedItems(pageCurrent);
        for (int i = 0; i < AllLv; i++) // All active -> false
        {
            items[i].ObjLevel.SetActive(false);
        }

        for (int i = 0; i < AllLv; i++) // Show level true of Page
        {
            foreach (var pt in GetPagedItems(pageCurrent))
            {
                if (items[i].ObjLevel.transform.GetChild(0).GetComponent<Text>().text == pt.ObjLevel.transform.GetChild(0).GetComponent<Text>().text)
                {
                    pt.ObjLevel.SetActive(true);
                    break;
                }
            }
        }
    }


    // then populate your items as you usually would.
    //page 1:1->12
    //page 2: 13->24
    IEnumerable<item> GetPagedItems(int pageNum)
    {
        return items.Skip((pageNum - 1) * pageLimit).Take(pageLimit);
    }
}
