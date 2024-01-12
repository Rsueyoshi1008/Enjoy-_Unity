using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ClearView : MonoBehaviour
{
    [SerializeField] private Button buttonTitle;
    // Start is called before the first frame update

    void Start()
    {
        buttonTitle.onClick.AddListener(OnTitle);
    }

    // Update is called once per frame
    private void OnTitle()
    {
        SceneManager.LoadScene("TitleScene");
        
    }
}
