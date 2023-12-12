using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaySeven.Core
{
    public class HandWithJokers : Hand
    {
        public HandWithJokers(string cards, int bid) : base(bid)
        {
            Cards.Add(ParseCard(cards[0]));
            Cards.Add(ParseCard(cards[1]));
            Cards.Add(ParseCard(cards[2]));
            Cards.Add(ParseCard(cards[3]));
            Cards.Add(ParseCard(cards[4]));

            // calculate the hand type
            HandType = GetHandType();
        }

        public HandWithJokers(HandWithJokers hand) : base(hand.Bid)
        {
            Cards = new(hand.Cards);

            // we don't need to set the hand type for these objects
            HandType = HandType.Indeterminate;
        }

        protected override Card ParseCard(char card)
        {
            // handle jokers
            return card == 'J' ? Card.Joker : base.ParseCard(card);
        }

        public override HandType GetHandType()
        {
            // get all possible hands
            var hands = GetAllPossibleHands(this);

            // sort the hands to determine the best possible hand
            var sortedHands = hands.OrderByDescending(hand => hand);
            return sortedHands.First().HandType;
        }

        private static List<Hand> GetAllPossibleHands(HandWithJokers hand)
        {
            // generate hands for each position
            var hands = new List<HandWithJokers>() { hand };

            for (int i = 0; i < 5; i++)
            {
                hands = GenerateHandPossibilties(hands, i);
            }

            // force a calculation of the hand type for each hand by casting them to Hand objects
            var allHands = hands.Select(hand => new Hand(hand)).ToList();

            // finally return all the new hands
            return allHands;
        }

        private static List<HandWithJokers> GenerateHandPossibilties(List<HandWithJokers> hands, int cardNumber)
        {
            var possibleCards = new List<Card>()
            {
                Card._2,
                Card._3,
                Card._4,
                Card._5,
                Card._6,
                Card._7,
                Card._8,
                Card._9,
                Card.T,
                Card.Q,
                Card.K,
                Card.A,
            };

            // for the specified card position, add all possibilities for jokers to the results
            var newHands = new List<HandWithJokers>();
            foreach (HandWithJokers hand in hands)
            {
                if (hand.Cards[cardNumber] == Card.Joker)
                {
                    // generate all replacement hands
                    for (int i = 0; i < possibleCards.Count; i++)
                    {
                        var newHand = new HandWithJokers(hand);
                        newHand.Cards[cardNumber] = possibleCards[i];
                        newHands.Add(newHand);
                    }
                }
                else
                {
                    newHands.Add(hand);
                }
            }
            
            return newHands;
        }
    }
}