using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MazeTimerScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Timer_Text;
    [SerializeField] float Remaining_Time;
    float Start_Time;
    VertexGradient Original_Gradient;

    private void Start()
    {
        Start_Time = Remaining_Time;
        Timer_Text.enableVertexGradient = true;
        Original_Gradient = Timer_Text.colorGradient;
    }

    void Update()
    {
        Remaining_Time -= Time.deltaTime;
        int Minutes = Mathf.FloorToInt(Remaining_Time  / 60);
        int Seconds = Mathf.FloorToInt(Remaining_Time % 60);
        Timer_Text.text = string.Format("{0:00}:{1:00}", Minutes, Seconds);

        float T = Remaining_Time / Start_Time;

        Timer_Text.colorGradient = new VertexGradient(
            Color.Lerp(Color.red, Original_Gradient.topLeft, T),
            Color.Lerp(Color.red, Original_Gradient.topRight, T),
            Color.Lerp(Color.red, Original_Gradient.bottomLeft, T),
            Color.Lerp(Color.red, Original_Gradient.bottomRight, T)
        );

        if (Seconds <= 0 && Minutes <= 0)
        {
            Debug.Log("times up");
            EditorApplication.ExitPlaymode();//will be replaced later
        }
    }
}
