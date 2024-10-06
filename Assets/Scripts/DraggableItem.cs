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

        CatTower ct = buildingPrefab.GetComponent<CatTower>();
        catfoodCost.SetText(ct.catFoodCost.ToString());
        catCost.SetText(ct.catCost.ToString());

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
            CatTower ct = buildingPrefab.GetComponent<CatTower>();
            if(ct.catFoodCost > GameState.catfood || ct.catCost > GameState.numberOfCats)
            {
                mistakeText.SetActive(true);
                mistakeText.GetComponent<TMP_Text>().SetText("it cost: " + ct.catFoodCost + " catfood and " + ct.catCost + " cats, but you have: " + GameState.catfood
                    + " catfood and " + GameState.numberOfCats + " cats");
            } else
            {
                GameState.DecreaseCatfood(ct.catFoodCost);
                GameState.DecreaseCats(ct.catCost);
                for(int i = 0; i<ct.catCost; i++)
                {
                    Destroy(GameObject.FindGameObjectWithTag("FollowCat"));
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
