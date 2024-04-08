using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DR.Utilities.Extensions;
using UnityEngine;

public class HandController : PoolElement
{
    [SerializeField] private HingeJoint2D anchor;
    [SerializeField] private HingeJoint2D bacon;
    
    [SerializeField] private List<BaconFlip> baconList = new();
    
    private Vector3 _initialAnchorPos;
    private bool _isDrop;
    public void OnInit()
    {
        SetIsDrop(false);
        _initialAnchorPos = anchor.transform.localPosition;
        
        baconList.ForEach(baconFlip => baconFlip.OnInit(this));
        
        PanController.OnFlipPan += FlipPanCallback;
    }

    private void OnDisable()
    {
        PanController.OnFlipPan -= FlipPanCallback;
    }

    public void Despawn()
    {
        anchor.transform.SetLocalPosition(_initialAnchorPos);
        baconList.ForEach(baconFlip =>
        {
            baconFlip.ResetBacon();
        });
        bacon.enabled = true;
        
        DespawnSelf();
    }

    public void MoveToTargetPos(float pos, float speed = 0.5f, float delay = 0.2f, Action onComplete = null)
    {
        anchor.transform.DOLocalMoveX(pos, speed).SetDelay(delay).OnComplete(() =>
        {
            onComplete?.Invoke();
        });
    }
    
    private void FlipPanCallback()
    {
        DropBacon();
        MoveToTargetPos(_initialAnchorPos.x);
    }

    public bool IsDone()
    {
        return _isDrop && baconList.All(baconFlip => !(baconFlip.Rb.velocity.magnitude > 0.1f));
    }

    public void SetIsDrop(bool value)
    {
        _isDrop = value;
    }

    public void SetVelocityZero()
    {
        baconList.ForEach(baconFlip =>
        {
            baconFlip.SetVelocityZero();
        });
    }

    private void DropBacon()
    {
        if(bacon == null) return;
        bacon.enabled = false;
        SetIsDrop(true);
    }


}
