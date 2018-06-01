using System.ComponentModel.DataAnnotations;

namespace LiveMusicLovers.Web.UI.Core.ViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
