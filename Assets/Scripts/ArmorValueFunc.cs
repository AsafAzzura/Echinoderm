using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorValueFunc : MonoBehaviour
{
    // example" https://www.youtube.com/watch?v=_RIsfVOqTaE
    public Text ArmorText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ArmorText.text = Armor.ToString();
    }

    public void UpdateArmorValue(int ArmorValue)
    {
          ArmorText.text = ArmorValue.ToString();
    }
}
