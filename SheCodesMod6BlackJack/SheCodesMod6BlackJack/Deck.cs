﻿using System;

namespace SheCodesMod6BlackJack
{
    /// <summary>
    /// Handles creation of a full deck of valid cards as well as the act of drawing a random card.
    /// </summary>
    class Deck
    {
        public Card[] Cards { get; set; }
        public Random RandomGenerator { get; set; }
        public int drawnCardsCounter { get; set; }

        // Constructor method which initializes the deck so all 52 cards are formatted to valid suits.
        public Deck()
        {
            this.drawnCardsCounter = 0;
            this.Cards = new Card[52];
            /*
            At this point all cards (objects) in the above this.Cards[] array are null. They have to be instantiated before their 
            properties can be set to a value.
            */
            int j = 0;
            foreach (var validSuit in Card.ValidSuits())
            {
                foreach (var validRank in Card.ValidRanks())
                {
                    this.Cards[j] = new Card(validSuit, validRank);
                    j++;
                }
            }

            RandomGenerator = new Random();

        }

        public Card DrawCard()
        {
            int randomNumber;
            do
                randomNumber = RandomGenerator.Next(0, 52);
            while ((this.Cards[randomNumber] == null) && (this.drawnCardsCounter < 53));

            Card drawnCard = this.Cards[randomNumber];
            this.Cards[randomNumber] = null;

            this.drawnCardsCounter++;

            return drawnCard;
        }
    }
}
