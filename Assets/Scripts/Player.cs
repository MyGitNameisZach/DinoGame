using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    private CharacterController character;
    private GameManager gameManager;
    public AudioSource coinSound;
    private Vector3 direction;

    public float jumpForce = 8f;
    public float gravity = 9.81f * 2f;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
        gameManager = GetComponent<GameManager>();
    }

    private void OnEnable()
    {
        direction = Vector3.zero;
    }

    private void Update()
    {
        direction += gravity * Time.deltaTime * Vector3.down;

        if (character.isGrounded)
        {
            direction = Vector3.down;

            if (Input.GetButton("Jump")) {
                direction = Vector3.up * jumpForce;
            }
        }

        character.Move(direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle")) {
            jumpForce = 8f;
            GameManager.Instance.GameOver();
        }else if (other.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            GameManager.Instance.coins++;
            coinSound.Play();

        }else if (other.CompareTag("Potion"))
        {
            other.gameObject.SetActive(false);
            StartCoroutine(JumpBoostCoroutine());
        }


        IEnumerator JumpBoostCoroutine()
        {
            jumpForce += 2;
            Debug.Log("yo");

            yield return new WaitForSeconds(15);

            jumpForce -= 2;

            Debug.Log(jumpForce);
        }

    }
    

}
