using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour {

    float m_MySliderValue;

    void Start()
    {
        m_MySliderValue = 0.5f;
        AudioListener.volume = m_MySliderValue;
    }

    void OnGUI()
    {
        m_MySliderValue = GUI.HorizontalSlider(new Rect(25, 600, 200, 60), m_MySliderValue, 0.0F, 1.0F);
        AudioListener.volume = m_MySliderValue;
    }
}
