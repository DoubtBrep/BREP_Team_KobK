using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    static public int score;
    Text text;

    //private void Awake()
    void Start()
    {
        text = GetComponent<Text>();
        //score = 0;
    }

    // Update is called once per frame Player score is increased with this call: Score.score += scoreValue;
    void Update()
    {
        text.text = "Score:" + score;
    }
}
