using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaySeven.Core
{
    public class Hand : IEquatable<Hand>, IComparable<Hand>
    {
        public List<Card> Cards { get; protected set; } = new List<Card>();
        
        public int Bid { get; }

        public HandType HandType { get; protected set; }

        protected Hand(int bid)
        {
            Bid = bid;
        }

        public Hand(string cards, int bid) : this(bid)
        {
            Cards.Add(ParseCard(cards[0]));
            Cards.Add(ParseCard(cards[1]));
            Cards.Add(ParseCard(cards[2]));
            Cards.Add(ParseCard(cards[3]));
            Cards.Add(ParseCard(cards[4]));

            // calculate the hand type
            HandType = GetHandType();
        }

        public Hand(Hand hand) : this(hand.Bid)
        {
            Cards = new(hand.Cards);
            
            // calculate the hand type
            HandType = GetHandType();
        }

        protected virtual Card ParseCard(char card)
        {
            return Enum.Parse<Card>(card.ToString());
        }

        public virtual HandType GetHandType()
        {
            // group the cards
            var groupedCards = Cards
                .GroupBy(card => card)
                .OrderByDescending(group => group.Count())
                .ThenByDescending(group => group.Key)
                .ToList();
            var groupCount = groupedCards.Count;

            if (groupCount == 1)
            {
                return HandType.FiveOfaKind;
            }
            else if (groupCount == 2 && groupedCards[0].Count() == 4)
            {
                return HandType.FourOfaKind;
            }
            else if (groupCount == 2 && groupedCards[0].Count() == 3) 
            {
                return HandType.FullHouse;
            }
            else if (groupCount == 3 && groupedCards[0].Count() == 3)
            {
                return HandType.ThreeOfaKind;
            }
            else if (groupCount == 3 && groupedCards[0].Count() == 2)
            {
                return HandType.TwoPair;
            }
            else if (groupCount == 4 && groupedCards[0].Count() == 2)
            {
                return HandType.OnePair;
            }
            else
            {
                return HandType.HighCard;
            }
        }

        public override string ToString() =>
         string.Join(string.Empty, Cards.Select(card => $"{card.ToString().Replace("_", string.Empty)}")) + $" [{Bid}]";

        #region IEquatable support
        public bool Equals(Hand? other)
        {
            if (other is null)
            {
                return false;
            }
            else if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            else if (GetType() != other.GetType())
            {
                return false;
            }
            else
            {
                return Cards.SequenceEqual(other.Cards) && Bid == other.Bid;
            }
        }

        public static bool operator ==(Hand? left, Hand? right)
        {
            if (left is null && right is null)
            {
                return true;
            }
            else if (left is not null && right is not null)
            {
                return left.Equals(right);
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(Hand? left, Hand? right) => !(left == right);

        public override bool Equals(object? obj) => Equals(obj as Hand);

        public override int GetHashCode() => (Cards.Sum(item => (int)item) + Bid).GetHashCode();
        #endregion

        #region IComparable support
        public int CompareTo(Hand? other)
        {
            if (other is null) 
            {
                return 1;
            }
            
            // get the comparisons
            var handTypeComparison = HandType.CompareTo(other.HandType);
            var card1Comparison = Cards[0].CompareTo(other.Cards[0]);
            var card2Comparison = Cards[1].CompareTo(other.Cards[1]);
            var card3Comparison = Cards[2].CompareTo(other.Cards[2]);
            var card4Comparison = Cards[3].CompareTo(other.Cards[3]);
            var card5Comparison = Cards[4].CompareTo(other.Cards[4]);

            return handTypeComparison != 0 ? handTypeComparison :
                card1Comparison != 0 ? card1Comparison :
                card2Comparison != 0 ? card2Comparison :
                card3Comparison != 0 ? card3Comparison :
                card4Comparison != 0 ? card4Comparison :
                card5Comparison != 0 ? card5Comparison : 0;
        }

        public static bool operator >(Hand? left, Hand? right)
        {
            return left?.CompareTo(right) > 0;
        }

        public static bool operator >=(Hand? left, Hand? right)
        {
            return left?.CompareTo(right) >= 0;
        }

        public static bool operator <(Hand? left, Hand? right)
        {
            return left?.CompareTo(right) < 0;
        }

        public static bool operator <=(Hand? left, Hand? right)
        {
            return left?.CompareTo(right) <= 0;
        }
        #endregion
    }
}