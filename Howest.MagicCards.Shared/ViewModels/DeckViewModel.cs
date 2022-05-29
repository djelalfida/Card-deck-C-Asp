using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.Shared.ViewModels;

public class DeckViewModel
{
    [Required(ErrorMessage = "Deck name is required")]
    [MinLength(3, ErrorMessage = "Deck name must be at least 3 characters long")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Deck cannot exist without cards")]
    public IEnumerable<Card> Cards { get; set; }
}
