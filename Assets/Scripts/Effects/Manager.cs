using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour
{
    private const string PARENT_NAME = "--- MANAGERS ---";

    protected void SetParent()
    {
        GameObject lTryParent = GameObject.Find(PARENT_NAME);

        GameObject lParent =
            lTryParent != null?
            lTryParent :
            new GameObject(PARENT_NAME);

        transform.SetParent(lParent.transform);
    }
}
