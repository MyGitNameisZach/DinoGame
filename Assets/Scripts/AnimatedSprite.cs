using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] sprites;
    public Sprite[] newskin;
    private Sprite[] skinholder;
    private SpriteRenderer spriteRenderer;
    private int frame;
    public bool boolSkin = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        skinholder = sprites;
    }

    private void OnEnable()
    {
        Invoke(nameof(Animate), 0f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
    public void changeskin()
    {
        skinholder = newskin;
    }

    private void Animate()
    {
        frame++;

        if (frame >= sprites.Length) {
            frame = 0;
        }

        if (frame >= 0 && frame < sprites.Length)
        {
            spriteRenderer.sprite = skinholder[frame];
        }
        
       

        Invoke(nameof(Animate), 1f / GameManager.Instance.gameSpeed);
    }

}
