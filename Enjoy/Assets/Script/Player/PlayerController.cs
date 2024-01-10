using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb; //物理演算をするコンポーネント
    [SerializeField] private Animator animator; //アニメーターの所得
    [SerializeField] private Camera playerCamera; //Playerを追従するカメラ
    float moveSpeed = 5.0f; //Playerの移動速度
    float jumpPower = 5.0f; //Playerのジャンプ力
    public float rotationSpeed; //キャラクタの回転速度
    bool jumpFlag = false; //ジャンプ中かどうかの追跡フラグ
    bool cathFlag = false; //掴み中の追跡フラグ
    private bool isRodHit = false; //回転棒に触れているかの取得
    //スクリプトが有効になった瞬間に呼び出される
    void Awake()
    {
        
    }
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
            //左スティックの入力を見る
            if(Gamepad.current.leftStick.ReadValue() != new Vector2(0.0f,0.0f))
            {
                animator.SetFloat("Movement",1);
                
            }
            else
            {
                animator.SetFloat("Movement",-1);
            }
            if(jumpFlag == false)
            {
                if(Gamepad.current.buttonSouth.wasPressedThisFrame) //Xボタンの条件分岐
                {
                    rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                    jumpFlag = true; // ジャンプ中のフラグを立てる
                    animator.SetBool("isJump",jumpFlag);
                }
            }
            
            if(Gamepad.current.leftShoulder.isPressed) //Lボタンの条件分岐
            {
                cathFlag = true;
                
            }
            //Debug.Log(Gamepad.current.leftStick.ReadValue());
        }
        else
        {
            Debug.Log("コントローラーが接続されていません！");
            #if UNITY_EDITOR //UnityEditorでプレイしているとき
                UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了

            #else  //ビルドしたゲームをプレイしいているとき
                Application.Quit();//ゲームプレイ終了

                #endif
        }
        
        if(Input.GetKeyDown(KeyCode.A))
        {
            rb.AddForce(1000f,0f,1000f);
        }
        if(Input.GetKeyDown(KeyCode.Escape))    //Escキーを押したときにゲームを終了する。
        {
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
        }
        //Debug.Log(rb.velocity);
        // 入力方向に回転する
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(velocity), rotationSpeed);
	}
    void FixedUpdate()
    {
        if(Gamepad.current.leftStick.ReadValue() != new Vector2(0.0f,0.0f))
        {
            if(isRodHit == false) //回転棒に当たったら移動を無効
            {
                
            }
            Move();
        }
        
        
    }

    public void Move()
    {
        var input = Gamepad.current.leftStick.ReadValue();
                
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraRight = playerCamera.transform.right;

        // Y軸成分を無視する
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        //方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 velocity = cameraForward.normalized * input.y + cameraRight.normalized * input.x;
        
        //  走った場合と歩いた場合
        //float speed = runFlag ? _model.RunSpeed : _model.WalkSpeed

        rb.velocity = velocity * moveSpeed + new Vector3(0, rb.velocity.y, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(velocity), rotationSpeed * Time.deltaTime);
    }
    void OnCollisionEnter(Collision collision) //触れたときの処理
    {
        if (collision.gameObject.CompareTag("Ground")) // 地面に接触したら
        {
            jumpFlag = false; // ジャンプ中のフラグをリセットする
            animator.SetBool("isJump",jumpFlag);
        }
        if(collision.gameObject.CompareTag("Rod"))
        {
            isRodHit = true;
        }
    }
    void OnCollisionExit(Collision collision) //離れたときの処理
    {
        if(collision.gameObject.CompareTag("Rod"))
        {
            //isRodHit = false;
        }
    }
    
}

