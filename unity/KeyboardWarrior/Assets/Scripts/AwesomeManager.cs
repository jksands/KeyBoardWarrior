using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwesomeManager : MonoBehaviour
{
    public List<GameObject> awesomeWords;

    private List<GameObject> activeWords;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activeWords.Count > 0)
        {
            for (int i = 0; i < activeWords.Count; i++)
            {

            }
        }
    }

    public void SpawnAwesome()
    {
        activeWords.Add(
            Instantiate(awesomeWords[Random.Range(0, awesomeWords.Count)], Vector3.zero, Quaternion.identity)
            );
    }
}
