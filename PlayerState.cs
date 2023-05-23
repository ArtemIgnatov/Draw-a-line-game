using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public bool Finished { get; private set; }

    private void Start()
    {
        Finished = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish") Finished = true;
        if (collision.gameObject.tag == "Player") FindAnyObjectByType<GameManager>().Lose();
        if (collision.gameObject.tag == "Wall") FindAnyObjectByType<GameManager>().Lose();
    }
}
