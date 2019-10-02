using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnergyScript : MonoBehaviour
{
    public GameObject obj;
    TextMeshProUGUI tm;
    PlayerController pc;

    void Awake()
    {
        pc = obj.GetComponent<PlayerController>();
        tm = GetComponent<TextMeshProUGUI>();

        tm.text = pc.Energy.ToString();
    }

    
    void Update()
    {
        tm.text = (Mathf.Ceil(pc.Energy / 10)).ToString();
    }
}
