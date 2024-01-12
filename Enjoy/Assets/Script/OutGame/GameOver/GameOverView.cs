using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class GameOverView : MonoBehaviour
{
    [SerializeField] private Button buttonTitle;
    [SerializeField] private Button buttonGame;
    // Start is called before the first frame update
    void Start()
    {
        buttonTitle.onClick.AddListener(OnTitle);
        buttonGame.onClick.AddListener(OnReGame);
    }

    // Update is called once per frame
    private void OnTitle()
    {
        SceneManager.LoadScene("TitleScene");
        
    }
    private void OnReGame()
    {
        SceneManager.LoadScene("Stage1Scene");
    }
}
