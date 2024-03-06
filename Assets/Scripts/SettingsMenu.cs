using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    public Image OFF, ON;
    private bool isMute;


    public void ToggleMuteAllAudios()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);

            AudioListener[] audioListeners = (AudioListener[])scene.GetRootGameObjects()
                .SelectMany(g => g.GetComponentsInChildren<AudioListener>())
                .ToArray();

            foreach (AudioListener listener in audioListeners)
            {
                listener.enabled = !listener.enabled;
                isMute = !isMute;
            }
        }
    }

    public void ChangeSlider()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);

            AudioListener[] audioListeners = (AudioListener[])scene.GetRootGameObjects()
                .SelectMany(g => g.GetComponentsInChildren<AudioListener>())
                .ToArray();

            foreach (AudioListener listener in audioListeners)
            {
                AudioListener.volume = slider.value;
            }
        }
        AudioListener.volume = slider.value;

        if(slider.value <= 0 && !isMute){
            ToggleMuteAllAudios();
            ON.enabled = true;
        } else if(slider.value > 0 && isMute){
            ToggleMuteAllAudios();
            ON.enabled = false;
        }
    }
}
