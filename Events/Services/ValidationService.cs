using Events.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Services
{
    public class ValidationService
    {
        public bool idValdation(int? id)
        {
            if (id == null)
                return false;
            return true;
        }

        public bool textValidation(string text)
        {
            if (text != null && text.Length != 0)
                return true;
            return false;
        }

        public bool objectValidation(Object obejct)
        {
            if (obejct == null)
                return false;
            return true;
        }

        public bool emailValidation(string email)
        {
            if (textValidation(email) && email.Contains('@'))
                return true;
            return false;
        }
    }
}
