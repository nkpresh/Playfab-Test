using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TheGame : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    void Start()
    {
        nameText.text=GameManager.instance.playerName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
