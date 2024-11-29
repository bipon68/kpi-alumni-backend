using System.Text.RegularExpressions;

namespace KpiAlumni.Utils.Validation;

public class Validation
{
   public static StatusObject IsValidDateFormat(string? date, string title = "Date")
        {
            if (string.IsNullOrEmpty(date) || date.Length == 0)
            {
                return new StatusObject { Error = 1, Message = "Please enter " + title };
            }

            // Define a regular expression pattern to match the YYYY-MM-DD format
            string pattern = @"^\d{4}-\d{2}-\d{2}$";
            var st = Regex.IsMatch(date, pattern);
            if (!st)
            {
                return new StatusObject { Error = 1, Message = "Please enter valid " + title };
            }

            return new StatusObject { Error = 0, Message = "Success" };
        }

        public static StatusObject IsValidEmailFormat(string? email, string title, string postText = "")
        {
            if (email == null || email.Trim().Length == 0)
            {
                return new StatusObject { Error = 1, Message = $"Please enter {title}. {postText}" };
            }

            if (email.Length < 6)
            {
                return new StatusObject { Error = 1, Message = $"{title} should be minimum 3 characters long. {postText}" };
            }

            if (email.Length > 200)
            {
                return new StatusObject { Error = 1, Message = $" {title} should be maximum 200 characters long. {postText}" };
            }

            // Define a regular expression pattern to match the email format
            string pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            var st = Regex.IsMatch(email, pattern);
            if (!st)
            {
                return new StatusObject { Error = 1, Message = $"Please enter valid {title}. {postText}" };
            }

            return new StatusObject { Error = 0, Message = "Success" };
        }

        public static StatusObject IsValidEmailAll(List<string> email, string title)
        {
            var validateAll = email.Select(x => IsValidEmailFormat(x, title, $"({x})")).ToList();

            return ValidateAll(validateAll);
        }

        public static StatusObject IsValidPhoneNumberFormat(string? pNumber, string title = "Phone Number", bool isOptional = false)
        {
            if (pNumber == null || pNumber.Trim().Length == 0)
            {
                if (isOptional)
                {
                    // If the phone number is optional, return success
                    return new StatusObject { Error = 0, Message = "Optional" };
                }

                return new StatusObject { Error = 1, Message = "Please enter " + title };
            }

            return new StatusObject { Error = 0, Message = "Success" };
        }

        public static StatusObject IsValidPasswordFormat(string? password, string title = "Password")
        {
            if (password == null || password.Trim().Length == 0)
            {
                return new StatusObject { Error = 1, Message = "Please enter " + title };
            }

            if (password.Length < 8)
            {
                return new StatusObject { Error = 1, Message = title + " should be minimum 8 characters long" };
            }

            if (password.Length > 32)
            {
                return new StatusObject { Error = 1, Message = title + " should be maximum 32 characters long" };
            }

            //--Define a regular expression pattern to match the password format
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,32}$";
            var st = Regex.IsMatch(password, pattern);
            if (!st)
            {
                return new StatusObject { Error = 1, Message = title + " should contain at least one uppercase letter, one lowercase letter, one digit and one special character" };
            }

