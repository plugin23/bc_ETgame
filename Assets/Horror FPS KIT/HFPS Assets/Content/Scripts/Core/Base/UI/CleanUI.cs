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

        foreach (GameObject gameObj in UIObjects)
        {
            StartCoroutine(fadeInAndOut(gameObj, false, gameObj.GetComponent<CanvasGroup>().alpha, 6f));
            Debug.Log(gameObj.GetComponent<CanvasGroup>().alpha);
        }
        pointerEventData = new PointerEventData(eventSystem);
    }

    private void Awake()
    {
        eyeTracking = eyeTrackingObject.GetComponent<EyeTracking>();
        healthGroup = UI_Health.GetComponent<CanvasGroup>();
        ammoGroup = UI_Ammo.GetComponent<CanvasGroup>();
        objectiveGroup = ObjectiveParent.GetComponent<CanvasGroup>();

        healthGroup.alpha = 1f;
        ammoGroup.alpha = 1f;
        objectiveGroup.alpha = 1f;
        
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
            Debug.Log(alpha);
            objectToFade.GetComponent<CanvasGroup>().alpha = alpha;
            if (fadeIn)
            {
                yield return new WaitForSeconds(5);
            }
            else
            {
                yield return null;
            }
            
        }

    }

    void FadeIn(CanvasGroup group)
    {
        while(group.alpha < 1)
        {
            group.alpha += 0.00000001f;
        }

    }

    void FadeOut(CanvasGroup group)
    {
        while(group.alpha > 0.2f)
        {
            group.alpha -= 0.02f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        pointerEventData.position = eyeTracking.screenPoint;
        raycastResults = new List<RaycastResult>();
        rayCaster.Raycast(pointerEventData, raycastResults);
        
        //Debug.Log(pointerEventData);
        foreach (RaycastResult result in raycastResults)
        {
            //Debug.Log(result);
            GameObject parentObject = result.gameObject.transform.parent.gameObject;
            if (UIObjects.Contains(parentObject))
            {
                //Debug.Log(parentObject.name);
                StartCoroutine(fadeInAndOut(parentObject, true, parentObject.GetComponent<CanvasGroup>().alpha, 1f));
                StartCoroutine(fadeInAndOut(parentObject, false, parentObject.GetComponent<CanvasGroup>().alpha, 1f));
                //fadeInAndOut(parentObject, false, 1f);
            }
            
        }

        /*foreach(GameObject gameObj in UIObjects)
        {
            FadeOut(gameObj.GetComponent<CanvasGroup>());
        }*/
        
    }
}
