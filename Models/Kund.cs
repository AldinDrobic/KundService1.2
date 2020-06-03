namespace KundService1._2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kund")]
    public partial class Kund
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InloggningsId { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Losenord { get; set; }

        [Required]
        [StringLength(50)]
        public string Fornamn { get; set; }

        [Required]
        [StringLength(50)]
        public string Efternamn { get; set; }

        [Required]
        [StringLength(50)]
        public string PersonNr { get; set; }

        [Required]
        [StringLength(50)]
        public string TelefonNr { get; set; }

        public int Bonuspoang { get; set; }
    }
}
