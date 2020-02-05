using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField][HideInInspector]
    private List<string> dialog;
    [SerializeField][HideInInspector]
    private List<string> advanceWords;

    public Text dialogBox;
    public GameObject advanceBox;
    private Text advanceBoxText;
    private Text advanceBoxOverlay;


    // Start is called before the first frame update
    void Start()
    {
        advanceBoxText = advanceBox.transform.FindChild("Text").GetComponent<Text>();
        //float width = advanceBoxText.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
