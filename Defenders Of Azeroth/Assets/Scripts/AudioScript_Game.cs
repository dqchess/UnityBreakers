using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript_Game : MonoBehaviour {

    float m_MySliderValue;

    void Start()
    {
        m_MySliderValue = 0.15f;
        AudioListener.volume = m_MySliderValue;
    }

    void OnGUI()
    {
        m_MySliderValue = GUI.HorizontalSlider(new Rect(1000, 25, 200, 60), m_MySliderValue, 0.0F, 1.0F);
        AudioListener.volume = m_MySliderValue;
    }
}
