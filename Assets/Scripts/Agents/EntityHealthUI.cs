using UnityEngine;
using UnityEngine.UI;


public class EntityHealthUI : MonoBehaviour
{
    private Slider slider;

    void Awake()
    {
        slider = GetComponentInChildren<Slider>(true);

        //Look if the canvas has a UI camera assigned
        Canvas canvas = GetComponent<Canvas>();

        if (canvas.worldCamera != null)
        {
            GameObject cameraGO = GameObject.FindGameObjectWithTag("UI Camera");
            Camera camera = cameraGO.GetComponent<Camera>();

            if (camera != null)
            {
                canvas.worldCamera = camera;
                Debug.Log("World Camera set for " + gameObject.name + ". Make sure to set it after play mode");
            }
            else Debug.LogWarning("UI Camera not found");
        }
    }

    public void SetUpSlider(float maxHealth)
    {
        slider.gameObject.SetActive(false);
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void UpdateHealthUI(float health)
    {
        if (health > 0)
        {
            slider.gameObject.SetActive(true);
            slider.value = health;
        }
        else slider.gameObject.SetActive(false);

    }
}
