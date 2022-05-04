using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public GameObject contentScroll;
    public RawImage bg_item_img;

    void Awake()
    {
        PrepareItem();
    }

    private void Add_Bg_Img(GameObject iteam)
    {
        GameObject bg_img = new GameObject();
        bg_img.transform.parent = iteam.transform;
      //  bg_img = gameObject.GetComponent<RawImage>();
    }

    private void PrepareItem()
    {
        GameObject gm = new GameObject();
        gm.transform.parent = contentScroll.transform;
        gm.name = "0";
       Add_Bg_Img(gm);

    }


}
