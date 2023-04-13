using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using NTL_Book.Models;

namespace AppDev.Models
{
    public class Store
    {
        public string Id { get; set; } = null!;
        [ValidateNever]
        public ApplicationUser StoreOwner { get; set; } = null!;

        public string Name { get; set; } = null!;
    }
}
