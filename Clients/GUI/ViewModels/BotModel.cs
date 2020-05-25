using DTO;
using GUI.CustomValidationAttributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GUI.ViewModels
{
    public class BotModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(32, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
        public List<BotRuleData> Rules { get; set; }
        public BotModel() { }
    }
}
