namespace KundService1._2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Notis
    {
        public int Id { get; set; }

        public int KundId { get; set; }

        [StringLength(50)]
        public string NotisEmail { get; set; }

        [StringLength(50)]
        public string NotisTelefonNr { get; set; }
    }
}
