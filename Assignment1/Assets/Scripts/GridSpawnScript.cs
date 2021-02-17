using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawnScript : MonoBehaviour
{
    public GameObject cube;
    public GameObject hiderCube;
    public Camera mainCamera;
    public int width = 16;
    public int height = 16;

    GameObject[,] gridArray;
    GameObject[,] hiderGridArray;

    [SerializeField] float areaX = 0.4f;
    [SerializeField] float areaY = 0.65f;


    //public List<Vector2Int> diamondPositions;

    int minDiamondCount = 6;
    int maxDiamondCount = 8; 

    // Start is called before the first frame update
    void Start()
    {
        SpawnGrid();
        SetUpCamera();
        SetupResources();
        SpawnHiderGrid();

        HiderCubePositionsScript.Instance.SetupCubes(gridArray, hiderGridArray);
    }

    void SpawnGrid()
    {
        float camH = mainCamera.orthographicSize * 2;
        float camW = camH * mainCamera.aspect;

        float singleCubeWidth = camW * areaX / width;
        float singleCubeHeight = camH * areaY / height;


        gridArray = new GameObject[height, width];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var grid = Instantiate(cube);
                grid.transform.localScale = new Vector3(singleCubeWidth, singleCubeHeight, 1);

                grid.transform.position = new Vector3(x * singleCubeWidth, y * singleCubeHeight, 0);
                grid.GetComponent<CubeResourceScript>().SetUpResource(UnityEngine.Random.Range(2000 / 16, 5000 /8), CubeType.Rock);
                gridArray[y, x] = grid;
                grid.GetComponent<CubeResourceScript>().cubePos = new Vector2Int(x, y);
            }
        }
        HiderCubePositionsScript.Instance.areaHeight = height;
        HiderCubePositionsScript.Instance.areaWidth = width;
    }

    void SetupResources()
    {
        int diamondCount = UnityEngine.Random.Range(minDiamondCount, maxDiamondCount);
        for (int i = 0; i < diamondCount; i++)
        {
            int diamondX = -1;//Random.Range(0, width);
            int diamondY = -1;//Random.Range(0, height);

            while(!PositionIsValid(diamondX, diamondY))
            {
                diamondX = UnityEngine.Random.Range(0, width);
                diamondY = UnityEngine.Random.Range(0, height);
            }

            gridArray[diamondY, diamondX].GetComponent<CubeResourceScript>().SetUpResource(UnityEngine.Random.Range(2000, 5000), CubeType.Diamond);
            //diamondPositions.Add(new Vector2Int(diamondX, diamondY));

            PlaceCircles(diamondX, diamondY);

        }

        //for loop for every cell if something exists dont place, if not place rock
       /* for (int y = 0; y < gridArray.GetLength(0); y++)//Length; i++)
        {
            for(int x = 0; x < gridArray.GetLength(1); x++)
            {
                if(gridArray[y, x].GetComponent<CubeResourceScript>().type == null)
                {
                    gridArray[y, x].GetComponent<CubeResourceScript>().SetUpResource(UnityEngine.Random.Range(.125f, 0.0625f), CubeType.Rock);
                }

            }
        }*/
    }

    void CheckIfCubeHasValue(int posX, int posY, float value, CubeType type)
    {
        if(posX >= 0 && posY >= 0 && posX < width && posY < height && gridArray[posY, posX].GetComponent<CubeResourceScript>().type.Value < type)
            gridArray[posY, posX].GetComponent<CubeResourceScript>().SetUpResource(value, type);
    }

    private void PlaceCircles(int diamondX, int diamondY)
    {
        int[] offsetGX = { 0, 0, 1, -1, -1, 1, 1, -1 };
        int[] offsetGY = { 1, -1, 0, 0, -1, -1, 1, 1 };
        for (int i = 0; i < offsetGX.Length; i++)
            CheckIfCubeHasValue(diamondX + offsetGX[i], diamondY + offsetGY[i], UnityEngine.Random.Range(2000 /2, 5000 /2), CubeType.Gold);

        int[] offsetSX = { 0, 1, 2, 2, 2, 2, 2, 1, 0, -1, -2, -2, -2, -2, -2, -1 };
        int[] offsetSY = { 2, 2, 2, 1, 0, -1, -2, -2, -2, -2, -2, -1, 0, 1, 2, 2 };
        for (int i = 0; i < offsetSX.Length; i++)
            CheckIfCubeHasValue(diamondX + offsetSX[i], diamondY + offsetSY[i], UnityEngine.Random.Range(2000 /4, 5000 /4), CubeType.Silver);

        /*gridArray[diamondY +2, diamondX].GetComponent<CubeResourceScript>().SetUpResource(0.25f, CubeType.Silver);
        gridArray[diamondY +2, diamondX +1].GetComponent<CubeResourceScript>().SetUpResource(0.25f, CubeType.Silver);
        gridArray[diamondY +2, diamondX +2].GetComponent<CubeResourceScript>().SetUpResource(0.25f, CubeType.Silver);
        gridArray[diamondY +1, diamondX +2].GetComponent<CubeResourceScript>().SetUpResource(0.25f, CubeType.Silver);
        gridArray[diamondY, diamondX +2].GetComponent<CubeResourceScript>().SetUpResource(0.25f, CubeType.Silver);
        gridArray[diamondY -1, diamondX +2].GetComponent<CubeResourceScript>().SetUpResource(0.25f, CubeType.Silver);
        gridArray[diamondY -2, diamondX +2].GetComponent<CubeResourceScript>().SetUpResource(0.25f, CubeType.Silver);
        gridArray[diamondY - 2, diamondX + 1].GetComponent<CubeResourceScript>().SetUpResource(0.25f, CubeType.Silver);
        gridArray[diamondY - 2, diamondX].GetComponent<CubeResourceScript>().SetUpResource(0.25f, CubeType.Silver);
        gridArray[diamondY - 2, diamondX - 1].GetComponent<CubeResourceScript>().SetUpResource(0.25f, CubeType.Silver);
        gridArray[diamondY - 2, diamondX - 2].GetComponent<CubeResourceScript>().SetUpResource(0.25f, CubeType.Silver);
        gridArray[diamondY - 1, diamondX - 2].GetComponent<CubeResourceScript>().SetUpResource(0.25f, CubeType.Silver);
        gridArray[diamondY, diamondX - 2].GetComponent<CubeResourceScript>().SetUpResource(0.25f, CubeType.Silver);
        gridArray[diamondY + 1, diamondX - 2].GetComponent<CubeResourceScript>().SetUpResource(0.25f, CubeType.Silver);
        gridArray[diamondY + 2, diamondX - 2].GetComponent<CubeResourceScript>().SetUpResource(0.25f, CubeType.Silver);
        gridArray[diamondY + 2, diamondX - 1].GetComponent<CubeResourceScript>().SetUpResource(0.25f, CubeType.Silver);
        */
    }

    private bool PositionIsValid(int diamondX, int diamondY)
    {
        return diamondX >= 0 && diamondX < width && diamondY >= 0 && diamondY < height && gridArray[diamondY, diamondX].GetComponent<CubeResourceScript>().type != CubeType.Diamond;
        //{
        //    return true;
            //for (int i = 0; i < diamondPositions.Count; i++)
            //{
            //    if (diamondX == diamondPositions[i].x && diamondY == diamondPositions[i].y)
            //    {
            //        return false;
            //    }
            //}
            //return true;
        //}
        //return false;
    }

    void SetUpCamera()
    {
        float camH = mainCamera.orthographicSize * 2;
        float camW = camH * mainCamera.aspect;

        float singleCubeWidth = camW * areaX / width;
        float singleCubeHeight = camH * areaY / height;

        //mainCamera.orthographicSize = height / 2;
        mainCamera.transform.position = new Vector3((width / 2.0f - 0.5f) * singleCubeWidth, (height * (1 - 0.475f) - 0.5f) * singleCubeHeight, -10);
    }

    void SpawnHiderGrid()
    {
        float camH = mainCamera.orthographicSize * 2;
        float camW = camH * mainCamera.aspect;

        float singleCubeWidth = camW * areaX / width;
        float singleCubeHeight = camH * areaY / height;

        hiderGridArray = new GameObject[height, width];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var grid = Instantiate(hiderCube);
                grid.transform.localScale = new Vector3(singleCubeWidth, singleCubeHeight, 1);

                grid.transform.position = new Vector3(x * singleCubeWidth, y * singleCubeHeight, -1);
                hiderGridArray[y, x] = grid;
                grid.GetComponent<DestroyCubesOnClickScript>().hiderCubePos = new Vector2Int(x, y);

            }
        }
    }
}
