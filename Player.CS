using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   [SerializeField] private SpriteRenderer idle, bate;

   [SerializeField] private Sprite [] sprite;
   public static int playerIndex;
   void Start() 
   {
      if(PlayerPrefs.HasKey("PlayerIndex")) playerIndex = PlayerPrefs.GetInt("PlayerIndex");
      else playerIndex = 1;
   
      CalculateSprite();
   }
   private void CalculateSprite()
   {
      switch(playerIndex) 
      {
         case 1:
         idle.sprite = sprite[0];
         bate.sprite = sprite[1];
            break;
         
         case 2:
         idle.sprite = sprite[2];
         bate.sprite = sprite[3];
            break;

         case 3:
         idle.sprite = sprite[4];
         bate.sprite = sprite[5];
            break;

         case 4:
         idle.sprite = sprite[6];
         bate.sprite = sprite[7];
            break;

         case 5:
         idle.sprite = sprite[8];
         bate.sprite = sprite[9];
            break;

         case 6:
         idle.sprite = sprite[10];
         bate.sprite = sprite[11];
            break;

         case 7:
         idle.sprite = sprite[12];
         bate.sprite = sprite[13];
            break;

         case 8:
         idle.sprite = sprite[14];
         bate.sprite = sprite[15];
            break;
      }
   }
}
