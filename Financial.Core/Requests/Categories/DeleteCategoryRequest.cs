using System.ComponentModel.DataAnnotations;

namespace Financial.Core.Requests.Categories
{
    public class DeleteCategoryRequest : Request
    {
        [Required(ErrorMessage = "Id requerido")]
        public long Id { get; set; }
    }
}
