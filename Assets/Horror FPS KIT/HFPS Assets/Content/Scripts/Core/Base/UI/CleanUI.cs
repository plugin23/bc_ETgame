using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CleanUI : MonoBehaviour
{
    public GameObject UI_Health;
    public GameObject UI_Ammo;
    public GameObject ObjectiveParent;
    public GameObject eyeTrackingObject;

    private Coroutine fadeRoutine;

    GraphicRaycaster rayCaster;
    PointerEventData pointerEventData;
    EventSystem eventSystem;

    CanvasGroup healthGroup;
    CanvasGroup ammoGroup;
    CanvasGroup objectiveGroup;
    EyeTracking eyeTracking;

    List<GameObject> UIObjects;
    List<RaycastResult> raycastResults;

    // Start is called before the first frame update
    void Start()
    {
        rayCaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
        UIObjects = new List<GameObject>();
        fadeRoutine = null;

        UIObjects.Add(UI_Health);
        UIObjects.Add(UI_Ammo);
        UIObjects.Add(ObjectiveParent);

        pointerEventData = new PointerEventData(eventSystem);
    }

    private void Awake()
    {
        eyeTracking = eyeTrackingObject.GetComponent<EyeTracking>();
        healthGroup = UI_Health.GetComponent<CanvasGroup>();
        ammoGroup = UI_Ammo.GetComponent<CanvasGroup>();
        objectiveGroup = ObjectiveParent.GetComponent<CanvasGroup>();

        if (StaticInfo.tracking)
        {
            healthGroup.alpha = 0.2f;
            ammoGroup.alpha = 0.2f;
            objectiveGroup.alpha = 0.2f;
        }
        else
        {
            healthGroup.alpha = 1f;
            ammoGroup.alpha = 1f;
            objectiveGroup.alpha = 1f;
        }
        
    }

    IEnumerator fadeInAndOut(GameObject objectToFade, bool fadeIn, float currAlpha, float duration)
    {
        float counter = 0f;

        //Set Values depending on if fadeIn or fadeOut
        float a, b;
        if (fadeIn)
        {
            a = currAlpha;
            b = 1;
        }
        else
        {
            a = currAlpha;
            b = 0.2f;
        }

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(a, b, counter / duration);
            objectToFade.GetComponent<CanvasGroup>().alpha = alpha;
            yield return null;
        }

        fadeRoutine = null;
    }

    // Update is called once per frame
    void Update()
    {
        pointerEventData.position = eyeTracking.screenPoint;
        raycastResults = new List<RaycastResult>();
        rayCaster.Raycast(pointerEventData, raycastResults);

        GameObject parentObject = null;

        foreach (RaycastResult result in raycastResults)
        {
            parentObject = result.gameObject.transform.parent.gameObject;
            if (UIObjects.Contains(parentObject))
            {
                if(fadeRoutine != null) {
                    StopCoroutine(fadeRoutine);
                    fadeRoutine = null;
                }
                fadeRoutine = StartCoroutine(fadeInAndOut(parentObject, true, parentObject.GetComponent<CanvasGroup>().alpha, 1f));
            }   
        }

        if (fadeRoutine == null)
        {
            foreach(GameObject gameObj in UIObjects)
            {
                if(gameObj.GetComponent<CanvasGroup>().alpha > 0.2f)
                {
                    Debug.Log(gameObj.name + " " + gameObj.GetComponent<CanvasGroup>().alpha);
                    if(fadeRoutine == null)
                    {
                        fadeRoutine = StartCoroutine(fadeInAndOut(gameObj, false, gameObj.GetComponent<CanvasGroup>().alpha, 0.5f));
                    }
                    
                }
 
            }
            
        }
        
    }
}
