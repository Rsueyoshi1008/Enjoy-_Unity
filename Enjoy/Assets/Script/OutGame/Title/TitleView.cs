using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TitleView : MonoBehaviour
{
    [SerializeField] private Button buttonNext;
    // Start is called before the first frame update
    void Start()
    {
        buttonNext.onClick.AddListener(OnStart);
    }

    // Update is called once per frame
    private void OnStart()
    {
        SceneManager.LoadScene("Stage1Scene");
        
        
    }
}
