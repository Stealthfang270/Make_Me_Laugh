using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollSpawner : MonoBehaviour
{
    public GameObject ragdollPrefab;
    public GameObject ragdollParent;
    public GameObject playerModel;
    public PlayerController playerController;
    public GameObject camPivotPoint;
    public Vector3 camOriginalPos;
    public GameObject player;
    GameObject ragInst;

    public bool inRagdoll;
    public float getUpTime = 3;
    public float getUpTimeLeft;

    private void Awake()
    {
        camOriginalPos = camPivotPoint.transform.localPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Hazard" && playerModel.activeInHierarchy)
        {
            RagDoll();
        }
    }

    private void Update()
    {
        if(inRagdoll)
        {
            camPivotPoint.transform.rotation = playerModel.transform.rotation;

            if (playerController.onGround)
            {
                getUpTimeLeft -= Time.deltaTime;
            }

            if(getUpTimeLeft < 0)
            {
                GetUp();
            }
        }
    }

    public void GetUp()
    {
        camPivotPoint.transform.parent = player.transform;
        Destroy(ragInst);
        playerModel.SetActive(true);
        playerController.hasControl = true;
        inRagdoll = false;
        camPivotPoint.transform.localPosition = camOriginalPos;
    }

    public void RagDoll()
    {
        playerModel.SetActive(false);
        ragInst = Instantiate(ragdollPrefab, ragdollParent.transform);
        ragInst.transform.localPosition = Vector3.zero;
        ragInst.transform.rotation = ragdollParent.transform.rotation;
        playerController.hasControl = false;

        //Set camera to follow ragdoll
        var armature = ragInst.transform.GetChild(0);
        var hips = armature.GetChild(0);
        camPivotPoint.transform.parent = hips.transform;
        inRagdoll = true;

        getUpTimeLeft = getUpTime;
    }
}
