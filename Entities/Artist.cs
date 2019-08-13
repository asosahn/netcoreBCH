using System.ComponentModel.DataAnnotations;

namespace miprimerAPI.Entities
{
    public class Artist
    {
        public int Id { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        public string passport { get; set; }
        public string country { get; set; }

    }
}