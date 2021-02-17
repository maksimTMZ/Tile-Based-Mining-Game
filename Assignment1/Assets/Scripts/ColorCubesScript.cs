using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCubesScript : MonoBehaviour
{
    public static ColorCubesScript Instance { get; private set; }

    [SerializeField] List<CubeType> types;
    [SerializeField] List<Material> materials;

    private Dictionary<CubeType, Material> materialsDict;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;

        materialsDict = new Dictionary<CubeType, Material>();

        for (int i = 0; i < types.Count; ++i)
            materialsDict.Add(types[i], materials[i]);
    }

    void Start()
    {
    }

    public Material GetMaterial(CubeType type)
    {
        return materialsDict[type];
    }
}
