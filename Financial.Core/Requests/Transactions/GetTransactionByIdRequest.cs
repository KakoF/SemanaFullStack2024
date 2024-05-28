using System.ComponentModel.DataAnnotations;

namespace Financial.Core.Requests.Transactions
{
    public class GetTransactionByIdRequest : Request
    {
        [Required(ErrorMessage = "Id requerido")]
        public long Id { get; set; }
    }
}
