using System.ComponentModel.DataAnnotations;

namespace SubuProtokol.Models
{
    public class AlbumCreate
    {
        [Display(Name="Albüm adı")]
        [Required(ErrorMessage ="{0} boş geçilemez.")]
        [StringLength(100, ErrorMessage ="{0} en fazla {1} karakter olabilir.")]
        public string Name { get; set; }

        [Display(Name = "Albüm açıklama")]
        [StringLength(100, ErrorMessage = "{0} en fazla {1} karakter olabilir.")]
        public string Description { get; set; }
    }

    public class AlbumQuery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<SongQuery> Songs { get; set; }
    }

    public class AlbumCreateWithSong
    {
        public AlbumCreate Album { get; set; }
        public SongCreate Song { get; set; }
    }
}