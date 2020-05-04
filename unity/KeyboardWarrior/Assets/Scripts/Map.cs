using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{
    public List<GameObject> players;
    public int moveIndex;

    public static int progress = 1;
    public static int activeIndex;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject g in players)
        {
            g.SetActive(false);
        }
        activeIndex = progress - 1;
        players[activeIndex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // If player has progressed this far
            if (activeIndex + 1 < progress)
            {
                // update active Index
                activeIndex++;
                ResetActive(activeIndex);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            // If player has progressed this far
            if (activeIndex - 1 >= 0)
            {
                // update active Index
                activeIndex--;
                ResetActive(activeIndex);
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Loading level: " + (activeIndex + 1));
            SceneManager.LoadScene(2 + activeIndex + 1);

        }
    }

    public void ResetActive(int index)
    {
        foreach (GameObject g in players)
        {
            g.SetActive(false);
        }
        players[index].SetActive(true);
    }
}
