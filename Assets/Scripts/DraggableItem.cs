using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform parentAfterDrag;

    public GameObject buildingPrefab;
    public TMP_Text catfoodCost;
    public TMP_Text catCost;

    public GameObject mistakeText;

    private Camera mainCam;

    private List<Vector2> blockedSpaces;

    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        blockedSpaces = new List<Vector2>();
        blockedSpaces.Add(new Vector2(0, 0));

        Tower ct = buildingPrefab.GetComponent<Tower>();
        catfoodCost.SetText(ct.CatFoodCost().ToString());
        catCost.SetText(ct.CatCost().ToString());

        mistakeText.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        mistakeText.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        float min = MinimumDistance(new Vector2(mousePos.x, mousePos.y));
        if(min < 0.5f)
        {
            mistakeText.SetActive(true);
            mistakeText.GetComponent<TMP_Text>().SetText("Invalid building location");
        } else
        {
            //Check costs
            Tower ct = buildingPrefab.GetComponent<Tower>();
            if(ct.CatFoodCost() > GameState.catfood || ct.CatCost() > GameState.numberOfCats)
            {
                mistakeText.SetActive(true);
                mistakeText.GetComponent<TMP_Text>().SetText("it cost: " + ct.CatFoodCost() + " catfood and " + ct.CatCost() + " cats, but you have: " + GameState.catfood
                    + " catfood and " + GameState.numberOfCats + " cats");
            } else
            {
                GameState.DecreaseCatfood(ct.CatFoodCost());
                GameState.DecreaseCats(ct.CatCost());
                GameObject[] cats = GameObject.FindGameObjectsWithTag("FollowCat");
                for (int i = 0; i<ct.CatCost(); i++)
                {
                    Destroy(cats[i]);
                }

                blockedSpaces.Add(new Vector2(mousePos.x, mousePos.y));
                Instantiate(buildingPrefab, mousePos, Quaternion.Euler(0, 0, 0));
            }         
        }

        
    }

    public float MinimumDistance(Vector2 mousePos)
    {
        float min = float.MaxValue;
        foreach(Vector2 vector in blockedSpaces)
        {
            float dist = Vector2.Distance(mousePos, vector);
            if(dist < min)
            {
                min = dist;
            }
        }
        return min;
    }

}
