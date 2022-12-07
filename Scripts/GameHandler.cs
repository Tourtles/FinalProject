using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameHandler : MonoBehaviour
{
    public TextMeshProUGUI CoinText;
    public int coins;

    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D Col)
    {
        if(Col.gameObject.tag == "Coin")
        {
            //Debug.Log("Coin Collected!");
            coins = coins + 1;
            //Col.gameObject.SetActive(false);
            Destroy(Col.gameObject);
            CoinText.text = "Coins : " + coins;
        }
    }
}
