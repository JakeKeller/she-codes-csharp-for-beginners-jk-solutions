using System;

namespace SheCodesMod6BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            // I'm leaving in some class identifiers on purpose. It's easier for me to learn that way for now. 
            Messages gameMessages = new Messages();
            Console.WriteLine(gameMessages.WelcomeMessage);
            Game newGame = new Game();

            newGame.PromptForRules(gameMessages);

            Console.WriteLine(gameMessages.StartMessage);
            newGame.ComputerDrawsCard(gameMessages);
            newGame.ComputerDrawsCard(gameMessages);

            Console.WriteLine(gameMessages.UserFirstDrawPrompt);
            Console.ReadLine();
            newGame.UserDrawsCard(gameMessages);
            newGame.UserDrawsCard(gameMessages);

            while (!newGame.ComputerWon || !newGame.UserWon)
            {
                newGame.UserDrawDecision(gameMessages);
                newGame.ComputerDrawDecision(gameMessages);
            }
        }
    }
}

