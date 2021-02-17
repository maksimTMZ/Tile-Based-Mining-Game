using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    public float resourceCount;

    [SerializeField] Text scoreText;
    [SerializeField] Text messageText;
   
    public static ScoreManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = resourceCount.ToString();

        if (ModesButtonScript.Instance.extractsCounter == 0)
        {
            messageText.text = "Game Over";
        }
    }
}
