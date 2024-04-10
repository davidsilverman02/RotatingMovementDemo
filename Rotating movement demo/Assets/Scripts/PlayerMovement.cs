using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] Transform lookingAt;
    [SerializeField] float playerSpeed;
    [SerializeField] float distFrom;
    [SerializeField] float camDist = 5;
    [SerializeField] float cameraHeight = 5;
    [SerializeField] Vector3 towards;

    Rigidbody rb;
    Vector2 calc;
    Vector2 rotVelo;
    Vector2 ang;
    Vector2 ang2;
    Vector2 ang3;
    Quaternion lerpTo;

    float workingSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        workingSpeed = playerSpeed;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, lerpTo, workingSpeed * Time.deltaTime);
        PlayerMoving();
    }
    void PlayerMoving()
    {
        // is supposed to move based on towards direction, and then around;
        distFrom = new Vector2((lookingAt.position - gameObject.transform.position).x, (lookingAt.position - gameObject.transform.position).z).magnitude;
        towards = (lookingAt.position - gameObject.transform.position).normalized;
        calc = new Vector2(towards.x, towards.z).normalized;

        ang = ((-Input.GetAxisRaw("Horizontal") * workingSpeed * Time.deltaTime * Vector2.Perpendicular(calc)));
        ang2 = ((ang + new Vector2(gameObject.transform.position.x, gameObject.transform.position.z)) - new Vector2(lookingAt.position.x, lookingAt.position.z)).normalized * distFrom; // use this
        ang3 = (ang2 - new Vector2(gameObject.transform.position.x, gameObject.transform.position.z));
        rotVelo = workingSpeed * ang3.normalized;

        //rotVelo = -Input.GetAxisRaw("Horizontal") * workingSpeed * Vector2.Perpendicular(calc);
        rb.velocity = (new Vector3(calc.x, 0, calc.y) * Input.GetAxisRaw("Vertical") * workingSpeed) + new Vector3(rotVelo.x, 0, rotVelo.y) + new Vector3(0, rb.velocity.y, 0);

        playerRotation();

    }
    void playerRotation()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            //lerpTo = Quaternion.LookRotation((((new Vector3(calc.x, 0, calc.y) * Input.GetAxisRaw("Vertical")) + new Vector3(rotVelo.x, 0, rotVelo.y)) / 2.0f).normalized);
        }
    }
    public Vector3 getLook()
    {
        Vector3 looking = this.transform.position - (towards * 4);
        return new Vector3(looking.x, cameraHeight + gameObject.transform.position.y, looking.z);
    }
    public Quaternion getLookAt()
    {
        return Quaternion.LookRotation(new Vector3(calc.x, 0, calc.y), Vector3.up);
    }

    public float getDistance()
    {
        return distFrom;
    }
}
