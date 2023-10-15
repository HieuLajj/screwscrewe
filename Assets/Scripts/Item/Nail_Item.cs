using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SocialPlatforms.Impl;

public class Nail_Item : MonoBehaviour, TInterface<Nail_Item>
{
    //pool
    private ObjectPool<Nail_Item> _pool;
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteRange;




    // public bool hasSlot = false;
    // Start is called before the first frame update
    public Nail nail;
    public Slot_Item slot_item;
    public Collider2D ColiderNail;
    public List<HingeJoint2D> listHingeJoin;

    private void Awake()
    {
        ColiderNail = GetComponent<Collider2D>();
    }
    //private void OnMouseDown()
    //{
    //    //Debug.Log("CHIEN than than than than than");
    //    if (transform.parent.CompareTag("Slot"))
    //    {
    //        transform.parent.GetComponent<Slot_Item>().ActiveWhenDown();
    //    }
    //}


    public void ResetDisactiveListHingeJoint()
    {
        for (int i = 0; i < listHingeJoin.Count; i++)
        {
            if (listHingeJoin[i]!=null)
            {
                listHingeJoin[i].enabled = false;
            }         
        }
        listHingeJoin.Clear();
    }



    public void CheckOverlapBoxBoard()
    {
        StartCoroutine(checkover());
    }
    IEnumerator checkover()
    {
        yield return new WaitForSeconds(0f);
        Bounds boundnail = ColiderNail.bounds;
        Vector2 size = boundnail.size;
       // Debug.Log(size + "=VLOVK");
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, size, 0);
        List<int> layerboard = new List<int>();
        foreach (Collider2D collider in colliders)
        {
            // Bounds bounds1 = collider.bounds;
            // if (bounds1.Intersects(boundnail) && collider.CompareTag("Board"))
            // {
            //     //CalculateOverlapPercentage(ColiderNail, collider);
            //     boundnail.Encapsulate(bounds1.min);
            //     boundnail.Encapsulate(bounds1.max);
            //     float overlapArea = boundnail.size.x * boundnail.size.y;
            //     float overlapPercentage = (overlapArea / (bounds1.size.x * bounds1.size.y)) * 100f;
            //     if (overlapPercentage >= 90 && overlapPercentage <= 100.5f)
            //     {
            //         layerboard.Add(collider.gameObject.layer - 6);

            //     }
            //    // Debug.Log(overlapPercentage + "OKKAHE");

            // }
            if(collider.gameObject.layer == 29){                
                Slot_board_Item slotboardItem = collider.GetComponent<Slot_board_Item>();
                listHingeJoin.Add(slotboardItem.hingeJointInSlot);
                slotboardItem.hingeJointInSlot.enabled = true;
                layerboard.Add(collider.transform.parent.gameObject.layer - 6);


                //dich chuyen doi tuong
                Board_Item parenttBoard = slotboardItem.transform.parent.GetComponent<Board_Item>();
                Slot_board_Item parentt = parenttBoard.FindOtherSlotBoard(slotboardItem);

                Vector3 dir = parentt.transform.position - this.transform.position;
                Vector3 dir2 = parentt.transform.position - slotboardItem.transform.position;              
                // Đọc các giá trị rotation x, y, z của đối tượng A
                float rotationX = parenttBoard.transform.eulerAngles.x;
                float rotationY = parenttBoard.transform.eulerAngles.y;
                float rotationZ = parenttBoard.transform.eulerAngles.z;


                parenttBoard.DetermineCenterPoint(parentt);

                // Sử dụng các giá trị rotation
                Debug.Log("Rotation X: " + rotationX);
                Debug.Log("Rotation Y: " + rotationY);
                Debug.Log("Rotation Z: " + rotationZ);
                //Quaternion targetRotation = Quaternion.FromToRotation(dir2, dir);

                // Gán targetRotation vào rotation của đối tượng
               // parenttBoard.transform.rotation = targetRotation;
            }
        }

        //Debug.Log("CHUYEN DOI" + string.Join(", ", layerboard));
        gameObject.layer = Controller.Instance.nailLayerController.InputNumber(layerboard);
        ColiderNail.isTrigger = false;
    }
    float CalculateOverlapPercentage(Collider2D colliderA, Collider2D colliderB)
    {
        Bounds boundsA = colliderA.bounds;
        Bounds boundsB = colliderB.bounds;

        float intersectionWidth = Mathf.Min(boundsA.max.x, boundsB.max.x) - Mathf.Max(boundsA.min.x, boundsB.min.x);
        float intersectionHeight = Mathf.Min(boundsA.max.y, boundsB.max.y) - Mathf.Max(boundsA.min.y, boundsB.min.y);

        float intersectionArea = Mathf.Max(0, intersectionWidth) * Mathf.Max(0, intersectionHeight);
        float areaA = boundsA.size.x * boundsA.size.y;

        float overlapPercentage = intersectionArea / areaA * 100f;
        return overlapPercentage;
    }
    public void SetPool(ObjectPool<Nail_Item> pool) {
        _pool = pool;
    }

    public void ResetPool() {
        _pool.Release(this);
    }

    public Nail_Item IGetComponentHieu() {
        return this;
    }

    public void ResetAfterRelease()
    {
        listHingeJoin.Clear();
        ColiderNail.isTrigger = false;
        ResetImageNail();
    }

    public void StartCreate()
    { 
    } 

    public void ActiveImageNail()
    {
        spriteRenderer.sprite = spriteRange[1];
        spriteRenderer.transform.localPosition = new Vector3(0,0.2f,0);
    }
    public void ResetImageNailWithParticle()
    {
        ParticleNailItem particleNailItem = ParticleNailSpawner.Instance._pool.Get();
        particleNailItem.transform.position = transform.position;
        ResetImageNail();
    }
    public void ResetImageNail()
    {
        spriteRenderer.sprite = spriteRange[0];
        spriteRenderer.transform.localPosition = new Vector3(0,0,0);
    }
}
