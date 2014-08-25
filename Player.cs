using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    partial class Player
    {
        
        public List<Cards> m_Hand = new List<Cards>();  
        private int pot = 100;
        private int bidHand = 0;

        public int bidProperty
        {
            get
            {
                return bidHand;
            }
            set
            {
                bidHand = value;
            }
        }
        public int potProperty
        {
            get
            {
                return pot;
            }
            set
            {
                pot = value;
            }
        }
        public void Deal1()
            //regions
            //bruce look and refractoring
        {
            Random rnd = new Random();
            Cards cardOne = new Cards();
            Cards cardTwo = new Cards();
            Cards cardThree = new Cards();
            m_Hand.Add(cardOne = (Cards)rnd.Next(1, 52));
            do
            {
                cardTwo = (Cards)rnd.Next(1, 52);
            } while (cardTwo == cardOne);
            m_Hand.Add(cardTwo);
            do
            {
                cardThree = (Cards)rnd.Next(1, 52);
            } while (cardThree == cardOne || cardThree == cardTwo);
            m_Hand.Add(cardThree);
        }
        public void Deal2(List<Cards> computerhand)
        {
            Random rnd = new Random();
            Cards cardOne = new Cards();
            Cards cardTwo = new Cards();
            Cards cardThree = new Cards();
            do
            {
                cardOne = (Cards)rnd.Next(1, 52);
            }
            while(computerhand.Contains(cardOne) == true);
            m_Hand.Add(cardOne);
            do
            {
                cardTwo = (Cards)rnd.Next(1, 52);
            } while (cardTwo == cardOne || computerhand.Contains(cardTwo) == true);
            m_Hand.Add(cardTwo);
            do
            {
                cardThree = (Cards)rnd.Next(1, 52);
            } while (cardThree == cardOne || cardThree == cardTwo || computerhand.Contains(cardThree) == true);
            m_Hand.Add(cardThree);
        }
        public void Bid()
        {
            Console.WriteLine("Enter how much more you want to bid: ");
            int bidAmount = 0;
            bool bidValid = false;
            while (bidValid == false)
            {
                try
                {
                    bidAmount = Convert.ToInt32(Console.ReadLine());
                    if (bidAmount < 0)
                        throw new Exception();
                    bidHand += bidAmount;
                    if (potProperty - bidAmount < 0)
                        Console.WriteLine("Your pot is now going to be negative");
                        potProperty -= bidAmount;
                    bidValid = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Reenter bid amount: ");
                }
            }
        }
        public void Display()
        {
            foreach(Cards item in m_Hand)
            {
                Console.WriteLine(item);
            }
        }

        public bool Hit(Cards cardOne, Cards cardTwo, Cards cardThree)
        {
            Random rnd = new Random();
            bool challenge = true, wentOver=false, inputCorrect=false;
            char decision = ' ';
            while ((int)cardOne + (int)cardTwo + (int)cardThree <= 21 || challenge == true)
            {
                Console.WriteLine("Enter Y if you want another card, or N to challenge: ");
                while (inputCorrect == false)
                {
                    try
                    {
                        decision = Convert.ToChar(Console.ReadLine());
                        if (decision != 'N' && decision != 'Y')
                            throw new Exception();
                        inputCorrect = true;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Enter 'Y' or 'N'");
                    }
                }
                inputCorrect = false;
                if (decision == 'N')
                {
                    challenge = false;
                }
                else
                {
                    bool newCardDetermined = false;
                    Cards nextCard = new Cards();
                    while (newCardDetermined == false)
                    {
                        nextCard = (Cards)rnd.Next(1, 52);
                        if (!inHand(nextCard))
                            newCardDetermined = true;
                        else
                            newCardDetermined = false;
                    }
                    m_Hand.Add(nextCard);
                    Display();
                    Bid();
                }
                if (sumHand() > 21)
                {
                    wentOver = true;
                    return wentOver;
                }          
            }
            return wentOver;
        }

        public int sumHand()
        {
            int sum = 0;
            foreach(Cards cards in m_Hand)
            {
                sum += cardValue(cards);
            }
            return sum;
        }
        
        public bool inHand(Cards cardDetermine)
        {
            for (int i = 0; i < m_Hand.Count; i++)
            {
                if (m_Hand[i] == cardDetermine)
                {
                    return true;
                }
            }
            return false;
        }
        public void determineWinner(List<Cards> human, List<Cards> computer)
        {
            int humanNum=0, computerNum=0;
            foreach(Cards cards in human)
            {
                humanNum += cardValue(cards);
            }  
            foreach(Cards cards in computer)
            {
                computerNum += cardValue(cards);
            }
            foreach(Cards card in human)
            {
                if(((int)card == 1 || (int)card == 14 || (int)card == 27 || (int)card== 40) && humanNum + 10 <= 21 )
                {
                    humanNum += 10;
                }
            }
            if(computerNum > 21)
            {
                Console.WriteLine("Computer player went over. You won!");
                return;
            }
            else if (humanNum > computerNum)
            {
                Console.WriteLine("You won! Your score is {0} to {1}", humanNum, computerNum);
                potProperty += bidProperty * 2;
                Console.WriteLine("Your pot amount is now {0}", potProperty);
                Console.ReadLine();
            }
            else if (humanNum == computerNum)
            {
                Console.WriteLine("Game is a tie. Your bet is returned");
                potProperty += bidProperty;
                Console.WriteLine("Your pot amount is now {0}", potProperty);
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("You lost! Your score is {0} to {1}", humanNum, computerNum);
                Console.WriteLine("Your pot amount is now {0}", potProperty);
                Console.ReadLine();
            }
        }
        public void deleteHand(ref List<Cards> Object)
        {
            int count = Object.Count;
            for (int i=count-1; i >= 0; i--)
            {
                Object.RemoveAt(i);
            }
            bidHand = 0;
        }
    }
}