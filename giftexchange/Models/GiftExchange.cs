using System;
using System.Collections.Generic;
using System.Linq;

namespace giftexchange.Models
{
    public class GiftExchange
    {
        // The mininum number of participants you need to have an exchange
        private static int MIN_PARTICIPANTS = 2;

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float MaxPurchasePrice { get; set; }

        public List<Participant> Participants { get; set; }

        /// <summary>
        /// Resets the gift assignments for all Participants
        /// </summary>
        public void Reset()
        {
            foreach (var p in Participants)
                p.GiftAssignment = null;
        }
        
        /// <summary>
        /// Assigns Participants to each other randomly
        /// </summary>
        public void Shuffle()
        {
            var numParticipants = Participants.Count;
            if (Participants == null || numParticipants < GiftExchange.MIN_PARTICIPANTS)
                return;

            var assignments = Enumerable.Range(0, numParticipants).ToList();
            var keepShuffling = true;

            // TODO: This derangement algorithm is bad, but it works for now. Currently the chance for this to return a shuffle with one or more people
            // in the same position (aka not a derangement) is 1/e or ~37%. So it takes on average 2.7 shuffles before we find a derangement...
            while (keepShuffling)
            {
                assignments = assignments.OrderBy(a => Guid.NewGuid()).ToList();
                keepShuffling = false;
                for (var i = 0; i < numParticipants; i++)
                {
                    if (assignments[i] == i)
                    {
                        keepShuffling = true;
                        break;
                    }
                }
            }

            for (var i = 0; i < numParticipants; i++)
                Participants[i].GiftAssignment = Participants[assignments[i]];
        }
        
        public void UpdateFromObject(GiftExchange exchange)
        {
            Name = exchange.Name;
            Description = exchange.Description;
            MaxPurchasePrice = exchange.MaxPurchasePrice;
        }
    }
}
