using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Android.LowLevel;

public class SupplyCollector : MonoBehaviour
{
    [SerializeField] private TMP_Text boxesCounter;
    
    private void OnTriggerEnter(Collider other)
    {
        // Interested only in boxes
        if (!other.gameObject.CompareTag("SupplyBox")) 
            return;
        
        CollectBox(other.gameObject.GetComponent<SupplyBox>());
    }


    private async void CollectBox(SupplyBox supplyBox)
    {
        // Do collecting logic
        GameController.Instance.AddCollectedBox();
        await supplyBox.CollectBox();

        boxesCounter.text = "Boxes: " + GameController.Instance.CollectedBoxes();
        Debug.Log("Collected boxes: " + GameController.Instance.CollectedBoxes());
    }
}
