using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCounter : MonoBehaviour
{
    public GameObject HeartPrefab;
    List<Graphic> Images;
    Health targetHealth;

    private void Start()
    {
        Initialize();
    }
    void Initialize()
    {
        Player player = FindFirstObjectByType<Player>();

        if (player != null)
        {
            targetHealth = player.healthComponent;
            Images = new List<Graphic>();

            for (int i = 0; i < targetHealth.MaxHp; i++)
            {
                Graphic graphic = Instantiate(HeartPrefab, gameObject.transform).GetComponent<Graphic>();
                Images.Add(graphic);
            }


            targetHealth.HealthChanged += UpdateUI;
        }
    }
    private void OnDestroy()
    {
        targetHealth.HealthChanged -= UpdateUI;
    }

    void UpdateUI(int newAmount)
    {
        for (int i = 0; i < Images.Count; i++)
        {
            if (i < newAmount) Images[i].enabled = true;
            else Images[i].enabled = false;
        }
    }
}
