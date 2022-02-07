using UnityEngine;


    public class StructurePlacer : MonoBehaviour
{
    //Place if allowed a structure

    #region variables
    private Grid grid;

    [Header("---! Build Pieces Objects !---")]
    [Tooltip("Build Pieces Objects")]
    public GameObject[] prefabToMove;
    public GameObject[] prefafToInsta;

    [Header("---! Structure Meshes !---")]
    [Tooltip("Structure meshes to move")]
    public MeshFilter[] StructureMeshes;
    

    [Header("---! Viability Materials  !---")]
    public Material[] Matbuild;

    [Header("---! Build Meshes insta  !---")]
    [Tooltip("Build Meshes")]
    public GameObject[] WoodMeshes;
    public GameObject[] BrickMeshes;
    public GameObject[] SteelMeshes;

    [Header("---! Build Meshes move  !---")]
    [Tooltip("Build Meshes")]
    public Mesh[] WoodMeshesMove;
    public Mesh[] BrickMeshesMove;
    public Mesh[] SteelMeshesMove;

    [Header("---! Build Conditionals  !---")]
    [Tooltip("bools ")]
    public bool InPlace;
    public bool SumD;
    public bool Move;
    public bool BuildTool=true;//If select the build tool aalos to build

   // [Header("---! Build Meshes  !---")]
    [Tooltip("ints ")]
    private int IntBuild;// number of the current build
    private int IntBuildMaterial;// number of the current material "wood", "brick",etc

    [Header("---! Build Position  !---")]
    [Tooltip("Build Position")]
    public Transform BuildPointRef;
    private Vector3 finalPosition; //final position according to the grid
    private Vector3 PreviousPoint;
    private Vector3 prefabtomoveposition;//?_______________________________________________________________________________________________________________TEST
    private GameObject prefabtomove;//?_______________________________________________________________________________________________________________TEST

    [Header("---! Audio  !---")]
    [Tooltip("Audio")]
    public AudioSource AudioS;
    public AudioClip[] AudioCLips;

    [Header("---! Keybinds  !---")]
    [Tooltip("Keys")]
    public KeySelector KeySel;

    [Header("---! Rotation  !---")]
    [Tooltip("Rotation")]
    public Transform RotRef; // Rotation reference
    private int Rotation;// fixed rotation
    public GameObject[] prefabToRotate; //prefabs to rotate 90


    [Header("---! Smooth Move  !---")]
    [Tooltip("Smooth Move")]
    public float m_smoothDamp = 0;
    public float lerptime = 15000;

    #endregion

     void Awake()
    {
        //gets the grid 
        grid = FindObjectOfType<Grid>();

        //Set the structures near to the player
        foreach (var item in prefabToMove)
        {
            item.transform.position = this.transform.position + new Vector3(0,-15,0); ;
        }
    }

    private void FixedUpdate()
    {
        if (BuildTool) // if we want to build
        {
            SumD = prefabToMove[IntBuild].GetComponentInChildren<CollisionDetectionEdge>().SumDetect();        //_______________________________________________________CHECK CONNECTION
            finalPosition = GetNearestPoint(BuildPointRef.position);
            m_Checkposition(finalPosition);
            RotatePrebaf(); //Rotate the object to build if hits the collider
            ViabilityMaterial();
        }

    }

    void Update()
    {

        if (Input.GetKeyDown(KeySel.BuildToolKey)) //Diseable building
        {
            if (!BuildTool)
            {
                BuildTool = !BuildTool;
            }
            else
            {
               
                foreach (var item in prefabToMove)
                {
                    item.gameObject.SetActive(false);
                    item.transform.position = prefabToMove[IntBuild].transform.position;
                }
                BuildTool = !BuildTool;
            }
        }
        if (BuildTool) //if build is eneabling
        {
            //Change the blue prefab to move when the key is pressed
            if (Input.GetKeyDown(KeySel.StairsKey)) //stair
            {
                ChangeBuildPrefab(0);
            }
            if (Input.GetKeyDown(KeySel.WallKey)) // Wall 
            {
                ChangeBuildPrefab(1);
            }
            if (Input.GetKeyDown(KeySel.FloorKey)) //Floor 
            {
                ChangeBuildPrefab(2);
            }

            //Build an intance .. instantiate a prefab in the scene
            if (Input.GetKey(KeyCode.Mouse0) && SumD && InPlace)
            {
                Instantiate(prefafToInsta[IntBuild], prefabToMove[IntBuild].transform.position, prefabToRotate[IntBuild].transform.rotation);
                AudioS.clip = AudioCLips[0];
                AudioS.Play();
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                ChangeBuildMaterial();
            }

            if (Input.GetKeyDown(KeySel.RotateKey)) //stair
            {
                RotateStructure90();
            }
        }
    }



