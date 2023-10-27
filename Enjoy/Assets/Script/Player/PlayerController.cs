using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    //[SerializeField] private 
    //private Animator animator;
    [SerializeField] private Rigidbody rb;
	[SerializeField] private CharacterController cCon;
    [SerializeField] private string jumpString;
    [SerializeField] private string catchString;
    [SerializeField] private string horizontalString;
    [SerializeField] private string verticalString;
	private Vector3 velocity;
    
    //private float speed = 5.0f;

	void Start () {
		//animator = GetComponent<Animator>();
		cCon = GetComponent<CharacterController>();
		velocity = Vector3.zero;
        //rb = GetComponent<Rigidbody>();
	}

	void Update () {
		//　キャラクターコライダが接地、またはレイが地面に到達している場合
		// if(cCon.isGrounded) {
            
		// }
        if(Input.GetAxis(horizontalString) != 0.0f || Input.GetAxis(verticalString) != 0.0f)
        {
            float speed = 5.0f;
            var input = new Vector3(Input.GetAxis(horizontalString), 0f, Input.GetAxis(verticalString));
		    // カメラの方向から、X-Z平面の単位ベクトルを取得
            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            // 方向キーの入力値とカメラの向きから、移動方向を決定
            velocity = cameraForward * input.z + Camera.main.transform.right * input.x;
            //移動ベクトルを正規化する
            velocity = velocity.normalized;
            //  走った場合と歩いた場合
            //float speed = runFlag ? _model.RunSpeed : _model.WalkSpeed;
            // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
            rb.velocity = velocity * speed + new Vector3(0, rb.velocity.y, 0);

            Debug.Log(rb.velocity);
        }
        
        // 入力方向に回転する
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(velocity), rotationSpeed);

        
        if(Input.GetButtonDown(catchString))
        {
            Debug.Log("jump");
        }
	}
}
