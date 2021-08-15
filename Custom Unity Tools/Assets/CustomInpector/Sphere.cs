using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public float baseSize = 1f;

    // Update is called once per frame
    void Update()
    {
        float animation = baseSize + Mathf.Sin(Time.deltaTime * 0.8f) * baseSize / 7f;
        transform.localScale = Vector3.one * animation;
    }
}