    public void ViabilityMaterial() //Change the material to the building piece Blue, Transparent or Red
    {
        MeshRenderer BuildPieceMesh = prefabToMove[IntBuild].GetComponentInChildren<CollisionDetectionEdge>().MeshR; // get the meshrender from the build piece 

        if (prefabToMove[IntBuild].GetComponentInChildren<CollisionDetectionEdge>().SamePiecebool)
        {
            BuildPieceMesh.material = Matbuild[2]; //Transparent Material
        }
        else if (SumD && InPlace)
        {
            BuildPieceMesh.material = Matbuild[0];//Blue Material
        }
        else
        {
            BuildPieceMesh.material = Matbuild[1];//Red Material
        }

        
    }

    private void m_Checkposition(Vector3 hitInfo)
    {

        if (PreviousPoint != finalPosition)
        {
            
            AudioS.clip = AudioCLips[1];
            AudioS.Play();
            Move = true;
            PreviousPoint = finalPosition;
        }

        if (Move) //Move  building piece
        {
            prefabtomove = prefabToMove[IntBuild];//?_________________________________________________________________________________________________________TEST
            prefabtomoveposition = prefabToMove[IntBuild].transform.position;//?_________________________________________________________________________________TEST

            if (prefabToMove[IntBuild].transform.position == finalPosition)
            {
                InPlace = true;
                Move = false;
            }
            else
            {
                Move = true;
                InPlace = false;
                SetPosition(finalPosition);
                //print("Moving!!!");
            }

        }

    } //checks if the current position is the same as the final point on the grid

    public void ChangeBuildMaterial()
    {
        IntBuildMaterial++;
        if (IntBuildMaterial > 1)
        {
            IntBuildMaterial = 0;
        }
        if (IntBuildMaterial == 0)//Wood
        {
            foreach (var item in WoodMeshes)
            {
                item.SetActive( true) ;

            }
            foreach (var item in BrickMeshes)
            {
                item.SetActive(false);
            }
            for (int i = 0; i < prefabToMove.Length; i++)
            {
                StructureMeshes[i].mesh = WoodMeshesMove[i];
            }

        }
        if (IntBuildMaterial == 1)//Brick
        {
            foreach (var item in  BrickMeshes)
            {
                item.SetActive(true);

            }
            foreach (var item in WoodMeshes)
            {
                item.SetActive(false);
            }

            for (int i = 0; i < prefabToMove.Length; i++)
            {
                StructureMeshes[i].mesh = BrickMeshesMove[i];
            }
            
        }
    } //changes structure's build material Wood or Brick ...steal coming soon

    public Vector3 GetNearestPoint(Vector3 clickPoint)
    {
      return  grid.GetNearestPointOnGrid(clickPoint);
    }// gets the point on the grid to move the structure
    
    public void SetPosition(Vector3 targetPosition)
    {

        Transform m_transform = prefabToMove[IntBuild].transform;
        var m_newPositon = targetPosition;
        Vector3 m_positionVelocity=Vector3.zero;
        Vector3 m_targetPositon = Vector3.SmoothDamp(m_transform.position, m_newPositon, ref m_positionVelocity, m_smoothDamp, lerptime);
        m_transform.position = m_targetPositon;
    }  //Moves the structure smoothly

    public void RotatePrebaf()// Rotates de structure when you rotate the view
    {
        Transform TrotRef = RotRef;

        if (TrotRef.localEulerAngles.y > -45 && TrotRef.localEulerAngles.y <= 45)
        {
            Rotation = 0;
        }
        else if (TrotRef.localEulerAngles.y > 45 && TrotRef.localEulerAngles.y <= 135)
        {
            Rotation = 90;
        }
        else if (TrotRef.localEulerAngles.y > 135 && TrotRef.localEulerAngles.y <= 225)
        {
            Rotation = 180;
        }
        else if (TrotRef.localEulerAngles.y > 225 && TrotRef.localEulerAngles.y <= 315)
        {
            Rotation = 270;
        }

        prefabToMove[IntBuild].transform.rotation = Quaternion.Euler(0, Rotation, 0);
    }

    public void RotateStructure90 () //rotate the structure 90º with a key
    {

        if (IntBuild ==1) //if wall rotate 180
        {
            prefabToRotate[IntBuild].transform.rotation = prefabToRotate[IntBuild].transform.rotation * Quaternion.Euler(0, 180, 0);

        }
        else
        {
            prefabToRotate[IntBuild].transform.rotation = prefabToRotate[IntBuild].transform.rotation * Quaternion.Euler(0, 90, 0);

        }
    }

    public void ChangeBuildPrefab(int intprefab)
    {

        // Deactivate each blue PrefabToMove
        foreach (var item in prefabToMove)
        {
            item.gameObject.SetActive(false);
            item.transform.position = prefabToMove[IntBuild].transform.position;
        }
        BuildTool = true;
        IntBuild = intprefab;
        prefabToMove[intprefab].SetActive(true);


    }

}
