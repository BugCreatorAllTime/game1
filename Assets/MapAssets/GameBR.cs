using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameBR : MonoBehaviour
{
    // thành phần Home
    public GameObject pnHome;
    //public Button btnSetting;
    //public Button btnShop;
    //public Button btnGift;
    //public Button btnAds;
    //public Button btnIQT;
    //public Button btnMoveT;
    //public Button btnLogicT;
    public Button btnMap;
    //public Button btnPlay;
    //public Image imgName;

    // thành phần panel 2
    public GameObject pnMap;
    public Button pMbtnBackHome;
    public Button pMbtnBack;
    public Button pMbtnPlay2;
    public Button pMbtnNext;
    public ScrollRect pMscrMap;
    public GameObject btnLvPass;
    public GameObject btnLvUnPass;
    public GameObject btnLvNow;
    public Text cPage;  

    // panel lv3

    public GameObject pnLv3;
    public Button p3btnBackHome;
    public Button p3btnPause;
    public Button p3btnReset;
    /*public Image img1;
    public Image img2;
    public Image img3;
    public Image img1b;
    public Image img2b;
    public Image img3b;*/

    //panel lv5
   /* public GameObject pnLv5;
    public GameObject p5imgIn;
    public GameObject p5imgOut;
    public Image p5imgSoi;
    public Button p5btnBackHome;
    public SpriteMask p5Sp;*/


    //----------------------------------



    void Start()
    {
        btnMap.onClick.AddListener(() => onPMAp());
        pMbtnBackHome.onClick.AddListener(() => offPMap());
        //btnPlay.onClick.AddListener(() => onLv5());
        //p3btnBackHome.onClick.AddListener(() => offLv3());
        //p5btnBackHome.onClick.AddListener(() => offLv5());



    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void onHome()
    {
        pnHome.SetActive(true);
        //btnSetting.gameObject.SetActive(true);
        //btnShop.gameObject.SetActive(true);
        //btnGift.gameObject.SetActive(true);
        //btnAds.gameObject.SetActive(true);
        //btnIQT.gameObject.SetActive(true);
        //btnMoveT.gameObject.SetActive(true);
        //btnLogicT.gameObject.SetActive(true);
        btnMap.gameObject.SetActive(true);
        //btnPlay.gameObject.SetActive(true);
        //imgName.gameObject.SetActive(true);
    }
    void offHome()
    {
        pnHome.SetActive(false);
        //btnSetting.gameObject.SetActive(false);
        //btnShop.gameObject.SetActive(false);
        //btnGift.gameObject.SetActive(false);
        //btnAds.gameObject.SetActive(false);
        //btnIQT.gameObject.SetActive(false);
        //btnMoveT.gameObject.SetActive(false);
        //btnLogicT.gameObject.SetActive(false);
        btnMap.gameObject.SetActive(false);
        //btnPlay.gameObject.SetActive(false);
        //imgName.gameObject.SetActive(false);        
    }
    void onPMAp()
    {
        offHome();
        pnMap.SetActive(true);
        pMbtnBackHome.gameObject.SetActive(true);
        pMbtnBack.gameObject.SetActive(true);
        pMbtnPlay2.gameObject.SetActive(true);
        pMbtnNext.gameObject.SetActive(true);
        pMscrMap.gameObject.SetActive(true);
        cPage.gameObject.SetActive(true); 
    }
    void offPMap()
    {
        pMbtnBackHome.gameObject.SetActive(false);
        pMbtnBack.gameObject.SetActive(false);
        pMbtnPlay2.gameObject.SetActive(false);
        pMbtnNext.gameObject.SetActive(false);
        pMscrMap.gameObject.SetActive(false);
        cPage.gameObject.SetActive(false);
        pnMap.SetActive(false);
        onHome();
    }

    //level 3
    void onLv3()
    {
        offHome();

        /*pnLv3.SetActive(true);
        p3btnBackHome.gameObject.SetActive(true);
        p3btnPause.gameObject.SetActive(true);
        p3btnReset.gameObject.SetActive(true);
        /*img1.gameObject.SetActive(true);
        img2.gameObject.SetActive(true);
        img3.gameObject.SetActive(true);
        img1b.gameObject.SetActive(true);
        img2b.gameObject.SetActive(true);
        img3b.gameObject.SetActive(true);*/

        //Debug.Log("Vi tri 1 = " + oldImg1b.x + "-" + oldImg1b.y);

    }
    void offLv3()
    {
        /*p3btnBackHome.gameObject.SetActive(false);
        p3btnPause.gameObject.SetActive(false);
        p3btnReset.gameObject.SetActive(false);
       /* img1.gameObject.SetActive(false);
        img2.gameObject.SetActive(false);
        img3.gameObject.SetActive(false);
        img1b.gameObject.SetActive(false);
        img2b.gameObject.SetActive(false);
        img3b.gameObject.SetActive(false);*/
       // pnLv3.SetActive(false);
        onHome();
    }

    //level 5
    void onLv5()
    {
        offHome();
        /*pnLv5.SetActive(true);
        p5imgIn.SetActive(true);
        p5imgOut.SetActive(true);
        p5imgSoi.gameObject.SetActive(true);
        p5btnBackHome.gameObject.SetActive(true);
        p5Sp.gameObject.SetActive(true);*/
    }
    void offLv5()
    {
        /*p5imgIn.SetActive(false);
        p5imgOut.SetActive(false);
        p5imgSoi.gameObject.SetActive(false);
        p5btnBackHome.gameObject.SetActive(false);
        pnLv5.SetActive(false);
        p5Sp.gameObject.SetActive(false);*/
        onHome();
    }
}
