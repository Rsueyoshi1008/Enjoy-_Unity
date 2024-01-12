using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb; //物理演算をするコンポーネント
    [SerializeField] private Text textGameCount; //時間制限の表示
    [SerializeField] private Text textGameCountNow; //時間経過の表示
    [SerializeField] private Animator animator; //アニメーターの所得
    [SerializeField] private Camera playerCamera; //Playerを追従するカメラ
    float moveSpeed = 5.0f; //Playerの移動速度
    float jumpPower = 5.0f; //Playerのジャンプ力
    public float rotationSpeed; //キャラクタの回転速度
    bool jumpFlag = false; //ジャンプ中かどうかの追跡フラグ
    bool onGround = false; //地面に触れているかどうか
    bool cathFlag = false; //掴み中の追跡フラグ
    private bool isRodHit = false; //回転棒に触れているかの取得
    private bool isBigRodHit = false; //大きな回転棒に当たっているか取得
    float statePositionY; //開始時のPlayerのYの値
    public float forceMagnitude; //棒にあった時に外側に押し出す力
    float gameCount = 0.0f; //進めるカウント
    public float endCount; //終了カウント
    //スクリプトが有効になった瞬間に呼び出される
    void Awake()
    {
        statePositionY = transform.position.y;
        if(endCount == null)
        {
            Debug.Log("時間制限が設けられていません");
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
        }
    }
	void Start () {
		//animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        
        
	}

	void Update () {
        //フラグ・変数の初期化
        cathFlag = false;
		gameCount += Time.deltaTime;
        int count = (int)gameCount;
        textGameCount.text = count.ToString();
        textGameCountNow.text = "終了時間 " + endCount.ToString();
        if(gameCount > endCount)
        {
            ClearGame();
        }
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
        
        /** ゲーム終了処理 **/
        if(transform.position.y <= -2.0f)
        {
            SceneManager.LoadScene("GameOverScene");
        }
        if(Input.GetKeyDown(KeyCode.Escape))    //Escキーを押したときにゲームを終了する。
        {
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
        }
        
	}
    void LateUpdate() //Updateの後に実行される
    {
        //地面にめり込まないようにする為の処理
        Vector3 pos = transform.position;
        if(onGround == true)
        {
            if(transform.position.y <= 0.0f)
            {
                pos.y = statePositionY;
                transform.position = pos;
            }
        } 
    }

    void FixedUpdate()
    {
        if(Gamepad.current.leftStick.ReadValue() != new Vector2(0.0f,0.0f))
        {
            if(isBigRodHit == false) //巨大回転棒に当たっていない
            {
                
            }
            if(onGround == true)
            {
                Move();
            }
            
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

    void ClearGame()
    {
        SceneManager.LoadScene("ClearScene");
    }
    void OnCollisionEnter(Collision collision) //触れたときの処理
    {
        if (collision.gameObject.CompareTag("Ground")) // 地面に接触したら
        {
            jumpFlag = false; // ジャンプ中のフラグをリセットする
            onGround = true; //触れたらフラグセット
            animator.SetBool("isJump",jumpFlag);
        }
        if(collision.gameObject.CompareTag("Rod"))
        {
            isRodHit = true;
        }
        if(collision.gameObject.CompareTag("HitByBigRod"))
        {
            isBigRodHit = true;
        }
        
        
    }
    void OnCollisionExit(Collision collision) //離れたときの処理
    {
        if(collision.gameObject.CompareTag("Rod"))
        {
            //isRodHit = false;
        }
        if(collision.gameObject.CompareTag("Ground"))
        {
            onGround = false; //離れたときにリセット
        }
        if(collision.gameObject.CompareTag("HitByBigRod"))
        {
            isBigRodHit = false; //離れたときにリセット
        }
    }
    void OnCollisionStay(Collision collision) //当たっている間
    {
        if(collision.gameObject.CompareTag("HitByRod"))
        {
            

            //ステージの外側にAddForceする
            // 棒に当たったら、ステージの外に飛ばす
            Vector3 currentPosition = transform.position;
            
            // ステージ境界までの距離を加味して外に飛ばす方向を計算
            Vector3 forceDirection = (currentPosition - Vector3.zero).normalized;

            // 力を加えて吹っ飛ばす
            rb.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
        }
    }
}

