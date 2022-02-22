using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class manAxis : MonoBehaviour
{
    Transform player;
    public GameObject man;
    float timer = 0;
    bool timeRunning = true;
    float z;

    public Volume post_processing;
    Vignette _vignette;

    void Start()
    {
        if(post_processing.profile.TryGet<Vignette>(out var vignette))
        {
            _vignette = vignette;
        }
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(waitForNewPosition());   
        man.GetComponent<Transform>().localPosition = new Vector3(0, 0, -400);   
    }

    void Update()
    {
        if(timeRunning)
        {
            timer += Time.deltaTime;
            z = -62 + 1.65f * timer;
            if(z > -10)
            {
                z = -10;
                Debug.Log("LLEGUE");
                timeRunning = false;
            }
            _vignette.intensity.value = (timer/28f)/2;
        }
    }

    IEnumerator waitForNewPosition()
    {
        while(true)
        {
            transform.position = new Vector3(player.position.x, 0, player.position.z);
            transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            man.SetActive(true);
            man.GetComponent<Transform>().localPosition = new Vector3(0, 0, z);
            yield return new WaitForSecondsRealtime(Random.Range(2f, 4.5f));
            man.SetActive(false);
            yield return new WaitForSecondsRealtime(Random.Range(0.25f, 1f));
        }
    }
}
