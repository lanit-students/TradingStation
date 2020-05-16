using DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GUI.ViewModels
{
    public class BotCreationModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public List<BotRuleData> Rules { get; set; }
        public BotCreationModel() { }
    }
}
