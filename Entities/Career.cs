using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kisko.Entities
{
    [Table("Career")]
    public class Career
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Division { get; set; }
        public string PdfName { get; set; }
    }
}
