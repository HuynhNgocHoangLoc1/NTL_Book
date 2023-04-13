using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace NTL_Book.Models;

public class Store
{
    public string Id { get; set; } = null!;
    [ValidateNever]
    public ApplicationUser StoreOwner { get; set; } = null!;

    public string Name { get; set; } = null!;
}
