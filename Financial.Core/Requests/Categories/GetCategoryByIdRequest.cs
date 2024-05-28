using System.ComponentModel.DataAnnotations;

namespace Financial.Core.Requests.Categories
{
    public class GetCategoryByIdRequest : Request
    {
        [Required(ErrorMessage = "Id requerido")]
        public long Id { get; set; }
    }
}
