using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
public class ClearPlayerAnimation : MonoBehaviour
{
    private Rigidbody rb; //物理演算をするコンポーネント
    [SerializeField] private Animator animator; //アニメーターの所得
    public float jumpPower; //ジャンプ力
    bool isJump = false; //空中フラグ
    List<string> currentAnimationName = new List<string>{"0","0","0"};
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // 現在再生中のアニメーションの名前を取得
        currentAnimationName[0] = GetCurrentAnimationNameJump();
        currentAnimationName[1] = GetCurrentAnimationNameRolling();
        currentAnimationName[2] = GetCurrentAnimationNameJumpFinish();

        if(currentAnimationName[0] == "JumpFull_Normal_InPlace_SwordAndShield") //ジャンプの出始め
        {
            if(isJump == false)
            {
                rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                isJump = true;
            }
            
        }
        
        if(currentAnimationName[1] == "JumpFull_Spin_InPlace_SwordAndShield") //空中でのローリング処理
        {
            //Debug.Log("ローリング");
        }
        if(currentAnimationName[2] == "JumpEnd_Normal_InPlace_SwordAndShield") //ジャンプの終わり目
        {
            //Debug.Log("Finish");
            isJump = false;
        }
        //Debug.Log(currentAnimationName[1]);
    }
    string GetCurrentAnimationNameJump()
    {
        if (animator != null)
        {
            // 現在のアクティブなレイヤーのステートを取得
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            
            // 検索したステートの名前を返す
            return stateInfo.IsName("Base Layer.JumpFull_Normal_InPlace_SwordAndShield") ? "JumpFull_Normal_InPlace_SwordAndShield" : stateInfo.fullPathHash.ToString(); //ジャンプ始め
            
        }
        return "Animatorコンポーネントが見つかりません";
    }
    string GetCurrentAnimationNameRolling()
    {
        if (animator != null)
        {
            // 現在のアクティブなレイヤーのステートを取得
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            
            // 検索したステートの名前を返す
            return stateInfo.IsName("Base Layer.JumpFull_Spin_InPlace_SwordAndShield") ? "JumpFull_Spin_InPlace_SwordAndShield" : stateInfo.fullPathHash.ToString(); //ローリング
            
        }
        return "Animatorコンポーネントが見つかりません";
    }
    string GetCurrentAnimationNameJumpFinish()
    {
        if (animator != null)
        {
            // 現在のアクティブなレイヤーのステートを取得
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            
            // 検索したステートの名前を返す
            return stateInfo.IsName("Base Layer.JumpEnd_Normal_InPlace_SwordAndShield") ? "JumpEnd_Normal_InPlace_SwordAndShield" : stateInfo.fullPathHash.ToString(); //ジャンプ終わり
            
        }
        return "Animatorコンポーネントが見つかりません";
    }

    void OnCollisionStay(Collision collision) //当たっている間
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isGround",true);
        }
    }
    void OnCollisionExit(Collision collision) //離れたときの処理
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isGround",false);
        }
    }
}
