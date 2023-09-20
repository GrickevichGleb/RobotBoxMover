using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMover : MonoBehaviour
{
    [SerializeField] private GameObject boxPlacementMark;
    
    private Player _player;

    private Vector3 _centerOfBoxCast;
    private Vector3 _halfExtentsOfBoxCast;

    private SupplyBox _supplyBox;
    private bool _isPickingOrPutting;
    
    private void Awake()
    {
        _player = GetComponent<Player>();
    }


    public void TryPickOrPuttBox()
    {
        if (_isPickingOrPutting) return;
        
        if(_supplyBox == null)
            TryPickBox();
        else
            TryPutBox();
    }
    

    public async void TryPickBox()
    {
        if (_supplyBox != null) return; // Means we already holding a box
        
        SupplyBox box = CheckForBoxToPick();
        if (box == null) return;
        
        _isPickingOrPutting = true;
        _player.canMove = false;
        
        _supplyBox = box;
        await _supplyBox.Grab(_player.GetBoxHoldingTransform());

        _player.GetAnimator().SetBool("isCarriesBox", true);
        _player.canMove = true;
        _isPickingOrPutting = false;
        
        boxPlacementMark.SetActive(true);
    }


    public async void TryPutBox()
    {
        if (_supplyBox == null) return; // If we aren't holding a box

        _isPickingOrPutting = true;
        _player.canMove = false;

        boxPlacementMark.SetActive(false);

        await _supplyBox.PutDown(_player.GetBoxPuttingTransform());
        _supplyBox = null;

        _player.GetAnimator().SetBool("isCarriesBox", false);
        _player.canMove = true;
        _isPickingOrPutting = false;
    }
    

    private SupplyBox CheckForBoxToPick()
    {
        RaycastHit[] hits = Physics.BoxCastAll(
            _player.GetBoxPuttingTransform().position + new Vector3(0f, 0.35f, 0f)
            , new Vector3(0.5f, 0.5f, 0.5f)
            , Vector3.up, Quaternion.identity
            , 0f);

        if (hits.Length < 1) return null; // If no hits - return early
        
        // Finding nearest box
        float[] distances = new float[hits.Length];
        for (int i = 0; i < hits.Length; i++)
        {
            distances[i] = hits[i].distance;
        }

        Array.Sort(distances, hits);
        
        // Return nearest supply box
        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("SupplyBox"))
            {
                if(hit.collider.TryGetComponent<SupplyBox>(out SupplyBox supplyBox))
                    if (supplyBox.IsPickable())
                        return supplyBox;
            }
        }

        return null;
    }

    
}