using System.ComponentModel.DataAnnotations;

namespace BddAPI.Models
{
    public class Record
    {
        [Key] public Guid Id { get; set; }

        public Guid ContractId { get; set; }
        public Contract Contract { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public string ConditionBefore { get; set; }

        public string ConditionAfter { get; set; }

        public string DamageDone { get; set; }

        public string Problems { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}