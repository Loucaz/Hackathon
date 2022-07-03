using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerChat : MonoBehaviour
{
    public Text chat;
    public InputField message;

    public Emoticone emoteVendeur;

    private int ctp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Message()
    {
        if (String.IsNullOrEmpty(message.text))
            return;
        AddMessage(message.text, "Bob");
        message.text = "";
    }
    public void AddMessage(string message,string author)
    {
        chat.text += author + ": " + message + "\n";

        StartCoroutine(SpritedAddMessage());
    }

    private IEnumerator SpritedAddMessage()
    {

        ctp++;
        yield return new WaitForSeconds(1f);
        switch (ctp)
        {
            case 1:
                chat.text += "Vendeur: Bonjour !\n";
            break;
            case 2:
                chat.text += "Vendeur: Oui dites moi ?\n";

                yield return new WaitForSeconds(0.5f);
                emoteVendeur.SeeEmote();
                break;
            case 3:
                chat.text += "Vendeur: Ahah non pas encore !\n";
                break;

        }

    }
}
