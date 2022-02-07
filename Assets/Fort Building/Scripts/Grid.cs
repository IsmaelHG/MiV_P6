 using UnityEngine;

public class Grid : MonoBehaviour
{

    /// 
    /// returns the nearest point on a grid 
    
    //[SerializeField]
    public float size_x = 1f;
    public float size_y = 1f;
    public float size_z = 1f;

    public float ScaleFactor = 1;//used to scale the buildings
  
    private void Start()
    {
        //new size acording to the factor you want to multiply
        size_x*=ScaleFactor;
        size_y *= ScaleFactor;
        size_z *= ScaleFactor;

        
    }

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;
        //print("position  " + position.y);
        int xCount = Mathf.RoundToInt(position.x / size_x);
        int yCount = Mathf.RoundToInt(position.y / size_y);
        //print("Mathf.RoundToInt  " + yCount);
        int zCount = Mathf.RoundToInt(position.z / size_z);

        Vector3 result = new Vector3(
            (float)xCount * size_x,
           // yCount,
            yCount * size_y,
            (float)zCount * size_z);

       // print("(float)yCount  " + result.y);

        result += transform.position;
        //print("result.y  " + result.y);
        //print("                   " );
        return result;
    }

   
}