using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
