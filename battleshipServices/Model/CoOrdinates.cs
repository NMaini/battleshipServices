using System.ComponentModel.DataAnnotations;

namespace battleshipServices.Model;
public class CoOrdinates
{
    [Required]
    [Range(0, 9)]
    public int X { get; set; }

    [Required]
    [Range(0, 9)]
    public int Y { get; set; }
}

