using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CogsCollectible : MonoBehaviour
{
    public TextMeshProUGUI CogsText;
    public int cogs;
    
    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D Col)
    {
        if(Col.gameObject.tag == "Cogs")
        {
            //Debug.Log("Ammo Collected!");
            cogs = cogs + 4;
            //Col.gameObject.SetActive(false);
            Destroy(Col.gameObject);
            CogsText.text = "Cogs 4: " + cogs;
        }
    }
}
