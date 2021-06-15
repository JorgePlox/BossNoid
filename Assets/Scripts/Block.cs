using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    normalBlock,
    bossBlock, 
    lastBossBlock
}

public class Block : MonoBehaviour
{
    // El primero e sla duración del bloque
    //el segundo la duración minima cuando no puede seguir siendo golpeado por requisito previo
    public int blockDuration = 1;
    [SerializeField] int blockDurationProtected = 1;

    public int maxBlockDuration;


    public GameObject[] previusBlocks;

    public BlockType blockType = BlockType.normalBlock;

    public bool isDead = false;

    //GameFeel
    SpriteRenderer spriteRenderder;

    private void Awake()
    {
        maxBlockDuration = blockDuration;
    }

    private void Start()
    {
        spriteRenderder = GetComponent<SpriteRenderer>();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            blockDuration--;
            StartCoroutine(ColorBlock());

            //La bola hace el sonido
            collision.gameObject.GetComponent<Ball>().PlayHitSound();



            if (!ProtectionOver() && blockDuration < blockDurationProtected)
            {
                blockDuration = blockDurationProtected;
            }

            else if (blockDuration <= 0)
            {
                switch (blockType)
                {
                    case BlockType.normalBlock:
                        Destroy(this.gameObject);
                        GameManager.sharedInstance.DecreaseBlockCount();
                        break;

                    case BlockType.bossBlock:
                        DestroyBossBlock();
                        GameManager.sharedInstance.DecreaseBlockCount();
                        break;

                    case BlockType.lastBossBlock:
                        DestroyBossBlock();
                        break;

                }

            }

        }
    }

    bool ProtectionOver()
    {

        for (int i = 0; i < previusBlocks.Length; i++)
        {
            if (previusBlocks[i] != null)
            {
                return false;
            }

        }

        return true;

    }



    IEnumerator ColorBlock()
    {
        if (spriteRenderder != null)
        {
            spriteRenderder.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderder.color = Color.white;
        }
    }

    private void DestroyBossBlock()
    {
        this.GetComponent<Collider2D>().enabled = false;
        
        if (blockType != BlockType.lastBossBlock)
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
            
            if (GetComponentInChildren<ParticleSystem>() != null)
            {
                GetComponentInChildren<ParticleSystem>().Play();
                Destroy(this.gameObject, GetComponentInChildren<ParticleSystem>().main.duration);
            }

            else
            {
                Destroy(this.gameObject);
            }
        }
        else 
        {
            isDead = true;
        }
        
    }

    public void PlayFinalParticle()
    {
        GetComponentInChildren<ParticleSystem>().Play();
    }

    public void KillBoss()
    {
        GameManager.sharedInstance.DecreaseBlockCount();
        Destroy(this.gameObject);
    }

}
