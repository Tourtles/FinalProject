using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public GameHandler GH;
    public AudioClip coinSound;
    // Start is called before the first frame update
    void Start()
    {
        GH  = GameObject.Find("UI").GetComponent<GameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        AudioSource.PlayClipAtPoint(coinSound, transform.position);
        Destroy(gameObject);
    }

}
