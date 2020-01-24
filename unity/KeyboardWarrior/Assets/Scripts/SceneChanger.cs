using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(ChangeScene);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ChangeScene()
    {
        SceneManager.LoadScene(1);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
    }
}
