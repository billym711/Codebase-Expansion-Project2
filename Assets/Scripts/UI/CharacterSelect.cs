using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField]
    public GameObject default_char;
    [SerializeField]
    public GameObject dual_char1;
    [SerializeField]
    public GameObject dual_char2;
    [SerializeField]
    public GameObject healthbar_2;
    public static bool defaultChar = true;
    public void selectDefault()
    {
        default_char.SetActive(true);
        dual_char1.SetActive(false);    
        dual_char2.SetActive(false);
        healthbar_2.SetActive(false);
        defaultChar = true;
    }
    public void selectDual()
    {
        default_char.SetActive(false);
        dual_char1.SetActive(true);
        dual_char2.SetActive(true);
        healthbar_2.SetActive(true);
        defaultChar = false;
    }

}
