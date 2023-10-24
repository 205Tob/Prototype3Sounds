 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float JumpForce = 10f;
    public float GravityModifier = 1f;
    public bool IsOnGround = true;
    public bool GameOver = false;
    public ParticleSystem dirtParticle;
    public ParticleSystem ExplosionParticle;
    public AudioClip JumpSound;
    public AudioClip CrashSound;

    
    private Animator playerAnim;
    private Rigidbody _playerRb;
    private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        //_playerRb.AddForce(Vector3.up * 1000);
        Physics.gravity *= GravityModifier;
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && IsOnGround && !GameOver)
        {
            _playerRb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            IsOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game OVer");
            GameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            ExplosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(CrashSound, 1.0f);

        }
    }
}
