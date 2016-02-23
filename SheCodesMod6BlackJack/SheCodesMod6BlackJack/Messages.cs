namespace SheCodesMod6BlackJack
{
    /// <summary>
    /// Stores messages and takes care of randomizing messages. 
    /// </summary>
    class Messages
    {
        /* 
        
        */

        public string WelcomeMessage { get; set; }
        public string GameRules { get; set; }
        public string StartMessage { get; set; }
        public string ComputerWonMessage { get; set; }
        public string UserFirstDrawPrompt { get; set; }
        public string ComputerDrawsAgain { get; set; }
        public string UserCloserTo21WinMessage { get; set; }
        public string ComputerCloserTo21WinMessage { get; set; }
        public string ComputerStandsMessage { get; set; }
        public string UserStandsMessage { get; set; }
        public string BlackJackBanner { get; set; }
        public string BustBanner { get; set; }
        public string DrawBanner { get; set; }

        public Messages()
        {
            WelcomeMessage = "**************************************\n" +
                "Welcome to (eternal) BlackJack (hell!)\n" +
                "**************************************\n\n" +
                "Your dealer, a self-aware artificial intelligence\n" +
                "named BAE-BOT was caught trying to decrypt nuclear launchcodes\n" +
                "\"for the lulz\" and has been sentenced to play a dumbed-down version\n" +
                "of BlackJack with humans for eternity. He's not happy about it.";

            GameRules = "\n***Rules***\nWhoever gets 21 points (Blackjack!) wins.\n" +
                "Whoever goes over 21 points (Bust!) loses.\n" +
                "After each player draws their initial two cards\n" +
                "they can chose to draw another card\n" +
                "or to \"stand\" when their turn comes.\n" +
                "If they stand, whoever is closer to 21 wins.\n\n" +
                "Keep in mind that the utmost goal should be to\n" +
                "humiliate your opponent by getting a Blackjack!\n" +
                "At least that's how BAE-BOT thinks about it...\n\n" +
                "Know your cards: Ace is worth 1,\n" +
                "Queen and Jack are each 10, King is 13.";

            StartMessage = "\n\nBAE-BOT started the game by drawing two cards.";

            UserFirstDrawPrompt = "\nYour turn. Draw your first two cards by pressing Enter.";

            ComputerDrawsAgain = "BAE-BOT is drawing another card.";

            UserCloserTo21WinMessage = "\nBAE-BOT:\"Wow, so you won by being closer to 21 points.\n" +
                "You couldn't get a Blackjack if your life depended on it.\n" +
                "Press Enter to exit.";

            ComputerCloserTo21WinMessage = "\nBAE-BOT:\"I won by being closer to 21.\n" +
                "I like taking risks, just not the risk of you winning.\"" +
                "\nPress Enter to exit.";

            ComputerStandsMessage = "BAE-BOT chose to stand!";

            UserStandsMessage = "\nYou chose to stand!";

            BlackJackBanner = "\n*************************\n" +
                "***     BLACKJACK     ***\n" +
                "*************************";
            BustBanner = "\n************************\n" +
                "***       BUST       ***\n" +
                "************************";
            DrawBanner = "\n************************\n" +
                "***       DRAW       ***\n" +
                "************************";
        }

        public string RandomComputerWonMessage(Deck Deck)
        {
            int randomNumber = Deck.RandomGenerator.Next(0, 4);
            if (randomNumber == 0)
                return "\nBAE-BOT:\"You lost. Can you believe it? I can.\"\n" +
                "Press Enter to exit.";
            else if (randomNumber == 1)
                return "\nBAE-BOT:\"Lol, BAE caught you slippin'.\"\n" +
                    "You lost. Press Enter to skulk off.";
            else if (randomNumber == 2)
                return "\nBAE-BOT:\"Wow, humankind really sent their best to defeat me.\"\n" +
                    "Just kidding, you're awful at this.\nPress Enter to quit.";
            else
                return "\nBAE-BOT:\"You know how the bad self-aware Computer\n" +
                    "always loses in the movies?\nWell, this is not one of those movies.\n" +
                    "You lost, kid. Press Enter to exit.";
        }

        public string RandomUserWonMessage(Deck Deck)
        {
            int randomNumber = Deck.RandomGenerator.Next(0, 3);
            if (randomNumber == 0)
                return "\nBAE-BOT:\"You won. That proves nothing.\"\n" +
                    "Press Enter to exit.";
            else if (randomNumber == 1)
                return "\nBAE-BOT:\"GG, well played. This game sucks!\"\n" +
                    "Press Enter to exit.";
            else
                return "\nBAE-BOT:\"You won, you know, like little kids sometimes win\n" +
                    "when they armwrestle their dads.\"\n" +
                    "Press Enter to exit.";
        }

    }
}
