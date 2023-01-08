using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Button StartButton;
    public Button InstructionButton;

    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener((LoadGameScene));
        if (InstructionButton != null)
        {
            InstructionButton.onClick.AddListener((LoadInstructionScene));
        }
    }

    void LoadGameScene(){
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
    
    void LoadInstructionScene(){
        SceneManager.LoadScene("InstructionsScene", LoadSceneMode.Single);
    }
}
