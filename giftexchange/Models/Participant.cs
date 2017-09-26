using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace giftexchange.Models
{
    public class Participant
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public int GiftExchangeId { get; set; }
        [ForeignKey("GiftExchangeId")]
        [Required]
        [JsonIgnore]
        public GiftExchange GiftExchange { get; set; }

        public int? GiftAssignmentId { get; set; }
        [ForeignKey("GiftAssignmentId")]
        [JsonIgnore]
        public Participant GiftAssignment { get; set; }

        public void UpdateFromObject(Participant participant)
        {
            Name = participant.Name;
            Email = participant.Email;
        }
    }
}
