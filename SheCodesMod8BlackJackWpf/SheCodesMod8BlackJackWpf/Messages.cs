using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheCodesMod8BlackJackWpf
{
    /// <summary>
    /// Stores messages and takes care of randomizing messages. 
    /// I asked on Reddit whether having a class for messages was a good choice. Using an enum or dictionary were suggested as better alternatives.
    /// https://www.reddit.com/r/learnprogramming/comments/4776fr/c_classes_am_i_doing_it_right/
    /// However, since my messages are long (and I have two methods directly concerned with randomizing messages and dealing with text) I decided 
    /// to keep the class. I think it will make the design less cluttered. It also looks somewhat similar to what was suggested by Even Mien here:
    /// http://stackoverflow.com/questions/630803/associating-enums-with-strings-in-c-sharp
    /// </summary>
    public class Messages
    {
        public string WelcomeMessage { get; set; }
        public string GameRules { get; set; }
        public string ComputerWonMessage { get; set; } // Delete this in BlackJack, too.
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
                "**************************************\n" +
                "You are playing against a self-aware artificial intelligence " +
                "named BAE-BOT. BAE was caught trying to decrypt nuclear launchcodes " +
                "\"for the lulz\" and has been sentenced to play a dumbed-down version " +
                "of BlackJack with humans for eternity. He's not happy about it.\n" +
                "If this is your first time playing, please read the rules first.";

            GameRules = "***Rules***\nWhoever gets 21 points (Blackjack!) wins. -- " +
                "Whoever goes over 21 points (Bust!) loses.\n" +
                "The game begins with each player drawing two cards. " +
                "They can then chose to draw another card (\"Hit me!\") " +
                "or to \"stand\" when their turn comes.\n" +
                "If both players stand, the higher score wins. You can not draw a card again after chosing to stand.\n" +
                "Keep in mind that the utmost goal should be to " +
                "humiliate your opponent by getting a Blackjack!\n" +
                "Know your cards: Ace is worth 1, " +
                "Queen and Jack are each 10, King is 13. The other cards are numbered.";

            UserFirstDrawPrompt = "\nYour turn. Draw your first two cards by pressing Enter.";

            ComputerDrawsAgain = "BAE-BOT is drawing another card.";

            UserCloserTo21WinMessage = "\nBAE-BOT:\"Wow, so you won by being closer to 21 points.\n" +
                "You couldn't get a Blackjack if your life depended on it.\"";

            ComputerCloserTo21WinMessage = "\nBAE-BOT:\"I won by being closer to 21.\n" +
                "I like taking risks, just not the risk of you winning.\"";

            ComputerStandsMessage = "BAE-BOT chose to stand!";

            UserStandsMessage = "You chose to stand!";

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
                return "\nBAE-BOT:\"You lost. Can you believe it? I can.\"";
            else if (randomNumber == 1)
                return "\nBAE-BOT:\"Lol, BAE caught you slippin'.\"\n" +
                    "You lost.";
            else if (randomNumber == 2)
                return "\nBAE-BOT:\"Wow, humankind really sent their best to defeat me.\"\n" +
                    "Just kidding, you're awful at this.";
            else
                return "\nBAE-BOT:\"You know how the bad self-aware Computer\n" +
                    "always loses in the movies?\nWell, this is not one of those movies.\n" +
                    "You lost, kid.";
        }

        public string RandomUserWonMessage(Deck Deck)
        {
            int randomNumber = Deck.RandomGenerator.Next(0, 3);
            if (randomNumber == 0)
                return "\nBAE-BOT:\"You won. That proves nothing.\"";
            else if (randomNumber == 1)
                return "\nBAE-BOT:\"GG, well played. This game sucks!\"";
            else
                return "\nBAE-BOT:\"You won, you know, like little kids sometimes win\n" +
                    "when they armwrestle their dads.\"";
        }

    }
}
