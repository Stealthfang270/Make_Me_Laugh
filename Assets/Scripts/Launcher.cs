using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public bool launchRagdoll;
    public Vector3 direction;

    public float power;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(direction * power);

            if(launchRagdoll)
            {
                var hips = GetHips(collision.gameObject);
                Rigidbody hipRb = hips.GetComponent<Rigidbody>();
                hipRb.AddForce(direction * power);
            }
        }
    }

    public Transform GetHips(GameObject player)
    {
        var playerModelPos = player.transform.GetChild(0);
        var ragdollPos = playerModelPos.transform.GetChild(1);
        if(ragdollPos.childCount > 0)
        {
            var ragdollPrefab = ragdollPos.transform.GetChild(0);
            var armature = ragdollPrefab.transform.GetChild(0);
            return armature.transform.GetChild(0);
        }
        return null;
    }
}
