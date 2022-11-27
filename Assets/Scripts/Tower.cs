using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tower : MonoBehaviour
{
    public float life;
    public Slider slider;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponentInChildren<Slider>(true);
        slider.maxValue = life;
        slider.value = life;

    }

    // Update is called once per frame
    void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
        if (life <= 0) gameObject.SetActive(false);
    }

    public bool TakeHit(float damage)
    {
        if(life > 0)
        {
            slider.gameObject.SetActive(true);
            slider.value -= damage;
            life -= damage;
            return false;
        }
        return true;

    }
}
