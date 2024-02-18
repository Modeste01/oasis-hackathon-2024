using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
    // Opens a link when called
    public void OpenALink(string link)
    {
        Application.OpenURL(link);
    }
}
