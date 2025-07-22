using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public float speed;
    public GameObject[] Spikes = new GameObject[5];
    public float spikeSpawnValue;
    private void OnEnable()
    {
        if (gameObject.tag == "Platform")
        {
            if (gameObject.name == "StartPlatform")
            {
                return;
            }

            for(int i = 0; i < Spikes.Length; i++)
            {
                if (Random.value < spikeSpawnValue)
                {
                    Spikes[i].SetActive(true);
                }
                else
                {
                    Spikes[i].SetActive(false);
                }
            }
        }
    }
    void Start()
    {
        spikeSpawnValue = 0.1f;
    }

    void Update()
    {
        if (!GameManager.instance.isGameover)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (gameObject.tag == "BackGround")
            {
                Reposition();
            }

        }

    }
    private void OnBecameInvisible()
    {
        if (gameObject.tag == "Platform" || gameObject.tag == "Dead")
        {
            gameObject.SetActive(false);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" &&  gameObject.tag == "Item")
        {
            gameObject.SetActive(false) ;
        }
    }
    void Reposition()
    {
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        if (boxCollider2D != null)
        {
            //29.47581
            //29.47594
            //29.47581
            float width = boxCollider2D.bounds.size.x;
            if (transform.position.x <= -width)
            {
                transform.position = new Vector2(width , transform.position.y);
            }
        }
    }
}
