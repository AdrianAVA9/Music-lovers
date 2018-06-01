﻿using System.ComponentModel.DataAnnotations;

namespace LiveMusicLovers.Web.UI.Core.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}