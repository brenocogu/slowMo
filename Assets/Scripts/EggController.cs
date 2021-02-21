using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EggController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] ExplodeController shatteredPrefab;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject oj = PoolHub.GetFromPool(shatteredPrefab.gameObject);
        oj.transform.position = transform.position;
        gameObject.SetActive(false);
        oj.GetComponent<ExplodeController>().explosionPart = eventData.pointerCurrentRaycast.worldPosition;
        oj.SetActive(true);
    }
}
