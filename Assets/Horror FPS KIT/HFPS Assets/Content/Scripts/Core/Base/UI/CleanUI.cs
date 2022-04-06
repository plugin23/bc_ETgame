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

        healthGroup.alpha = 0.2f;
        ammoGroup.alpha = 0.2f;
        objectiveGroup.alpha = 0.2f;
        
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

    }

    // Update is called once per frame
    void Update()
    {
        pointerEventData.position = eyeTracking.screenPoint;
        raycastResults = new List<RaycastResult>();
        rayCaster.Raycast(pointerEventData, raycastResults);

        Coroutine fadeRoutine = null;
        GameObject parentObject = null;

        //Debug.Log(pointerEventData);
        foreach (RaycastResult result in raycastResults)
        {
            //Debug.Log(result);
            parentObject = result.gameObject.transform.parent.gameObject;
            if (UIObjects.Contains(parentObject))
            {
                //Debug.Log(parentObject.name);
                if(fadeRoutine != null) {
                    StopCoroutine(fadeRoutine);
                    fadeRoutine = null;
                }
                StartCoroutine(fadeInAndOut(parentObject, true, parentObject.GetComponent<CanvasGroup>().alpha, 10f));
            }   
        }

        if (fadeRoutine == null)
        {
            foreach(GameObject gameObj in UIObjects)
            {
                if(gameObj.GetComponent<CanvasGroup>().alpha > 0.2f)
                {
                    Debug.Log(gameObj.name + " " + gameObj.GetComponent<CanvasGroup>().alpha);
                    StartCoroutine(fadeInAndOut(gameObj, false, gameObj.GetComponent<CanvasGroup>().alpha, 5f));
                }
                
            }
            
        }
        
    }
}
