using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBody : MonoBehaviour
{
    void endOfAnimation()
    {
        Destroy(gameObject);
    }
}
