﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using DG.Tweening;
using System.Linq;

public class Key_Item : MonoBehaviour, TInterface<Key_Item>
{
    //pool
    private ObjectPool<Key_Item> _pool;
    private bool checkRunning = false;
    private Tween moveTween;
    public void SetPool(ObjectPool<Key_Item> pool)
    {
        _pool = pool;
    }

    public void ResetPool()
    {
        _pool.Release(this);
    }

    public Key_Item IGetComponentHieu()
    {
        return this;
    }

    public void ResetAfterRelease()
    {
        checkRunning = false;
        if (moveTween != null)
        {
            moveTween.Kill(); 
            moveTween = null;
        }
    }

    public void StartCreate()
    {

    }

    public void FindLock(){
        //(checkRunning == true || Controller.Instance.rootlevel.litslock.Count == 0) return;
        if (checkRunning == true || Controller.Instance.rootlevel.litslock_mydictionary.Count == 0) return;
        //Lock_Item lock_Item = Controller.Instance.rootlevel.litslock[0];
        Lock_Item lock_Item = Controller.Instance.rootlevel.litslock_mydictionary.First().Value;
        if (lock_Item == null){
            return;
        }
        checkRunning = true;
        moveTween = transform.DOMove(lock_Item.transform.position,2.0f).OnComplete(()=>{
            //Debug.Log("bat dau chay");
            //Transform g = lock_Item.transform.parent;
            //if(g!=null){
            //    Slot_Item slot_Item = g.transform.GetComponent<Slot_Item>();
            //    slot_Item.hasLock = false;
            //}
            ////lock_Item.ResetPool();
            //lock_Item.animator.SetTrigger("play");
            this.ResetPool();
            Controller.Instance.rootlevel.listkey.Remove(this);
           
        });
    }
}
