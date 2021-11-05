using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace PierreTreats.ViewModels
{
	public class LoginViewModel
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}
}