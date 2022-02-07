using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectionEdge : MonoBehaviour
{

    //Detects all the points that are connected by collecting all the info from point detection scripts

    public bool SamePiecebool;
    public bool Terrainbool;

    public float radius = 0.1f;
    public bool CanBuild;
    //public int  SumPoints;
    public MeshRenderer MeshR;
    public PointdetectiononEdge[] Pdetect;
    public Transform CennterPiece;//samepiece center reference overlap
    public Collider[] hitColliders;

      
    public void CheckConnection()
    {
        //print("CheckConnection");
        SamePiecebool = false;
        Terrainbool = false;
        hitColliders = Physics.OverlapSphere(CennterPiece.position, radius);

        if (hitColliders.Length > 0)// if same piece or terrain don't check the points
        {
            
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].transform.tag == this.tag)
                { 
                    SamePiecebool = true;
                  //  print("samepiece");
                   // return;
                }

               else if (hitColliders[i].transform.tag == "Terrain")
                {
                    Terrainbool = true;
                    //print("terrain");
                    //return;
                }
                
            }

            
        }
        if(hitColliders.Length==0 || !Terrainbool || !SamePiecebool) // if is not terrain or same piece check points for conection
        {
            Pdetect[0].CheckOverlap();
            Pdetect[1].CheckOverlap();
            Pdetect[2].CheckOverlap();
            Pdetect[3].CheckOverlap();
            /* 
             Pdetect[0].CheckOverlap();
            if (Pdetect[0].conected==false)
             {
                 Pdetect[1].CheckOverlap();
                 if (Pdetect[1].conected == false)
                 {
                     Pdetect[2].CheckOverlap();
                     if (Pdetect[2].conected == false)
                     {
                         Pdetect[3].CheckOverlap();

                     }
                     else
                     {
                         return;
                     }
                 }
                 else
                 {
                     return;
                 }
             }
             else
             {
                 return;
             }
             */

        }

    }

    

    public bool SumDetect()
    {
        CheckConnection();
        if (SamePiecebool)
        {
             CanBuild= false;
        }
        else if (Terrainbool)
        {
             CanBuild=true;
        }

        else
        {
            
            if (Pdetect[0].conected || Pdetect[1].conected || Pdetect[2].conected || Pdetect[3].conected)
            {
                CanBuild = true;
            }
            else
            {
                CanBuild = false;
            }
        }


        return CanBuild;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(CennterPiece.position, radius);
    }

    private void OnDisable()
    {

        SamePiecebool = false;

    }
}
