using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Experimental.XR;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Fonco : MonoBehaviour
{
    public GameObject[] objectToPlace;
    public AudioClip[] audioToLes;
    public AudioSource audioSour;
    public GameObject contentScroll;
    public GameObject Ref;
    public GameObject quitSession,popUp;
    public ScrollRect scrollRect;
    public GameObject refPos;
    public Button btnPopUp;
    public TextMeshProUGUI txt;
    private GameObject clone;
    private ARRaycastManager arOrigin;
    private Pose placementPose ,savedPose;
    private int  nbrObj=0;
    private bool placementPoseIsValid = false;
    private bool elg= true;
    private string lang = null , cours =null, pathObjet=null;
    private List<Button> btnList = new List<Button>();
    private Data matLang;
   

    void Awake()
    {

        matLang = SaveSystem.LoadInfo();

        string line = Resources.Load<TextAsset>("storage/indiceSession").text;
        string[] param = line.Split('|');
        line = null;
        txt.text = param[matLang.GetMatLang()];
        if (matLang.GetMatLang() == 2) txt.fontSize = 50;
        param = null;
        lang = ParVar.lang;
        cours = ParVar.cours;
        contentScroll.SetActive(false);
        quitSession.SetActive(false);
        popUp.SetActive(true);
        btnPopUp.onClick.AddListener(() => Quit_Popup());
        switch (cours)
         {
             case "alphabet":
                              switch (lang)
        		             {
                         case "ar":
		            pathObjet="obj/alphabet/ar/";
		            nbrObj=28;
                             break;
                         case "tmz":
                    pathObjet= "obj/alphabet/tmz/";
		            nbrObj=33;
                             break;
                         case "fr":
		            pathObjet="obj/alphabet/latin/";	
		            nbrObj=26;
                             break;
                         case "eng":
                    pathObjet="obj/alphabet/latin/";	
		            nbrObj=26;
                             break;

         		            }

                 break;
             case "numbers":
		pathObjet= "obj/numbers/";	
		nbrObj=11;
                 break;
	     case "fruits":
		pathObjet="obj/fruits/";	
		nbrObj=7;
                 break;
             case "vegetables":
		pathObjet= "obj/vegetables/";	
		nbrObj=5;
                 break;
             case "animals":
		pathObjet="obj/animals/";	
		nbrObj=10;
                 break;
	     case "space":
		pathObjet="obj/space/";	
		nbrObj=11;
                 break;
	     case "shapes":
		pathObjet= "obj/shapes/";	
		nbrObj=3;
                 break;
         case "colors":
        pathObjet = "obj/colors/";
        nbrObj = 9;
                break;

        }
         
        
         

        objectToPlace = new GameObject[nbrObj];
        audioToLes = new AudioClip[nbrObj];
        for (int i = 0; i < nbrObj; i++) {
            var audioClip = Resources.Load<AudioClip>("ArSound/"+cours+"/" + lang + "/" + i);
            var objLoad = Resources.Load<GameObject>(pathObjet + i);
            audioToLes[i] = audioClip;
            objectToPlace[i] = objLoad;
        }

        Button btn = Ref.GetComponent<Button>();
        btn.onClick.AddListener(() => Rest());
        for (int i = 0; i < nbrObj; i++)
        {
            Button tempo = Add_Item(i);
            btnList.Add(tempo);
            int rep = i;
            btnList[rep].onClick.AddListener(() => Iteam_Choice(rep));
        }
        
    }

    public void Quit_Popup()
    {
        quitSession.SetActive(true);
        popUp.SetActive(false);

    }

    private void Rest()
    {
        Destroy(clone);
        contentScroll.SetActive(false);
        elg = true;
        //indicator popup tap to place
    }

        private void Iteam_Choice(int d)
    {   
        Destroy(clone);
        clone = Instantiate(objectToPlace[d], savedPose.position, savedPose.rotation);
        audioSour.clip = audioToLes[d];
        audioSour.Play();

    }


    void Start()
    {
        if(pathObjet==null || cours==null || lang==null || nbrObj == 0) SceneManager.LoadScene(0);
        arOrigin = FindObjectOfType<ARRaycastManager>();
        
       
       


    }

    void Update()
        {
            UpdatePlacementPose();
            if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && elg )
            {
            
            PlaceObject();
            contentScroll.SetActive(true);
            elg=false;
            }
        
    }

    //get childen 
    GameObject GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }


    //Scroll Iteam clone

    private Button Add_Item(int x)
    {
        GameObject gh = Add_Texture(x);
        return gh.GetComponent<Button>();
    }
    private GameObject Add_Texture(int x)
    {

        Vector3 scale = new Vector3(1f, 1f, 1f);
        GameObject gm = Instantiate(Ref);
        gm.transform.SetParent(contentScroll.transform);
        gm.name = "" + x;
        gm.transform.localScale = scale;
        RawImage info_img = GetChildWithName(gm, "content_img").GetComponent<RawImage>();
        var texture = Resources.Load<Texture2D>(pathObjet + x);
        info_img.texture = texture;
        return gm;
    }


        //placer Notre objet 3D
        private void PlaceObject()
        {
            savedPose = placementPose;
            clone = Instantiate(refPos, placementPose.position, placementPose.rotation);
           

    }


        //Chercher Position d'objet 3D 
        private void UpdatePlacementPose()
        {
            var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
            var hits = new List<ARRaycastHit>();
            arOrigin.Raycast(screenCenter, hits, TrackableType.Planes);

            placementPoseIsValid = hits.Count > 0;
            if (placementPoseIsValid)
            {
                placementPose = hits[0].pose;

                var cameraForward = Camera.current.transform.forward;
                var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
                placementPose.rotation = Quaternion.LookRotation(cameraBearing);
            }
        }
    
}
