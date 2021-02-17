using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiderCubePositionsScript : MonoBehaviour
{
    public static HiderCubePositionsScript Instance { get; private set; }

    public int areaWidth;
    public int areaHeight;

    GameObject[,] resourceCubes;
    GameObject[,] hideCubes;

    public void SetupCubes(GameObject[,] resourceCubes, GameObject[,] hideCubes)
    {
        this.resourceCubes = resourceCubes;
        this.hideCubes = hideCubes;
    }

    private void Awake()
    {
        Instance = this;
    }

    public GameObject GetResourceCube(int x, int y) { return resourceCubes[y, x]; }
    public GameObject GetHideCube(int x, int y) { return hideCubes[y, x]; }
}
