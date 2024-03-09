using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankingApp.Core.ViewModels.User
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [StringLength(15, MinimumLength = 2)]
        public string FirstName { get; set; }

        [StringLength(15, MinimumLength = 2)]
        public string LastName { get; set; }

        [StringLength(25, MinimumLength = 6)]
        public string Email { get; set; }
    }
}
