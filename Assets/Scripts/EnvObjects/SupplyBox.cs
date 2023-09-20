using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class SupplyBox : MonoBehaviour
{
    [SerializeField] private MeshRenderer mainBodyMesh;
    [SerializeField] private BoxCollider boxCollider;
    
    private Transform _initialParent;
    private bool _isPickable = true;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _initialParent = transform.parent;
    }

  
    // Public state getters
    public bool IsPickable() => _isPickable;

    
    public async Task Grab(Transform boxHolding)
    {
        _isPickable = false;
        
        gameObject.transform.SetParent(boxHolding);
        await transform.DOLocalRotate(Vector3.zero, 0.7f).AsyncWaitForCompletion();
        await transform.DOLocalMove(Vector3.zero, 0.7f).AsyncWaitForCompletion();
    }


    public async Task PutDown(Transform boxPlacing)
    {
        await transform.DORotate(boxPlacing.rotation.eulerAngles, 0.5f).AsyncWaitForCompletion();
        await transform.DOMove(boxPlacing.position, 0.5f).AsyncWaitForCompletion();
        gameObject.transform.SetParent(_initialParent);

        _isPickable = true;
    }


    public async Task CollectBox()
    {
        _isPickable = false;
        boxCollider.enabled = false;
        Vector3 dirUp = transform.position + transform.up * 20f;
        await mainBodyMesh.material.DOFade(0f, 2f).AsyncWaitForCompletion();
        await transform.DOMove(dirUp, 1f).AsyncWaitForCompletion();
        //Destroying box
        Destroy(gameObject);
    }


    private void OnDestroy()
    {
        transform.DOKill(false); // Kill tween without completion 
    }
}
