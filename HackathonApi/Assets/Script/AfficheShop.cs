using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfficheShop : MonoBehaviour
{
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseEnter()
    {
        sprite.enabled = true;
    }
    void OnMouseExit()
    {
        sprite.enabled = false;
    }

    IEnumerator OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {

            GameObject g = GameObject.Find("Camera");
            g.GetComponent<CircleWipeController>().FadeOut();
            yield return new WaitForSeconds(2);
            g.transform.position = g.transform.position + new Vector3(40, 0);

            g.GetComponent<CircleWipeController>().FadeIn();

            yield return new WaitForSeconds(2);
            ControllerCanvas.instance.canvas.SetActive(true);

        }
    }

    public IEnumerator Login()
    {
            GameObject g = GameObject.Find("Camera");
            g.GetComponent<CircleWipeController>().FadeOut();
            yield return new WaitForSeconds(1);
            g.transform.position = g.transform.position + new Vector3(40, 0);

            g.GetComponent<CircleWipeController>().FadeIn();

    }

    public void LoginButton()
    {
        StartCoroutine(Login());
    }
}
