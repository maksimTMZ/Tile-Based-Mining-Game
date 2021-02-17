using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode {
    ScanMode,
    ExtractMode
}

public class ModesButtonScript : MonoBehaviour
{
    [SerializeField] GameObject scanModeText;
    [SerializeField] GameObject extractModeText;

    [SerializeField] GameObject textBox;

    [SerializeField] Button button;

    public int scansCounter = 6;
    public int extractsCounter = 3;
    

    public GameMode mode;

    public static ModesButtonScript Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void SwitchMode()
    {
        if (mode == GameMode.ScanMode)
        {
            EnableExtractMode();
        }
        else if (mode == GameMode.ExtractMode)
        {
            EnableScanMode();
        }
    }

    void UpdateTextBox()
    {
        print("Game Over");
    }

    void EnableScanMode() {
        scanModeText.SetActive(true);
        extractModeText.SetActive(false);
        mode = GameMode.ScanMode;
    }

    void EnableExtractMode() {
        scanModeText.SetActive(false);
        extractModeText.SetActive(true);
        mode = GameMode.ExtractMode;
    }


    private void Update()
    {
        if (scansCounter == 0)
        {
            EnableExtractMode();
            button.enabled = false;
            scansCounter = -1;
        }

        if (extractsCounter == 0)
        {
            extractModeText.SetActive(false);
            UpdateTextBox();
        }
    }
}
