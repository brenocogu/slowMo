using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeController : MonoBehaviour
{
    public Vector3 explosionPart;
    [SerializeField] ParticleSystem prts;
    private void OnEnable()
    {
        StartCoroutine(SlowMotion());
        foreach (Transform child in transform)
        {
            Rigidbody rbd = child.gameObject.GetComponent<Rigidbody>();
            if(rbd != null)
                rbd.AddExplosionForce(20, explosionPart, 15, 1, ForceMode.Impulse);
        }
        
    }


    IEnumerator SlowMotion()
    {
        float t = 0;
        while (t < 0.5f)
        {
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            Time.timeScale = Mathf.Lerp(1, 0.25f, t / .5f);
        }
    }
}
