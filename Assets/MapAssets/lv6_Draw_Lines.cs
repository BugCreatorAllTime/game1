using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lv6_Draw_Lines : MonoBehaviour
{

    public int reSize;
    public float phanTram;
    public Text Ok;

    private Texture2D _Tt;
    private Color[] _Color;
    private RaycastHit2D rayHit;
    private SpriteRenderer spriRen;
    private Vector2Int lastPos;
    private bool Drawing = false;
    private int count;
    private GameObject finish;


    void Start()
    {
        spriRen = gameObject.GetComponent<SpriteRenderer>();
        var tex = spriRen.sprite.texture;
        _Tt = new Texture2D(tex.width, tex.height, TextureFormat.ARGB32, false); 
        _Tt.filterMode = FilterMode.Bilinear; //
        _Tt.wrapMode = TextureWrapMode.Clamp; //
        _Color = tex.GetPixels();
        _Tt.SetPixels(_Color);
        _Tt.Apply();
        spriRen.sprite = Sprite.Create(_Tt, spriRen.sprite.rect, new Vector2(0.5f, 0.5f));
        finish = this.transform.parent.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            rayHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(rayHit.collider != null)
            {
                UpdateT();
                Drawing = true;
            }
            else
                Drawing = false;
        }

        if(Input.GetMouseButtonUp(0))
        {
            for(int i = 0; i< _Color.Length; i++)
            {
                if(_Color[i] == Color.clear)
                {
                    count++;
                }
            }
            if((float)count/_Color.Length >= phanTram)
            {
                finish.SetActive(true);
                Ok.gameObject.SetActive(true);
            }
            count = 0;
        }
    }

    //xac dinh vi tri click mouse
    void UpdateT()
    {
        int w = _Tt.width;
        int h = _Tt.height;
        var mouseP = rayHit.point - (Vector2)rayHit.collider.bounds.min;

        mouseP.x *= w / rayHit.collider.bounds.size.x;
        mouseP.y *= h / rayHit.collider.bounds.size.y;

        Vector2Int p = new Vector2Int((int)mouseP.x, (int)mouseP.y);
        Vector2Int start = new Vector2Int();
        Vector2Int end = new Vector2Int();

        if(!Drawing)
        {
            lastPos = p;
        }

        start.x = Mathf.Clamp(Mathf.Min(p.x, lastPos.x) - reSize, 0, w);
        start.y = Mathf.Clamp(Mathf.Min(p.y, lastPos.y) - reSize, 0, h);
        end.x = Mathf.Clamp(Mathf.Min(p.x, lastPos.x) + reSize, 0, w);
        end.y = Mathf.Clamp(Mathf.Min(p.y, lastPos.y) + reSize, 0, h);

        Vector2 dir = p - lastPos;

        for(int x = start.x;x<end.x;x++)
        {
            for(int y =start.y;y<end.y;y++)
            {
                Vector2 pixel = new Vector2(x, y);
                Vector2 linesP = p;

                if(Drawing)
                {
                    float d = Vector2.Dot(pixel - lastPos, dir) / dir.sqrMagnitude; //
                    d = Mathf.Clamp01(d); // trả về giá trị 0 hoặc 1
                    linesP = Vector2.Lerp(lastPos, p, d); // 
                }

                if((pixel-linesP).sqrMagnitude <=reSize*reSize)
                {
                    _Color[x + y * w] = Color.clear;
                }
            }
        }

        lastPos = p;
        _Tt.SetPixels(_Color);
        _Tt.Apply();
        spriRen.sprite = Sprite.Create(_Tt, spriRen.sprite.rect, new Vector2(0.5f, 0.5f));
    }
}
