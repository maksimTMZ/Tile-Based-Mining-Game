using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCubesOnClickScript : MonoBehaviour
{
    public Vector2Int hiderCubePos;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnMouseEnter()
    //{
    //    print("On Cube: " + transform.position.ToString());
    //}

    private void OnMouseDown()
    {
        int scansCounter = ModesButtonScript.Instance.scansCounter;
        if (scansCounter > 0 && ModesButtonScript.Instance.mode == GameMode.ScanMode)
        {
            Destroy(gameObject);
            Vector2Int cubePos = gameObject.GetComponent<DestroyCubesOnClickScript>().hiderCubePos;

            int[] offsetX = { 0, 0, 1, -1, -1, 1, 1, -1 };
            int[] offsetY = { 1, -1, 0, 0, -1, -1, 1, 1 };

            var cube = HiderCubePositionsScript.Instance.GetHideCube(cubePos.x, cubePos.y);

            for (int i = 0; i < offsetX.Length; i++)
            {
                    int destroyableCubeX = cubePos.x + offsetX[i];
                    int destroyableCubeY = cubePos.y + offsetY[i];
                    if (destroyableCubeX >= 0 && destroyableCubeX < HiderCubePositionsScript.Instance.areaWidth && destroyableCubeY >= 0 && destroyableCubeY < HiderCubePositionsScript.Instance.areaHeight)
                    {
                        var destroyableCube = HiderCubePositionsScript.Instance.GetHideCube(destroyableCubeX, destroyableCubeY);
                        Destroy(destroyableCube);
                        print("CubeX: " + destroyableCubeX.ToString());
                        print("CubeY: " + destroyableCubeY.ToString());
                    }
                    
            }
            ModesButtonScript.Instance.scansCounter--;
        }
        //print("Press Cube: " + transform.position.ToString());
    }
}
