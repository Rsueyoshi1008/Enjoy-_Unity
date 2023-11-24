using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    //[SerializeField] private 
    //private Animator animator;
    private Rigidbody rb; //物理演算をするコンポーネント
    [SerializeField] private Camera playerCamera; //Playerを追従するカメラ
    float moveSpeed = 500.0f; //Playerの移動速度
    float jumpPower = 5.0f; //Playerのジャンプ力
    bool jumpFlag = false; //ジャンプ中かどうかの追跡フラグ
    bool cathFlag = false; //掴み中の追跡フラグ

	void Start () {
		//animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        
	}

	void Update () {
        //フラグ・変数の初期化
        cathFlag = false;

		//　キャラクターコライダが接地、またはレイが地面に到達している場合
		// if(cCon.isGrounded) {
            
		// }
        if(Gamepad.current != null) //ゲームパッドが接続されてなかったらゲーム終了
        {
            if(Gamepad.current.leftStick.ReadValue() != new Vector2(0.0f,0.0f))
            {
                var input = Gamepad.current.leftStick.ReadValue();

                Vector3 cameraForward = Vector3.Scale(playerCamera.transform.forward, new Vector3(1, 0, 1)).normalized;// カメラの方向から、X-Z平面の単位ベクトルを取得

                Vector3 velocity = cameraForward * input.x + playerCamera.transform.right * input.y;// 方向キーの入力値とカメラの向きから、移動方向を決定

                velocity = velocity.normalized;//移動ベクトルを正規化する

                //float speed = runFlag ? _model.RunSpeed : _model.WalkSpeed;//  走った場合と歩いた場合

                rb.velocity = new Vector3(velocity.x * moveSpeed, rb.velocity.y, velocity.z * moveSpeed);// 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。

                //Debug.Log("Velocity" + rb.velocity);


            } 
            if(jumpFlag == false)
            {
                if(Gamepad.current.buttonSouth.wasPressedThisFrame) //Xボタンの条件分岐
                {
                    rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                    jumpFlag = true; // ジャンプ中のフラグを立てる
                }
            }
            
            if(Gamepad.current.leftShoulder.isPressed) //Lボタンの条件分岐
            {
                cathFlag = true;
                
            }
            Debug.Log(cathFlag);
        }
        else
        {
            #if UNITY_EDITOR //UnityEditorでプレイしているとき
                UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了

            #else  //ビルドしたゲームをプレイしいているとき
                Application.Quit();//ゲームプレイ終了

                #endif
        }
        
        
        
        //Debug.Log(rb.velocity);
        // 入力方向に回転する
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(velocity), rotationSpeed);
	}
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // 地面に接触したら
        {
            jumpFlag = false; // ジャンプ中のフラグをリセットする
        }
    }
    
}

