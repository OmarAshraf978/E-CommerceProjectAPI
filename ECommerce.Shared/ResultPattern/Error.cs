using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.ResultPattern
{
    public class Error
    {
        public string Code { get; }
        public string Description { get; }
        public ErrorTypes Type { get; }
        private Error(string code, string description, ErrorTypes type)
        {
            Code = code;
            Description = description;
            Type = type;
        }
        public static Error Failure(string code = "General.Failure", string description = "General Failure Has Occurred")
        {
            return new Error(code, description, ErrorTypes.Failure);
        }
        public static Error Validation(string code = "General.Validation", string description = "Validation Error Has Occurred")
        {
            return new Error(code, description, ErrorTypes.Validation);
        }
        public static Error NotFound(string code = "General.NotFound", string description = "The Requested Resource Was Not Found")
        {
            return new Error(code, description, ErrorTypes.NotFound);
        }
        public static Error Unauthorized(string code = "General.Unauthorized", string description = "You Are Not Authorized")
        {
            return new Error(code, description, ErrorTypes.Unauthorized);
        }
        public static Error Forbidden(string code = "General.Forbidden", string description = "You Are Not Have Permission")
        {
            return new Error(code, description, ErrorTypes.Forbidden);
        }
        public static Error InvalidCredentials(string code = "General.InvalidCredentials", string description = "Invalid username or password")
        {
            return new Error(code, description, ErrorTypes.InvalidCrendentials);
        }
    }
}
