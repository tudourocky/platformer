using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackItemController : MonoBehaviour
{
    public float nextUseTime;
    public bool canUse;
    // Start is called before the first frame update
    void Start()
    {
        canUse = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextUseTime)
        {
            canUse = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canUse)
        {
            return;
        }
        if (collision.tag == "Player")
        {
            Debug.Log("touch");
            collision.GetComponent<PlayerCombat>().attackBoost();
        }
        if (collision.tag == "Player2")
        {
            collision.GetComponent<PlayerCombat2>().attackBoost();
        }
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        nextUseTime = Time.time + 5f;
        canUse = false;
    }
}
