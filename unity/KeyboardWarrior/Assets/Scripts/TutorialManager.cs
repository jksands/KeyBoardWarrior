using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField][HideInInspector]
    private List<string> dialog;
    [SerializeField][HideInInspector]
    private List<string> advanceWords;

    public float timeBetweenLetters = .1f;
    public Text dialogBox;
    public GameObject advanceBox;
    private Text advanceBoxText;
    private Text advanceBoxOverlay;


    private bool canType;
    private float timeBeforeCanType = 1f;

    private float timer = 0f;
    /// <summary>
    /// The index of the dialog entry being displayed on the screen
    /// </summary>
    private int dialogEntryIndex;
    /// <summary>
    /// The index of the next character in the dialog entry to add to the display
    /// </summary>
    private int displayIndex;
    /// <summary>
    /// The index of the last correctly typed letter the player entered from the advanceWord
    /// </summary>
    private int charIndex;
    

    // Start is called before the first frame update
    void Start()
    {
        string str;
        string[] seperator = { "\\n" };
        for (int i = 0; i<dialog.Count; i++)
        {
            if ((str = dialog[i]).Contains("\\n"))
            {
                string[] strArray = str.Split(seperator, System.StringSplitOptions.RemoveEmptyEntries);
                dialog[i] = string.Join("\n", strArray);
            }
        }

        advanceBoxText = advanceBox.transform.Find("Text").GetComponent<Text>();
        advanceBoxOverlay = advanceBox.transform.Find("ColorOverlay").GetComponent<Text>();

        dialogEntryIndex = 0;
        dialogBox.text = "";
        SetAdvanceBoxText("");
        canType = true;
        advanceBoxOverlay.color = Color.yellow;

        timer = 0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(2);
        }

        if (displayIndex < dialog[dialogEntryIndex].Length)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenLetters)
            {
                timer -= timeBetweenLetters;
                dialogBox.text += dialog[dialogEntryIndex][displayIndex];
                displayIndex++;
            }

            if (Input.anyKeyDown)
            {
                dialogBox.text = dialog[dialogEntryIndex];
                displayIndex = dialogBox.text.Length;
            }
        }
        else if (!canType)
        {
            timer += Time.deltaTime;
            if (timer >= timeBeforeCanType)
            {
                canType = true;
                advanceBoxText.color = Color.white;
            }
        }
        else
        {
            if (advanceBoxText.text != advanceWords[dialogEntryIndex])
            {
                SetAdvanceBoxText(advanceWords[dialogEntryIndex]);
                charIndex = -1;
            }

            char currentChar = ' ';
            if (advanceWords[dialogEntryIndex].Length > 0)
                currentChar = advanceWords[dialogEntryIndex][charIndex + 1];
            

            if (advanceWords[dialogEntryIndex].Length>0 && Input.GetKeyDown(currentChar.ToString()))
            {
                charIndex++;
                advanceBoxOverlay.text += advanceWords[dialogEntryIndex][charIndex];

                if (charIndex+1 == advanceWords[dialogEntryIndex].Length)
                {
                    OnWordComplete();
                }
            }
            else if (Input.anyKeyDown && dialogEntryIndex == 0)
            {
                OnWordComplete();
            }
            else if (Input.anyKeyDown)
            {
                charIndex = -1;
                advanceBoxOverlay.text = "";
                advanceBoxText.color = Color.red;
                canType = false;
                timer = 0;
            }
        }
    }

    void OnWordComplete()
    {
        // Load the next dialog
        if (dialogEntryIndex + 1 == dialog.Count)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            displayIndex = 0;
            dialogEntryIndex++;
            dialogBox.text = "";
            timer = 0f;
            SetAdvanceBoxText("");
        }
    }

    void SetAdvanceBoxText(string text)
    {
        advanceBoxText.text = text;
        advanceBoxOverlay.text = "";

        float width = advanceBoxText.preferredWidth;
        advanceBox.transform.localPosition = new Vector3(-width / 2, advanceBox.transform.localPosition.y, 0);
    }
}
