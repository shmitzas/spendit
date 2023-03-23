using System.Globalization;
using System.Text.RegularExpressions;

namespace WebApp.Services
{
    public class InputValidationService
    {
        public async Task<bool> ValidateUsername(string input)
        {
            if (Regex.IsMatch(input, @"^[a-zA-Z0-9]+$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> ValidatePassword(string input)
        {
            if (Regex.IsMatch(input, @"^[a-zA-Z0-9!@#$%^&*()\-+=\[\]{};:'"",.<>/?\\|]+$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> CheckPasswordRequirements(string input)
        {
            if (input.Length < 8)
                return false;
            else
            {
                return true;
            }
        }
        public async Task<bool> ValidateEmail(string input)
        {
            if (Regex.IsMatch(input, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> ValidateText(string input)
        {
            if (Regex.IsMatch(input, @"^[\p{L}\p{N}\s\.,\-_()$€£¥₽?!]+$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> ValidateDecimal(string input)
        {
            if (Regex.IsMatch(input, @"^\d+(?:\.\d+)?$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> ValidateDate(string input)
        {
            if (Regex.IsMatch(input, @"^(0?[1-9]|1[0-2])\/(0?[1-9]|[12]\d|3[01])\/\d{4} (0?[1-9]|1[0-2]):[0-5]\d:[0-5]\d (AM|PM)$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}