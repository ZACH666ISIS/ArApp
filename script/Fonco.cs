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
    public Button buttonNext;
    public TextMeshProUGUI tmp;
    public AudioSource audioSour;

    private GameObject clone;
    
    private ARRaycastManager arOrigin;
    private Pose placementPose ,savedPose;
    private int x=0;
    private bool placementPoseIsValid = false;
    private static bool t = false;
    private string lang = null , cours =null;




    void Awake()
    {


        lang = ParVar.lang;
        cours = ParVar.cours;


        /*
          switch (cours)
         {
             case 0:
                  switch (lang)
        		 {
             case "ar":
		pathObjet="obj/Alpha/ar/";
		nbrObj=28;
                 break;
             case "tmz":
                pathObjet="obj/Alpha/tmz/";
		nbrObj=33;
                 break;
             case "fr":
		pathObjet="obj/Alpha/latin/";	
		nbrObj=26;
                 break;
             case "eng":
                pathObjet="obj/Alpha/latin/";	
		nbrObj=26;
                 break;

         		}

                 break;

                 break;
             case 1:
		pathObjet="obj/Number/";	
		nbrObj=11;
                 break;
	     case 2:
		pathObjet="obj/Fruits/";	
		nbrObj=7;
                 break;
             case 3:
		pathObjet="obj/Legumes/";	
		nbrObj=X;
                 break;
             case 4:
		pathObjet="obj/Animals/";	
		nbrObj=10;
                 break;
	     case 5:
		pathObjet="obj/Espace/";	
		nbrObj=11;
                 break;
	     case 6:
		pathObjet="obj/Formes/";	
		nbrObj=11;
                 break;
           
         }
         
         
         */

        objectToPlace = new GameObject[11];
        audioToLes = new AudioClip[11];
        for (int i = 0; i <= 10; i++) {
            var audioClip = Resources.Load<AudioClip>("ArSound/Number/" + lang + "/" + i);
            var objLoad = Resources.Load<GameObject>("obj/Number/" + lang + "/" + i);
            audioToLes[i] = audioClip;
            objectToPlace[i] = objLoad;
        }
       

    }


    void Start()
    {
       
        buttonNext.gameObject.SetActive(false);
        arOrigin = FindObjectOfType<ARRaycastManager>();
        Button btn = buttonNext.GetComponent<Button>();
       btn.onClick.AddListener(TaskOnClick);
       



    }


        void Update()
        {
            UpdatePlacementPose();
            if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && x==0)
            {
            audioSour.clip = audioToLes[x];
            audioSour.Play();
            PlaceObject();
                x++;
            }

        }



        void TaskOnClick()
        {

                 if (t){
                        x = 0;
                        t = false;
                        SceneManager.LoadScene(0);

                    }
                    if (objectToPlace.Length > x)
                    {
                         Debug.Log("Langue : " + lang + "| l'objet numero : " + x);
                         Destroy(clone);
                         clone = Instantiate(objectToPlace[x], savedPose.position, savedPose.rotation);
                            audioSour.clip = audioToLes[x];
                            audioSour.Play();
                            x++;

                    }
                     else
                    {
                        tmp.text = "Terminer";
                        t = true;
                    } 




        }








        //placer Notre objet 3D
        private void PlaceObject()
        {
            savedPose = placementPose;
            clone = Instantiate(objectToPlace[x], placementPose.position, placementPose.rotation);
            buttonNext.gameObject.SetActive(true);

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
