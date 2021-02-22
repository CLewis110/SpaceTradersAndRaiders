using UnityEngine;
using System.Collections;

public class GridOverlay : MonoBehaviour
{
    //Switch for displaying the grid
    private bool showMain = true;

    //The grid's total size and the size of the cells
    public int gridSizeX;
    public int gridSizeY;
    private int gridSizeZ = 0;
    public float cellSize;

    //Starts at scene origin
    private float startX = 0;
    private float startY = 0;
    private float startZ = 0;

    //The lines themselves and color of the lines
    private Material lineMaterial;
    private Color mainColor = new Color(0f, 1f, 0f, 1f);
    //public GameObject grid;

    /*******************************
     * Turns on/off the grid visual.
     *******************************/
    void Update()
    {
        /*if(Input.GetKeyDown("space"))
        {
            showMain = !showMain;
        }*/
    }

    /*
     * Creates the lines of the grid.
     */
    void CreateLineMaterial()
    {
        if (!lineMaterial)
        {
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            var shader = Shader.Find("Hidden/Internal-Colored");
            lineMaterial = new Material(shader);
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            // Turn on alpha blending
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            lineMaterial.SetInt("_ZWrite", 0);
        }
    }

    /*****************************************************
     * NOTE:This is what we should use to change the grid
     * size or use the start method/function.
     * Construtors don't seem to work properly.
     *****************************************************/
    /*void OnPreRender()
    {
        gridSizeX = 20;
        gridSizeY = 20;
    }*/

    /*void Start()
    {
        //Grid test = grid.GetComponent<Grid>();
        gridSizeX = 20;//test.GetGridWidth();
        gridSizeY = 20;
    }*/

    /*****************************************************
     * Draws the grid via For loops.
     * 
     * NOTES: Runs every frame I think.
     * Runs after the camera is done with everything else.
     *****************************************************/
    void OnPostRender()
    {
        CreateLineMaterial();

        // set the current material
        lineMaterial.SetPass(0);

        //GL is graphics library and is what draws the grid
        GL.Begin(GL.LINES);

        //If the user wants to display the grid
        if(showMain)
        {
            GL.Color(mainColor);

            //Layers
            for (float j = 0; j <= gridSizeY; j += cellSize)
            {
                //X axis lines
                for (float i = 0; i <= gridSizeZ; i += cellSize)
                {
                    GL.Vertex3(startX, startY + j, startZ + i);
                    GL.Vertex3(startX + gridSizeX, startY + j, startZ + i);
                }

                //Z axis lines
                for (float i = 0; i <= gridSizeX; i += cellSize)
                {
                    GL.Vertex3(startX + i, startY + j, startZ);
                    GL.Vertex3(startX + i, startY + j, startZ + gridSizeZ);
                }
            }

            //Y axis lines
            for (float i = 0; i <= gridSizeZ; i += cellSize)
            {
                for (float k = 0; k <= gridSizeX; k += cellSize)
                {
                    GL.Vertex3(startX + k, startY, startZ + i);
                    GL.Vertex3(startX + k, startY + gridSizeY, startZ + i);
                }
            }
        }

        GL.End();
    }
}