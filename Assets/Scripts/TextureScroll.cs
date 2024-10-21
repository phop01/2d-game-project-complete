using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScroll : MonoBehaviour
{
    public float Scrollspeed;
    public bool isScroll = true;
    Material bgMaterial;

    private void Awake()
    {
        bgMaterial = GetComponent<Renderer>().material;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isScroll) // ตรวจสอบว่า isScroll เป็น true หรือไม่
        {
            Vector2 offset = new Vector2(Scrollspeed * Time.time, 0f);
            bgMaterial.mainTextureOffset = offset;
        }
    }
}