            return new StatusObject { Error = 0, Message = "Success" };
        }

        public static StatusObject IsValidString(string? name, string title, int min = 2, int max = 100)
        {
            if (name == null || name.Trim().Length == 0)
            {
                return new StatusObject { Error = 1, Message = "Please enter " + title };
            }

            if (name.Length < min)
            {
                return new StatusObject { Error = 1, Message = title + " should be minimum " + min + " character long" };
            }

            if (name.Length > max)
            {
                return new StatusObject { Error = 1, Message = title + " should be maximum " + max + " character long" };
            }

            //--Define a regular expression pattern to match the name format win a min and max length
            string pattern = @"^[a-zA-Z0-9\s\.\- ,\(\)]+$";
            var st = Regex.IsMatch(name, pattern);
            if (!st)
            {
                return new StatusObject { Error = 1, Message = title + " should be alphanumeric" };
            }

            return new StatusObject { Error = 0, Message = "Success" };
        }

        public static StatusObject IsValidLongString(string? name, string title, int min = 2, int max = 5000, bool isOptional = false)
        {
            if (isOptional && (name == null || name.Trim().Length == 0))
            {
                return new StatusObject { Error = 0, Message = "Success" };
            }

            if (name == null || name.Trim().Length == 0)
            {
                return new StatusObject { Error = 1, Message = "Please enter " + title };
            }

            if (name.Length < min)
            {
                return new StatusObject { Error = 1, Message = title + " should be minimum " + min + " character long" };
            }

            if (name.Length > max)
            {
                return new StatusObject { Error = 1, Message = title + " should be maximum " + max + " character long" };
            }

            return new StatusObject { Error = 0, Message = "Success" };
        }

        public static StatusObject IsValidateOptions(string? option, string title, List<string>? validOptions = null, string postText = "", bool isOptional = false)
        {
            if (option == null || option.Length == 0)
            {
                if (isOptional)
                {
                    return new StatusObject { Error = 0, Message = "Optional" };
                }

                return new StatusObject { Error = 1, Message = $"Please select {title}. {postText}" };
            }

            if (validOptions != null && !validOptions.Contains(option))
            {
                return new StatusObject { Error = 1, Message = $"{title} is not valid. {postText}" };
            }

            return new StatusObject { Error = 0, Message = "Success" };
        }

        public static StatusObject IsValidateOptionsAll(List<string> options, List<string> validOptions, string title)
        {
            var validateAll = options.Select(x => IsValidateOptions(x, title, validOptions, $"({x})")).ToList();

            return ValidateAll(validateAll);
        }

        public static StatusObject IsValidDomainFormat(string domainName, string title, bool isOptional = false)
        {
            if (isOptional && (domainName == null || domainName.Length == 0))
            {
                return new StatusObject { Error = 0, Message = "Success" };
            }

            //--VerifyAsync Valid Domain Format
            if (domainName == null || domainName.Trim().Length == 0)
            {
                return new StatusObject { Error = 1, Message = "Please enter " + title };
            }

            //--Define a regular expression pattern to match the domain format
            string pattern = @"^([a-zA-Z0-9][a-zA-Z0-9\-]{0,61}[a-zA-Z0-9]\.)+[a-zA-Z]{2,}$";
            var st = Regex.IsMatch(domainName, pattern);
            if (!st)
            {
                return new StatusObject { Error = 1, Message = title + " is not valid" };
            }

            return new StatusObject { Error = 0, Message = title + " is Valid" };
        }

        public static StatusObject IsEqual(string? val1, string? val2, string failedMessage)
        {
            if (!string.IsNullOrEmpty(val1) && val1 != val2)
            {
                return new StatusObject { Error = 1, Message = failedMessage };
            }

            return new StatusObject { Error = 0, Message = "Success" };
        }

        public static StatusObject IsTrue(bool val, string failedMessage)
        {
            if (!val)
            {
                return new StatusObject { Error = 1, Message = failedMessage };
            }

            return new StatusObject { Error = 0, Message = "Success" };
        }

        public static StatusObject IsFalse(string? val1, string? val2, string successMessage)
        {
            if (!string.IsNullOrEmpty(val1) && val1 == val2)
            {
                return new StatusObject { Error = 1, Message = successMessage };
            }

            return new StatusObject { Error = 0, Message = "Success" };
        }

        public static StatusObject IsValidUrlFormat(string? logoUrl, string title)
        {
            // Check if the URL is null or empty
            if (string.IsNullOrWhiteSpace(logoUrl))
            {
                return new StatusObject { Error = 1, Message = "Please enter " + title };
            }

            // Updated pattern to allow localhost with ports and optional paths
            //             = @"^((http|https):\/\/)?([\w-]+\.)+[\w-]+(\/[\w- .\/?%&=]*)?$";
            string pattern = @"^(http|https):\/\/(localhost(:\d+)?|([\w-]+\.)+[\w-]+)(\/[\w- .\/?%&=]*)?$";
            bool isValid = Regex.IsMatch(logoUrl, pattern, RegexOptions.IgnoreCase);

            if (!isValid)
            {
                return new StatusObject { Error = 1, Message = title + " is not valid" };
            }

            return new StatusObject { Error = 0, Message = "Success" };
        }

        public static StatusObject IsValidUserNameFormat(string userName, string title)
        {
            //--Check is Username Format is valid
            if (userName == null || userName.Trim().Length == 0)
            {
                return new StatusObject { Error = 1, Message = "Please enter " + title };
            }

            if (userName.Length < 5)
            {
                return new StatusObject { Error = 1, Message = title + " should be minimum 5 characters long" };
            }

            if (userName.Length > 20)
            {
                return new StatusObject { Error = 1, Message = title + " should be maximum 20 characters long" };
            }

            //--Define a regular expression pattern to match the username format
            string pattern = @"^[a-zA-Z0-9\.\-\@]+$";
            var st = Regex.IsMatch(userName, pattern);
            if (!st)
            {
                return new StatusObject { Error = 1, Message = title + " should be alphanumeric or Dot(.), Hyphen(-), At the rate(@)" };
            }

            //--Check if username not starts with a letter
            if (!char.IsLetter(userName[0]))
            {
                return new StatusObject { Error = 1, Message = title + " should start with a letter" };
            }

            //--Check if username not ends with a letter or number
            if (!char.IsLetterOrDigit(userName[^1]))
            {
                return new StatusObject { Error = 1, Message = title + " should end with a letter or number" };
            }

            return new StatusObject { Error = 0, Message = "Success" };
        }

        public static StatusObject IsValidateInt(int number, string title, int? min = null, int? max = null)
        {
            if (double.IsNaN(number))
            {
                return new StatusObject { Error = 1, Message = "Please enter " + title };
            }

            if (min != null && number < min)
            {
                return new StatusObject { Error = 1, Message = title + " should be minimum " + min };
            }

            if (max != null && number > max)
            {
                return new StatusObject { Error = 1, Message = title + " should be maximum " + max };
            }

            return new StatusObject { Error = 0, Message = "Success" };
        }

        public static StatusObject IsValidateNumber(double number, string title, double? min = null, double? max = null)
        {
            if (double.IsNaN(number))
            {
                return new StatusObject { Error = 1, Message = "Please enter " + title };
            }

            if (min != null && number < min)
            {
                return new StatusObject { Error = 1, Message = title + " should be minimum " + min };
            }

            if (max != null && number > max)
            {
                return new StatusObject { Error = 1, Message = title + " should be maximum " + max };
            }

            return new StatusObject { Error = 0, Message = "Success" };
        }

        public static StatusObject ValidateAll(List<StatusObject> parameters)
        {
            var x = 0;
            foreach (var parameter in parameters)
            {
                if (parameter.Error == 1)
                {
                    return new StatusObject { Error = parameter.Error, Message = parameter.Message, ErrorIndex = x };
                }
                x++;
            }

            return new StatusObject { Error = 0, Message = "Success", ErrorIndex = -1 };
        }

        public static string GetReferenceName(List<string> refNames, int errorIndex)
        {
            if (errorIndex == -1)
            {
                return "";
            }

            if (errorIndex >= refNames.Count)
            {
                return "";
            }

            return refNames[errorIndex];
        }

        public static StatusObject IsValidImageFile(IFormFile? attachment, string title)
        {
            if (attachment == null)
            {
                return new StatusObject { Error = 1, Message = "Please select " + title };
            }

            if (attachment.Length == 0)
            {
                return new StatusObject { Error = 1, Message = "Please select " + title };
            }

            if (attachment.Length > 1000000)
            {
                return new StatusObject { Error = 1, Message = title + " should be maximum 1 MB" };
            }

            if (attachment.ContentType != "image/jpeg" && attachment.ContentType != "image/png" && attachment.ContentType != "image/svg+xml")
            {
                return new StatusObject { Error = 1, Message = title + " should be in jpeg, png or svg format" };
            }

            return new StatusObject { Error = 0, Message = "Success" };
        }

        public static StatusObject IsValidDocFormat(IFormFile? attachment, string title)
        {
            if (attachment == null)
            {
                return new StatusObject { Error = 1, Message = "Please select " + title };
            }

            if (attachment.Length == 0)
            {
                return new StatusObject { Error = 1, Message = "Please select " + title };
            }

            if (attachment.Length > 1000000)
            {
                return new StatusObject { Error = 1, Message = title + " should be maximum 1 MB" };
            }

            // image/*, .pdf, .doc, .docx
            if (attachment.ContentType != "application/pdf" && attachment.ContentType != "application/msword" && attachment.ContentType != "application/vnd.openxmlformats-officedocument.wordprocessingml.document" && attachment.ContentType != "image/jpeg" && attachment.ContentType != "image/png")
            {
                return new StatusObject { Error = 1, Message = title + " should be in pdf, doc, docx, jpeg or png format" };
            }

            return new StatusObject { Error = 0, Message = "Success" };
        }


        public static StatusObject IsValidDocAndMediaFormat(IFormFile? attachment, string title)
        {
            if (attachment == null)
            {
                return new StatusObject { Error = 1, Message = "Please select " + title };
            }

            if (attachment.Length == 0)
            {
                return new StatusObject { Error = 1, Message = "Please select " + title };
            }

            if (attachment.Length > 100000000) // 100000000 to Mb = 100
            {
                return new StatusObject { Error = 1, Message = title + " should be maximum 100 MB" };
            }

            // image/*, .pdf, .doc, .docx
            if (
                attachment.ContentType != "application/pdf"
                && attachment.ContentType != "application/msword"
                && attachment.ContentType != "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                && attachment.ContentType != "image/jpeg"
                && attachment.ContentType != "image/png"
                && attachment.ContentType != "video/mp4"
                && attachment.ContentType != "video/x-msvideo"
                && attachment.ContentType != "video/x-ms-wmv"
                && attachment.ContentType != "video/quicktime"
                // && attachment.ContentType != "video/x-flv"
                && attachment.ContentType != "video/3gpp"
                && attachment.ContentType != "video/3gpp2"
                && attachment.ContentType != "video/ogg"
                && attachment.ContentType != "video/webm"
                && attachment.ContentType != "audio/mpeg"
                && attachment.ContentType != "audio/mp4"
                && attachment.ContentType != "audio/ogg"
                && attachment.ContentType != "audio/wav"
                && attachment.ContentType != "audio/webm"
            )
            {
                return new StatusObject { Error = 1, Message = title + " should be in pdf, doc, docx, jpeg or png format" };
            }

            return new StatusObject { Error = 0, Message = "Success" };
        }

        public static StatusObject IsValidXlsxFormat(IFormFile? xlsxFile, string title)
        {
            if (xlsxFile == null)
            {
                return new StatusObject { Error = 1, Message = "Please select " + title };
            }

            if (xlsxFile.Length == 0)
            {
                return new StatusObject { Error = 1, Message = "Please select " + title };
            }

            if (xlsxFile.Length > 1000000)
            {
                return new StatusObject { Error = 1, Message = title + " should be maximum 1 MB" };
            }

            if (xlsxFile.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                return new StatusObject { Error = 1, Message = title + " should be in xlsx format" };
            }

            return new StatusObject { Error = 0, Message = "Success" };
        }
}