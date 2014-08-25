using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    class BlackJack
    {
        static void Main(string[] args)
        {
            bool inputCorrect = false;
            char gameEnd = 'Y';
            Console.WriteLine("This is black jack.");
            Console.ReadLine();
            Player computerPlayer = new Player();
            Player humanPlayer = new Player();
            //how make more object oriented
            while(gameEnd == 'Y')
            {
                Console.WriteLine("New game. I will now deal you your cards.");
                computerPlayer.Deal1();
                humanPlayer.Deal2(computerPlayer.m_Hand);
                //black jack on two cards. first 21. dealer draw more cards
                Console.WriteLine("Your cards are: ");
                humanPlayer.Display();
                humanPlayer.Bid();
                if(humanPlayer.sumHand() > 21)
                {
                    Console.WriteLine("You went over! This hand is over");
                    Console.WriteLine("Your pot amount is now {0}", humanPlayer.potProperty);
                    Console.ReadLine();
                    humanPlayer.deleteHand(ref humanPlayer.m_Hand);
                    computerPlayer.deleteHand(ref computerPlayer.m_Hand);
                    Console.ReadLine();
                    continue; 
                }
                else if (humanPlayer.Hit(humanPlayer.m_Hand[0], humanPlayer.m_Hand[1], humanPlayer.m_Hand[2]) == true)
                {
                    Console.WriteLine("You went over! This hand is over");
                    Console.WriteLine("Your pot amount is now {0}", humanPlayer.potProperty);
                    Console.ReadLine();
                    humanPlayer.deleteHand(ref humanPlayer.m_Hand);
                    computerPlayer.deleteHand(ref computerPlayer.m_Hand);
                    continue;
                }
                humanPlayer.determineWinner(humanPlayer.m_Hand, computerPlayer.m_Hand);
                //make function
                Console.WriteLine("Do you want to play again? Hit Y for yes and N for no");
                while (inputCorrect == false)
                {
                    try
                    {
                        gameEnd = Convert.ToChar(Console.Read());
                        if (gameEnd != 'N' && gameEnd != 'Y')
                            throw new Exception();
                        inputCorrect = true;
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Enter 'Y' or 'N'");
                        Console.ReadLine();
                    }
                }
                inputCorrect = false;
                if(gameEnd == 'Y')
                {
                    humanPlayer.deleteHand(ref humanPlayer.m_Hand);
                    computerPlayer.deleteHand(ref computerPlayer.m_Hand);
                }
             } 
           }
        }
    }
   
