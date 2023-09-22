using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    // Start is called before the first frame update
    void Start()
    {
        winScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("star hit");
        if(other.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            winScreen.SetActive(true);
        }
    }
}
 