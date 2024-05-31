using Financial.Core.Handlers;
using Financial.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Financial.App.Pages.Categories
{
	public partial class CreateCategoryPage : ComponentBase
	{
		public bool IsBusy { get; set; } = false;
		public CreateCategoryRequest Request { get; set; } = new();



		[Inject]
		public ICategoryHandler Handler { get; set; } = null!;
		[Inject]
		public NavigationManager NavigationManager { get; set; } = null!;
		[Inject]
		public ISnackbar Snackbar { get; set; } = null!;



		public async Task OnValidSubmitAsync()
		{
			IsBusy = true;
			try
			{
				var result = await Handler.CreateAsync(Request);
				if (result.IsSuccess)
				{
					Snackbar.Add(result.Message, Severity.Success);
					NavigationManager.NavigateTo("/categorias");
				}
				else
					Snackbar.Add(result.Message, Severity.Error);
			}
			catch (Exception ex)
			{
				Snackbar.Add(ex.Message, Severity.Error);
			}
			finally 
			{ 
				IsBusy = false; 
			}
		}

	}
}
