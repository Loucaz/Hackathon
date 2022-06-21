using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchBar 
{
    public static string getCode(string search)
    {
        if (int.TryParse(search, out int n))
            return search;
        switch (search.ToLower())
        {
            case "jean":
                return "5230";
            case "polo":
                return "5229";
            case "socks":
                return "3764";
            default:
                return "4208";
        }
    }
}
