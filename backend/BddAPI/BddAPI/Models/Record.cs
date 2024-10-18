using System.ComponentModel.DataAnnotations;

namespace BddAPI.Models
{
    public class Record
    {
        [Key] public Guid Id { get; set; }

        public Guid ContractId { get; set; }
        public ContractForSearch Contract { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        [Required] public string ConditionBefore { get; set; }

        [Required] public string ConditionAfter { get; set; }

        [Required] public string DamageDone { get; set; }

        [Required] public string Problems { get; set; }

        [Required] public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}