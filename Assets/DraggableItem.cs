using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform parentAfterDrag;

    public GameObject buildingPrefab;
    private Camera mainCam;

    private List<Vector2> blockedSpaces;

    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        blockedSpaces = new List<Vector2>();
        blockedSpaces.Add(new Vector2(0, 0));
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
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
        //TODO check if valid position.
        float min = MinimumDistance(new Vector2(mousePos.x, mousePos.y));
        if(min < 0.5f)
        {
            Debug.Log("NO! >:(");
        } else
        {
            Debug.Log(min);
            blockedSpaces.Add(new Vector2(mousePos.x, mousePos.y));
            Instantiate(buildingPrefab, mousePos, Quaternion.Euler(0, 0, 0));
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
