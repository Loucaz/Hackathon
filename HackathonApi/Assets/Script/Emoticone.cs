using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emoticone : MonoBehaviour
{

    private new Rigidbody2D rigidbody;
    public Sprite[] emoticones;
    public bool actif = false;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = this.transform.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!actif && Input.GetKeyDown(KeyCode.Keypad1))
        {
            actif = true;
            SeeEmote();
        }
    }

    public void SeeEmote()
    {
        StartCoroutine(AfficheEmote());
    }

    private IEnumerator AfficheEmote()
    {
        GameObject emote = new GameObject();

        SpriteRenderer sr = emote.AddComponent<SpriteRenderer>();
        sr.sprite = emoticones[Random.Range(0,emoticones.Length)];
        sr.sortingOrder = 2;
        emote.transform.SetParent(rigidbody.transform);
        emote.transform.position = rigidbody.position + new Vector2(0,1);
        emote.transform.localScale /= 2; 
        yield return new WaitForSeconds(2f);
        Destroy(emote);


        actif = false;
    }
}
