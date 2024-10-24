using System.Collections.Generic;
using UnityEngine;

public class ItemController : IController
{
    public List<ItemView> Views { get; private set; } = new();

    private PlayerController playerController;
    private ItemView cachedItem;
    private bool isTaked;

    public ItemController()
    {
        playerController = MonoEntryPoint.Instance.Get<PlayerController>();
    }
    
    public void AddView(BaseView view)
    {
        Views.Add(view as ItemView);
    }

    public void GetOrDropItem()
    {
        if (cachedItem != null)
        {
            OnDropItem();
        }
        else
        {
            TryGetItem();
        }
    }
    
    private void TryGetItem()
    {
        var camera = playerController.View.PlayerCamera;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out var hit) == false)
        {
            return;
        }

        foreach (var view in Views)
        {
            if (view.Item != hit.transform)
            {
                continue;
            }
            
            if (view.inPlace)
            {
                break;
            }
            
            cachedItem = view;
            SetPhysicsState(false);
            cachedItem.Item.SetParent(playerController.View.ItemPoint);
            cachedItem.Item.localEulerAngles = cachedItem.TakedRot;
            cachedItem.ChangeColorPlace(true);
            break;
        }
    }

    private void OnDropItem()
    {
        var camera = playerController.View.PlayerCamera;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out var hit))
        {
            if (cachedItem.HoldPlace == hit.transform)
            {
                cachedItem.transform.position = cachedItem.HoldPlace.position;
                cachedItem.transform.eulerAngles = cachedItem.HoldPlace.eulerAngles;
                cachedItem.HoldPlace.gameObject.SetActive(false);
                cachedItem.SetPlace();
            }
        }
        
        SetPhysicsState(true);
        cachedItem.ChangeColorPlace(false);
        cachedItem.Item.SetParent(null);
        isTaked = false;
        cachedItem = null;

        if (IsAllItemInPlace())
        {
            playerController.View.Story.SetEndAnim();
        }
    }

    private void SetPhysicsState(bool state)
    {
        cachedItem.Rigidbody.isKinematic = !state;
    }

    private bool IsAllItemInPlace()
    {
        var isAllPlace = true;
        foreach (var view in Views)
        {
            if (view.inPlace == false)
            {
                isAllPlace = false;
            }
        }

        return isAllPlace;
    }
    
    public void OnUpdate()
    {
        if (cachedItem == null)
        {
            return;
        }

        if (isTaked)
        {
            return;
        }

        var delta = Time.deltaTime;
        var handTransform = playerController.View.ItemPoint;
        
        cachedItem.Item.position = Vector3.Lerp(cachedItem.Item.position, handTransform.position, 10f * delta);
        if (Vector3.Distance(cachedItem.Item.position, handTransform.position) < 0.2f)
        {
            isTaked = true;
        }
    }
}