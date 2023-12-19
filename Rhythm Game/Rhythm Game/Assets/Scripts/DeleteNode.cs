using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteNode : MonoBehaviour
{
    private SpriteRenderer SR;
    public Sprite Idle;
    public Sprite Pressed;
    public BoxCollider2D buttonCollider;
    public KeyCode ButtonPress;

    private void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        buttonCollider.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(ButtonPress)) {
            SR.sprite = Pressed;
            buttonCollider.enabled = true;
            Invoke("makeIdle",0.1f);
        }
    }

    void makeIdle() {
        SR.sprite = Idle;
        buttonCollider.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Note") {
            if (Mathf.Abs(collision.transform.position.y) > 0.6285f) GameManager.addQ();
            else if (Mathf.Abs(collision.transform.position.y) > 0.475f) GameManager.addW();
            else if (Mathf.Abs(collision.transform.position.y) > 0.2675f) GameManager.addE();
            else if (Mathf.Abs(collision.transform.position.y) > 0.05f) GameManager.addR();
            Destroy(collision.gameObject); 
        }
    }
}
