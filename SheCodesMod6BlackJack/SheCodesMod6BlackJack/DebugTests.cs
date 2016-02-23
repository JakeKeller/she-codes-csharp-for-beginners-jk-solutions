using System;
using System.Text;

namespace SheCodesMod6BlackJack
{
    /// <summary>
    /// Repository for various debug tests.
    /// </summary>
    abstract class DebugTests
    {
        // Note: I'm making this class abstract so nobody accidentally creates a (useless) instance of it.

        // Some method calls that I can paste into the main if needed:
        // Card myFakeCard4 = new Card("Silly", "15"); // Invalid values. Should prompt error message.
        // Deck myTestDeck = new Deck();
        // DebugTests.DisplayFullDeck(myTestDeck);
        // DebugTests.DrawAllCards(myTestDeck);
        // DebugTests.CheckValues();

        public static void DisplayFullDeck(Deck deck)
        {
            // Writes all cards in the Deck.Cards array along with their index to the console.

            Console.WriteLine("Contents of your deck:");

            StringBuilder sb = new StringBuilder();
            foreach (var item in deck.Cards)
            {
                sb.Append("At Index : " + Array.IndexOf(deck.Cards, item) + " Face : " + item.GetFace() + "\n");
            }
            Console.WriteLine(sb);
        }

        public static void DrawAllCards(Deck deck)
        {
            Console.WriteLine("A list of all randomly drawn cards in your deck:");

            for (int i = 0; i < deck.Cards.Length; i++)
            {
                Console.WriteLine(deck.drawnCardsCounter + "    " + deck.DrawCard().GetFace());
            }
        }

        public static void CheckValues()
        {
            /* 
            Method allows to do a spot check if values are generated correctly.
            
            Sidenote: At first I passed in a deck to this method. However this would mess with that deck
            - setting cards in the decks card array to null by calling DrawCard() etc -  so I changed it and the method
            now creates its own deck for testing purposes.
            */
            Deck tempDeck = new Deck();

            Console.WriteLine("A list of four randomly drawn cards and their values.");

            Card randomCard0 = tempDeck.DrawCard();
            Card randomCard1 = tempDeck.DrawCard();
            Card randomCard2 = tempDeck.DrawCard();
            Card randomCard3 = tempDeck.DrawCard();

            Console.WriteLine("{0} has a value of: {1}", randomCard0.GetFace(), randomCard0.GetValue());
            Console.WriteLine("{0} has a value of: {1}", randomCard1.GetFace(), randomCard1.GetValue());
            Console.WriteLine("{0} has a value of: {1}", randomCard2.GetFace(), randomCard2.GetValue());
            Console.WriteLine("{0} has a value of: {1}", randomCard3.GetFace(), randomCard3.GetValue());

            tempDeck = null;

        }

    }
}
