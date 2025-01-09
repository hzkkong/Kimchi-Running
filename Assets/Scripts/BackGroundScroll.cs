using UnityEngine;

public class BackGroundScroll : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("How fas should the texture scroll?")]
    public float scrollSpeed;

    [Header("References")]
    public MeshRenderer meshRenderer;

    void Start()
    {
        
    }

    void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(scrollSpeed * Time.deltaTime, 0);
    }
}
