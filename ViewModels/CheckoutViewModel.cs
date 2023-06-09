﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using NTL_Book.Models;

namespace NTL_Book.ViewModels;

public class CheckoutViewModel
{
    [ValidateNever]
    public IEnumerable<Order> Orders { get; set; } = null!;

    [Display(Name = "Shipping Address")]
    [StringLength(255)]
    public string HomeAddress { get; set; } = null!;

    [Display(Name = "Full Name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Display(Name = "Phone Number")]
    [StringLength(20)]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; } = null!;
  
}