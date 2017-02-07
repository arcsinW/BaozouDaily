using BaoZouRiBao.Core.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Core.Model
{
    /// <summary>
    /// baozou login input
    /// </summary>
    public class BaozouLoginInput : VerifiableBase
    {
        private string account;
        [Required]
        public string Account
        {
            get
            {
                return account;
            }
            set
            {
                Set(ref account, value);
            }
        }

        private string password;
        [Required]
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                Set(ref password, value);
            }
        }
    }
}
