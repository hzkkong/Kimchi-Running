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
        // platform 가로 길이 20 -> offset / 20
        meshRenderer.material.mainTextureOffset += new Vector2(scrollSpeed * GameManager.Instance.CalculateGameSpeed() / 20 * Time.deltaTime, 0);
    }
}
