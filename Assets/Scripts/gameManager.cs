using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class gameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int botScore = 0;
    public int playerScore = 0;

    [SerializeField] Text playerScoreText;
    [SerializeField] Text botScoreText;

    public Text Instruction;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerScore!=10)
            playerScoreText.text = "Player : "+ playerScore;
        else
            playerScoreText.text = "Match Point!";
        
        if(botScore!=10)
            botScoreText.text = "Bot : "+ botScore;
        else
            botScoreText.text = "Match Point!";
        
        if(GameObject.Find("player").GetComponent<Player>().serving){
            Instruction.text = "Press R to serve\n Use Movement Keys to change\n your hit direction";
        }
        else{
            Instruction.text = "Hold F and use Movement Keys to\n change your hit direction";
        }

        if(botScore == 11 ){
			SceneManager.LoadScene("GameOverLostScene");
        }

        if(playerScore == 11 ){
			SceneManager.LoadScene("GameOverWonScene");
        }
    }
}
