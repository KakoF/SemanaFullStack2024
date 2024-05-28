using System.ComponentModel.DataAnnotations;

namespace Financial.Core.Requests.Categories
{
    public class UpdateCategoryRequest : Request
    {
        [Required(ErrorMessage = "Id requerido")]
        public long Id { get; set; }

        [Required(ErrorMessage = "Título requerido")]
        [MaxLength(80, ErrorMessage = "O título deve conter até 80 caracteres")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Descrição requerida")]
        public string Description { get; set; } = string.Empty;
    }
}
