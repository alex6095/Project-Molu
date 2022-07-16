using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlaceCharacterButton : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    GameObject characterPrefab;
    GameObject characterObject;

    GameObject blackCover;
    bool isActiveState;

    // Start is called before the first frame update
    void Start()
    {
        characterPrefab = Resources.Load("Prefabs/Characters/Unity-chan") as GameObject;

        blackCover = transform.GetChild(0).gameObject;
        blackCover.SetActive(false);
        isActiveState = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isActiveState)
        {
            characterObject = Instantiate(characterPrefab);
            characterObject.tag = "Ally";
            int layerMask = 1 << LayerMask.NameToLayer("Ground");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                characterObject.transform.position = hit.point;
            }
            OnDrag(eventData);

            blackCover.SetActive(true);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isActiveState)
        {
            int layerMask = 1 << LayerMask.NameToLayer("Ground");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                characterObject.transform.position = hit.point;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        if (isActiveState)
        {
            int layerMask = 1 << LayerMask.NameToLayer("Ground");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                characterObject.transform.position = hit.point;
            }

            isActiveState = false;
        }
    }
}
