using UnityEngine;

public class Heart : MonoBehaviour
{
    public Sprite OnHeart;
    public Sprite OffHeart;

    public SpriteRenderer spriteRenderer;
    public int LiveNumber;

    
    void Start()
    {
        
    }

    void Update()
    {
        if(GameManager.Instance.Lives >= LiveNumber)
        {
            spriteRenderer.sprite = OnHeart;
        }
        else
        {
            spriteRenderer.sprite = OffHeart;
        }
    }
}
