using System.ComponentModel.DataAnnotations;

namespace apifmu.Dtos
{
    public class SearchDto
    {
        public string Type { get; set; }

        public string Breed { get; set; }

        public string Color { get; set; }

        [Required]
        public string Size { get; set; }

        [Required]
        public char Sex { get; set; }

        public string Personality { get; set; }

        public int UserId { get; set; }
    }
}
