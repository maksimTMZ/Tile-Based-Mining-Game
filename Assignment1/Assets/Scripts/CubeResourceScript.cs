using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CubeType
{
    Diamond = 4,
    Gold = 3,
    Silver = 2,
    Rock = 1
}

public class CubeResourceScript : MonoBehaviour
{
    public float value;
    public CubeType? type;

    public Vector2Int cubePos; 

    public void SetUpResource(float value, CubeType type)
    {
        this.value = value;
        this.type = type;

        GetComponent<MeshRenderer>().material = ColorCubesScript.Instance.GetMaterial(type);
    }
}
