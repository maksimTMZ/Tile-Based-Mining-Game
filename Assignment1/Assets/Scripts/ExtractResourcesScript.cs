using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractResourcesScript : MonoBehaviour
{
    
    private void OnMouseDown()
    {
        int extractsCounter = ModesButtonScript.Instance.extractsCounter;
        if (extractsCounter > 0 && ModesButtonScript.Instance.mode == GameMode.ExtractMode)
        {
            
            Vector2Int cubePos = gameObject.GetComponent<CubeResourceScript>().cubePos;

            int[] offsetX = { 0, 0, 1, -1, -1, 1, 1, -1, 0, 1, 2, 2, 2, 2, 2, 1, 0, -1, -2, -2, -2, -2, -2, -1 };
            int[] offsetY = { 1, -1, 0, 0, -1, -1, 1, 1, 2, 2, 2, 1, 0, -1, -2, -2, -2, -2, -2, -1, 0, 1, 2, 2 };

            for (int i = 0; i < offsetX.Length; i++)
            {
                    int degradateCubeX = cubePos.x + offsetX[i];
                    int degradateCubeY = cubePos.y + offsetY[i];
                    if (degradateCubeX >= 0 && degradateCubeX < HiderCubePositionsScript.Instance.areaWidth && degradateCubeY >= 0 && degradateCubeY < HiderCubePositionsScript.Instance.areaHeight)
                    {
                        var cube = HiderCubePositionsScript.Instance.GetResourceCube(degradateCubeX, degradateCubeY);
                        var crs = cube.GetComponent<CubeResourceScript>();
                        switch(crs.type)
                        {
                            case CubeType.Diamond:
                                crs.SetUpResource(UnityEngine.Random.Range(2000 /2, 5000 /2), CubeType.Gold);
                                break;

                            case CubeType.Gold:
                                crs.SetUpResource(UnityEngine.Random.Range(2000 /4, 5000 /4), CubeType.Silver);
                                break;

                            case CubeType.Silver:
                                crs.SetUpResource(UnityEngine.Random.Range(2000 /16, 5000 /8), CubeType.Rock);
                                break;
                        }
                    }
            }
            ScoreManager.Instance.resourceCount += gameObject.GetComponent<CubeResourceScript>().value;
            GetComponent<CubeResourceScript>().SetUpResource(Random.Range(2000 /16, 5000 /8), CubeType.Rock);
            ModesButtonScript.Instance.extractsCounter--;
        }
        //print("Press Cube: " + transform.position.ToString());
    }
}
