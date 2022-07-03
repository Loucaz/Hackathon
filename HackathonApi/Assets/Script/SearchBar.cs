using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SearchBar : MonoBehaviour
{
    public static SearchBar instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Une instance de SearchBar est déjà existante!");
            return;
        }
        instance = this;
    }
    public KeyValuePair<string, CodeBool> getCode(string search)
    {
        return new KeyValuePair<string, CodeBool>(search,Search[search]);
    }

    public Dictionary<string, CodeBool> Search = new Dictionary<string, CodeBool>
    {
        { "5230",new CodeBool("jean",false)},
        { "5229",new CodeBool("polo",false)},
        { "3764",new CodeBool("socks",false)},
    };
    public class CodeBool
    {
        public string name;
        public bool actif;

        public CodeBool(string name, bool actif)
        {
            this.name = name;
            this.actif = actif;
        }
    }


}
