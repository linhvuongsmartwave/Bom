//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;

//public class Info : MonoBehaviour
//{
//    private TextMeshProUGUI txtHeart;
//    private TextMeshProUGUI txtExplosion;
//    private TextMeshProUGUI txtSpeed;
//    private TextMeshProUGUI txtBom;

//    private Player player;

//    // Start is called before the first frame update
//    private void Awake()
//    {
//        txtHeart = GameObject.Find("txtHeart").GetComponent<TextMeshProUGUI>();
//        txtExplosion = GameObject.Find("txtExplosion").GetComponent<TextMeshProUGUI>();
//        txtSpeed = GameObject.Find("txtSpeed").GetComponent<TextMeshProUGUI>();
//        txtBom = GameObject.Find("txtBom").GetComponent<TextMeshProUGUI>();
//    }
//    void Start()
//    {
//        Debug.Log("info");


//        player  = FindObjectOfType<Player>();
//        if (player!=null)
//        {
//            print("tim thay player");
//            txtHeart.text = (player.currentHealth+1).ToString();
//            txtSpeed.text = player.speedMove.ToString();
//            txtExplosion.text=1.ToString();
//            txtBom.text=1.ToString();
//        }
//        else
//        {
//            print("khong thay player");
//            player = FindObjectOfType<Player>();
//            if (player != null)
//            {
//                print("lần 2 tim thay player");
//            }
//            else
//            {
//                print("lầm 2 tìm khong thay player");

//            }
//        }

//    }
//}
