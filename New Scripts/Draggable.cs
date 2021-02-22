using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Security.Cryptography;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler

{
    ////public Transform ShipToReturnTo = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
       
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      
    }
}
