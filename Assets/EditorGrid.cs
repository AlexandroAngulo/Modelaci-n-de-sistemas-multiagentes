using UnityEditor;
using UnityEngine;

public class GridGenerator : EditorWindow
{
    #region GridSettings
    Vector2 gridSize = new Vector2(0,0);
    Vector2 gridDimensions = new Vector2(1,1);

    Object emptyRoomObject = null;
    GameObject emptyRoomGameObject = null;

    private Vector2 truePadding;

    GameObject gridManager;
    #endregion

    [MenuItem ("Window/Grid Generator")]
    public static void  ShowWindow () 
    {
        EditorWindow.GetWindow(typeof(GridGenerator));
    }
    
    void OnGUI () 
    {
        GUILayout.Label ("Grid Settings", EditorStyles.boldLabel);
        gridSize = EditorGUILayout.Vector2Field("Amount of Columns and Rows", gridSize);
        gridDimensions = EditorGUILayout.Vector2Field("Grid Dimensions", gridDimensions);
        EditorGUILayout.Space();

        emptyRoomObject = EditorGUILayout.ObjectField("EmptyRoom Prefab",emptyRoomObject, typeof(GameObject), true);

        EditorGUILayout.Space();


        EditorGUILayout.Space();

        if (GUILayout.Button("Generate Grid"))
        {
            //Configure the grids properties
            ApplyGridSettings();

            //if we already have a grid, then destroy it
            DestroyOldGrid();

            //Generate the grid
            GenerateGrid();
        }
    }

    private void ApplyGridSettings() 
    {
        emptyRoomGameObject = emptyRoomObject as GameObject;

        truePadding =  gridDimensions / gridSize;

    }

    private void GenerateGrid()
    {
        Vector3 spawnPos;
        gridManager = new GameObject("Grid Manager");
        //Starts a loop for instantiating the Grid
        int i = 0;
        for (float x = 0; x < gridSize.x * truePadding.x; x += truePadding.x)
        {
            
            GameObject row = new GameObject("Row_" + i.ToString());
            row.transform.parent = gridManager.transform;
            int j = 0;
            for (float y = 0; y < gridSize.y * truePadding.y; y += truePadding.y)
            {
                spawnPos = new Vector3( x, 1f, y);

                GameObject spawnedRoom = Instantiate(emptyRoomGameObject, spawnPos, Quaternion.identity);
                spawnedRoom.transform.parent = row.transform;
                spawnedRoom.name = "Point_"+j.ToString();
                j++;
                
            }
            i++;
        }
    }

    private void DestroyOldGrid()
    {
        if (gridManager != null)
        {
            DestroyImmediate(gridManager);
        }
    }
}