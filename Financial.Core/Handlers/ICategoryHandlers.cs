using Financial.Core.Models;
using Financial.Core.Requests.Categories;
using Financial.Core.Responses;

namespace Financial.Core.Handlers
{
    public interface ICategoryHandlers
    {
        Task<Response<Category>> CreateAsync(CreateCategoryRequest request);
        Task<Response<Category>> UpdateAsync(UpdateCategoryRequest request);
        Task<Response<Category>> DeleteAsync(DeleteCategoryRequest request);
        Task<Response<Category>> GetByIdAsync(GetCategoryByIdRequest request);
        Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoryRequest request);
    }
}
