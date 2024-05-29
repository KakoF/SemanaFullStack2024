using Financial.Api.Data;
using Financial.Core.Handlers;
using Financial.Core.Models;
using Financial.Core.Requests.Categories;
using Financial.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Financial.Api.Handlers
{
    public class CategoryHandler : ICategoryHandler
    {
        private readonly AppDataContext _context;
        public CategoryHandler(AppDataContext context)
        {
            _context = context;
        }
        public async Task<Response<Category>> CreateAsync(CreateCategoryRequest request)
        {
            var category = new Category(request.Title, request.Description, request.UserId);
            try
            {
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                return new Response<Category>(category, 201, "Categoria criada com sucesso");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Response<Category>(null, 400, "Não foi possível criar categoria");
            }
            
        }

        public async Task<Response<Category>> DeleteAsync(DeleteCategoryRequest request)
        {
            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
                if (category == null)
                    return new Response<Category>(null, 404, "Categoria não encontrada");

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return new Response<Category>(category, message: "Categoria excluída com sucesso");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Response<Category>(null, 400, "Não foi possível excluir categoria");
            }
        }

        public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoryRequest request)
        {
            //Queryu que não foi executada ainda

            try
            {
                var query = _context.Categories.AsNoTracking().Where(c => c.UserId == request.UserId).OrderBy(c => c.Title);
            
            var categories = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();
            return new PagedResponse<List<Category>>(categories, count, request.PageNumber, request.PageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new PagedResponse<List<Category>>(null, 400, "Não foi possível consultar as categorias");
            }
        }

        public async Task<Response<Category>> GetByIdAsync(GetCategoryByIdRequest request)
        {
            var category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);
            if(category == null)
                return new Response<Category>(null, 404, "Categoria não encontrada");
            return new Response<Category>(category);
        }

        public async Task<Response<Category>> UpdateAsync(UpdateCategoryRequest request)
        {
            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
                if (category == null)
                    return new Response<Category>(null, 404, "Categoria não encontrada");

                category.Update(request.Title, request.Description);

                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
                return new Response<Category>(category, message: "Categoria atualizada com sucesso");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Response<Category>(null, 400, "Não foi possível atualizar categoria");
            }
        }
    }
}
