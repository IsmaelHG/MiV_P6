using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointdetectiononEdge : MonoBehaviour {

    public bool conected;
    public float radius=0.6f;
   
    public Collider[] hitColliders;
    private void OnDisable()
    {
        conected = false;
    }

	public void CheckOverlap () {

       // print("Checkoverlap");
        conected = false;
        hitColliders = Physics.OverlapSphere(transform.position, radius);
        
        if (hitColliders.Length>0)
        {
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].tag == "point" || hitColliders[i].tag == "Terrain")
                {
                    conected = true;
                    //Debug.Log("Connected point");
                    return;
                }
                
            }
        }
        else
        {
            conected = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
