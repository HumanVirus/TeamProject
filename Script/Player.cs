using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  
    private GameObject obj;
    Animator animator;
    
    [SerializeField]
    private float walkspeed;

    [SerializeField]
    private float crunchspeed;

    [SerializeField]
    private float Runspeed;
    private float applyspeed;

    [SerializeField]
    private float jumpForce;
 

    private bool isWalk = false;
    private bool isRun=false;
    private bool isGround = true;
    private bool isCrunch = false;

    //움직임 체크변수
    
    private Vector3 lastpos;

    [SerializeField]
    private float CrunchposY;
    private float originposY;
    private float applyCrunchposY;

    private CapsuleCollider capsuleCollider;


    [SerializeField]
    private float lookSensitivity;

    [SerializeField]
    private float cameraRotationLimit;
    private float currentCamerarotationX = 0;

    [SerializeField]
    private Camera thecamera;
    private Rigidbody myrigid;
    private GunController gunController;
    private CrossHair crossHair;


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        myrigid = GetComponent<Rigidbody>();
        applyspeed = walkspeed;
        originposY = thecamera.transform.localPosition.y;
        applyCrunchposY = originposY;
        gunController = FindObjectOfType<GunController>();
        crossHair = FindObjectOfType<CrossHair>();

        
    }

    void Update()
    {
        //hAxis = Input.GetAxisRaw("Horizontal");
        //vAxis = Input.GetAxisRaw("Vertical");
        //wRun = Input.GetButtonDown("Run");

        //move = new Vector3(hAxis, 0, vAxis).normalized;

        //transform.position += move * speed * (wRun ? 0.3f : 1f) * Time.deltaTime;

        //animator.SetBool("isWalk", move != Vector3.zero);
        //animator.SetBool("isRun", wRun);

        //transform.LookAt(transform.position + move);
        IsRun();
        IsJump();
        IsGround();
        IsCrunch();
        float CheckXZ = Move();
        MoveCheck(CheckXZ);
        CameraRotation();
        CharacterRotation();

        

    }
    //private void FixedUpdate()
    //{
        
    //}
    private void IsCrunch() 
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crunch();
        }
    
    }
    private void Crunch()
    {
        isCrunch = !isCrunch;
        crossHair.CrunchingAni(isCrunch);
        if (isCrunch)
        {
            applyspeed = crunchspeed;
            applyCrunchposY = CrunchposY;
        }
       else
        {
            applyspeed = walkspeed;
            applyCrunchposY = originposY;
        }
        StartCoroutine(CrunchCoroutine());
    }

    IEnumerator CrunchCoroutine()
    {
        float _posY = thecamera.transform.localPosition.y;
        int count = 0;
        while(_posY != applyCrunchposY)
        {
            count++;
            _posY = Mathf.Lerp(_posY, applyCrunchposY, 0.3f);
            thecamera.transform.localPosition = new Vector3(0, _posY, 0);
            if (count > 15)
                break;
            yield return null;
        }
        thecamera.transform.localPosition = new Vector3(0, applyCrunchposY, 0f);

    }


    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
        crossHair.JumpingAni(!isGround);
    }
    private void IsJump()
    {
        if(Input.GetKeyDown(KeyCode.Space)&& isGround)
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (isCrunch)
            Crunch();
        //gunController.CancelFineSight();
        myrigid.velocity = transform.up * jumpForce;
    }
    private void IsRun()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            Running();
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            NoRunning();
        }

    }
    private void NoRunning()
    {
        isRun = false;
        crossHair.RunningAni(isRun);
        gunController.RunningAni(isRun);
        applyspeed = walkspeed;
    }
    private void Running()
    {
        if (isCrunch)
            Crunch();
        gunController.CancelFineSight();
        isRun = true;
        gunController.RunningAni(isRun);
        crossHair.RunningAni(isRun);
        applyspeed = Runspeed;
    }
    private float Move()
    {
       float _moveDixX = Input.GetAxisRaw("Horizontal");
       float _moveDixZ = Input.GetAxisRaw("Vertical");
        float moveXZ = Mathf.Abs(_moveDixX) + Mathf.Abs(_moveDixZ);

        Vector3 _moveHorizontal = transform.right * _moveDixX;
        Vector3 _moveVertical = transform.forward * _moveDixZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applyspeed;

        myrigid.MovePosition(transform.position + _velocity * Time.deltaTime);
        return moveXZ;

    }
    private void MoveCheck(float CheckXZ)
    {
        if (!isRun && !isCrunch && isGround)
        {
            Debug.Log(CheckXZ);
            if (CheckXZ !=0)
            {
                isWalk = true;
            }
            else
            {
                isWalk = false;
            }
            crossHair.WalkingAni(isWalk);
            lastpos = transform.position;
        }
    }
    private void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myrigid.MoveRotation(myrigid.rotation * Quaternion.Euler(_characterRotationY));
    }
    private void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotaionX = _xRotation * lookSensitivity;
        currentCamerarotationX -= _cameraRotaionX;
        currentCamerarotationX = Mathf.Clamp(currentCamerarotationX, -cameraRotationLimit, cameraRotationLimit);

        thecamera.transform.localEulerAngles = new Vector3(currentCamerarotationX, 0.0f, 0.0f);
    }

   

}
