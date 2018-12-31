using System;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class player_controller : MonoBehaviour
{

    [SerializeField]
    private float DefSpeed = 5f;
    private float CurretSpeed;
    [SerializeField]
    float DefStepTime;
    float CurretDefStepTime;
    float curretsteptime;

    public AudioClip[] StepSounds;

    public AudioClip LandSound;
    public AudioClip JumpSound;

    bool canJump = true;

    private Rigidbody rb;
    void Start()
    {
        CurretDefStepTime = DefStepTime;
        CurretSpeed = DefSpeed;
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))//Бег
        {
            CurretDefStepTime = DefStepTime / 2f;
            CurretSpeed = DefSpeed * 2f;
        }
        else
        {
            CurretDefStepTime = DefStepTime;
            CurretSpeed = DefSpeed;
        }
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 moveHor = transform.right * xMov;//Вектор движения по оси X 
        Vector3 moveVer = transform.forward * zMov;//Вектор движения по оси Z

        Vector3 velocity = (moveHor + moveVer).normalized * CurretSpeed;//Вектор движения

        Move(velocity);

        if ((canJump == true) && Input.GetKeyDown(KeyCode.Space))//Прыжок
        {
            GetComponent<AudioSource>().PlayOneShot(JumpSound);
            rb.AddForce(transform.up * 5f, ForceMode.Impulse);
            canJump = false;
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.relativeVelocity.magnitude > 2)
        {
            canJump = true;
            GetComponent<AudioSource>().PlayOneShot(LandSound);
        }
    }
    void Move(Vector3 _velocity)//Функция передвижения
    {
        if(_velocity != Vector3.zero)
        {
            System.Random rnd = new System.Random();
            int rndaudint = rnd.Next(0, StepSounds.Length);
            AudioClip RndStep = StepSounds[rndaudint];
            if (curretsteptime < CurretDefStepTime)
            {
                curretsteptime += Time.deltaTime;
            }
            if(curretsteptime >= CurretDefStepTime)
            {
                GetComponent<AudioSource>().PlayOneShot(RndStep);
                curretsteptime = 0f;
            }
            rb.MovePosition(rb.position + _velocity * Time.fixedDeltaTime);
        }
    }
}
