using System;

namespace SheCodesMod6BlackJack
{
    /// <summary>
    /// Handles computer and player moves ("decision making") and checking the scores.
    /// </summary>
    class Game
    {
        public bool UserWon { get; set; }
        public bool ComputerWon { get; set; }
        public int UserScore { get; set; }
        public int ComputerScore { get; set; }
        public Deck Deck { get; set; }

        public Game()
        {
            this.UserWon = false;
            this.ComputerWon = false;
            this.UserScore = 0;
            this.ComputerScore = 0;

            this.Deck = new Deck();
        }

        /* 
        I wonder if it would be helpful to make one player class for both players or alternatively a class for each player.
        Would it make sense to have "drawn" as a property of a card, like a "flag"?
        Also, should I make just one method called move rather than one for each player?
        Make as many methods universal as possible and leave the specifitiy in the messages?
        But the more I do that, the more I have to "pass around" parameters calling methods from classes all over 
        */
        public void PromptForRules(Messages gameMessages)
        {
            Console.WriteLine("\nPress R and Enter to display the Rules or Enter to continue");
            if (Console.ReadLine().ToUpperInvariant() == "R")
                Console.WriteLine(gameMessages.GameRules);
        }

        public void ComputerDrawsCard(Messages gameMessages)
        {
            Card drawnCard = Deck.DrawCard();
            this.ComputerScore += drawnCard.GetValue();
            Console.WriteLine("BAE-BOT drew: \"{0}\". His current score is: {1}.", drawnCard.GetFace(), this.ComputerScore);

            if (ComputerScore == 21)
            {
                Console.WriteLine(gameMessages.BlackJackBanner);
                this.ComputerWon = true;
                Console.WriteLine(gameMessages.RandomComputerWonMessage(Deck));
                QuitGame();
            }

            if (ComputerScore > 21)
            {
                Console.WriteLine(gameMessages.BustBanner);
                this.UserWon = true;
                Console.WriteLine(gameMessages.RandomUserWonMessage(Deck));
                QuitGame();
            }
        }

        public void UserDrawsCard(Messages gameMessages)
        {
            Card drawnCard = Deck.DrawCard();
            this.UserScore += drawnCard.GetValue();
            Console.WriteLine("You drew: \"{0}\". Your current score is: {1}.", drawnCard.GetFace(),
                this.UserScore);

            if (UserScore == 21)
            {
                Console.WriteLine(gameMessages.BlackJackBanner);
                this.UserWon = true;
                Console.WriteLine(gameMessages.RandomUserWonMessage(Deck));
                QuitGame();
            }

            if (UserScore > 21)
            {
                Console.WriteLine(gameMessages.BustBanner);
                this.ComputerWon = true;
                Console.WriteLine(gameMessages.RandomComputerWonMessage(Deck));
                QuitGame();
            }
        }

        public void UserDrawDecision(Messages gameMessages)
        {
            Console.WriteLine("\nMake Your Choice: Press Enter to draw a card \n(or press \"S\" and Enter to \"stand\".)");
            if (Console.ReadLine().ToUpperInvariant() == "S")
            {
                Console.WriteLine(gameMessages.UserStandsMessage);
                this.CheckIfUserIsCloserTo21(gameMessages);
            }
            else
                this.UserDrawsCard(gameMessages);
        }

        public void ComputerDrawDecision(Messages gameMessages)
        {
            Console.WriteLine("\nBAE BOT is making his move.");

            int randomNumber = Deck.RandomGenerator.Next(0, 2);

            if (UserScore == 20)
                this.ComputerDrawsCard(gameMessages);
            else if (ComputerScore < 18)
                this.ComputerDrawsCard(gameMessages);
            else if (ComputerScore < 18 && ComputerScore > 15 && randomNumber == 1)
                this.ComputerDrawsCard(gameMessages);
            else if (ComputerScore == UserScore)
                this.ComputerDrawsCard(gameMessages);
            else
                this.ComputerStands(gameMessages);
        }

        public void ComputerStands(Messages gameMessages)
        {
            Console.WriteLine(gameMessages.ComputerStandsMessage);
            this.CheckIfComputerIsCloserTo21(gameMessages);
        }

        public void CheckIfUserIsCloserTo21(Messages gameMessages)
        {
            if (UserScore > ComputerScore)
            {
                Console.WriteLine(gameMessages.UserCloserTo21WinMessage);
                QuitGame();
            }
        }

        public void CheckIfComputerIsCloserTo21(Messages gameMessages)
        {
            if (ComputerScore > UserScore)
            {
                Console.WriteLine(gameMessages.ComputerCloserTo21WinMessage);
                QuitGame();
            }
        }
        public void QuitGame()
        {
            Console.ReadLine();
            Environment.Exit(1);
        }
    }
}
