using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player instance;

    private int lifePoints = 6;
    [SerializeField] private List<GameObject> bodyParts;
    [SerializeField] private ParticleSystem particleHit;
    private bool canTakeDamage = true;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);

        else instance = this;
    }

    public static Player getInstance()
    {
        if (instance == null) new GameObject("Player").AddComponent<Player>();
        return instance;
    }

    public void TakeDamage()
    {
        if (canTakeDamage)
        {
            StartCoroutine(LosePart());
            if(lifePoints <= 0)
            {
                GameManager.getInstance().GameOver();
            }
        }
    }

    IEnumerator LosePart()
    {
        canTakeDamage = false;
        lifePoints--;
        particleHit?.Play();

        for (int i = bodyParts.Count - 1; i >= 0; i--)
        {
            if (i == lifePoints)
            {
                GameObject lPart = bodyParts[i];
                lPart.GetComponent<Collider2D>().enabled = false;
                lPart.GetComponent<SpriteRenderer>().enabled = false;
                break;
            }
        }
        yield return new WaitForSeconds(1f);
        canTakeDamage = true;
    }
}
