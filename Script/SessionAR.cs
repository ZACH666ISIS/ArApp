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

public class SessionAR : MonoBehaviour
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
    private string pathObjet=null, pathSound=null;
    private List<Button> btnList = new List<Button>();
    private User matLang;

    void Awake()
    {
        if (ParVar.lang == null || ParVar.cours == null) SceneManager.LoadScene(0);
        else
        {   
            ParVar.SetPath();
            pathObjet = ParVar.pathObjet;
            pathSound = ParVar.pathSound;
            nbrObj = ParVar.nbrObj;
        }
        
        matLang = SaveSystem.LoadInfo();

        string line = Resources.Load<TextAsset>("storage/indiceSession").text;
        string[] param = line.Split('|');
        line = null;
        txt.text = param[matLang.GetMatLang()];
        if (matLang.GetMatLang() == 2) txt.fontSize = 50;
        param = null;
        
        contentScroll.SetActive(false);
        quitSession.SetActive(false);
        popUp.SetActive(true);
        btnPopUp.onClick.AddListener(() => Quit_Popup());

       objectToPlace = new GameObject[nbrObj];
      audioToLes = new AudioClip[nbrObj];

      for (int i = 0; i < nbrObj; i++) {
         var audioClip = Resources.Load<AudioClip>(pathSound+i);
         var objLoad = Resources.Load<GameObject>(pathObjet+i);
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
    //enlever indicator popup tap to place
    private void Rest()
    {
        Destroy(clone);
        contentScroll.SetActive(false);
        elg = true;

    }
    //evenment choix objet
        private void Iteam_Choice(int d)
    {   
        Destroy(clone);
        clone = Instantiate(objectToPlace[d], savedPose.position, savedPose.rotation);
        audioSour.clip = audioToLes[d];
        audioSour.Play();
    }


    void Start()
    {
        arOrigin = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
        {

        UpdatePlacementPose();
        if ( placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && elg && !popUp.activeSelf)
        {
            PlaceObject();
            contentScroll.SetActive(true);
            elg=false;
         }
        
    }

    //get childen set parent 
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


    //Scroll Iteam clonage iteam

    private Button Add_Item(int x)
    {
        GameObject gh = Add_Texture(x);
        return gh.GetComponent<Button>();
    }
    //ajout texture au scroll
    private GameObject Add_Texture(int x)
    {

        Vector3 scale = new Vector3(1f, 1f, 1f);
        GameObject gm = Instantiate(Ref);
        gm.transform.SetParent(contentScroll.transform);
        gm.name = "" + x;
        gm.transform.localScale = scale;
        RawImage info_img = GetChildWithName(gm, "content_img").GetComponent<RawImage>();
        var texture = Resources.Load<Texture2D>( pathObjet + x);
        info_img.texture = texture;
        return gm;
    }

    public void Retour()
    {
        SceneManager.LoadScene(1);

    }

    //placer Notre objet 3D
    private void PlaceObject()
        {
            savedPose = placementPose;
            clone = Instantiate(refPos, placementPose.position, placementPose.rotation);
        }


        //positionnement screenPlane
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
