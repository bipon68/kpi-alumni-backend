using System.Runtime.CompilerServices;
using KpiAlumni.Dtos.Account;
using KpiAlumni.Tables;

namespace KpiAlumni.Mappers
{
    public static class AccountMapper
    {
        public static SignUpDto ToSignUpDto(this User userModel)
        {
            return new SignUpDto
            {
                Id = userModel.Id,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                Phone = userModel.Phone,
                Password = userModel.Password,
                Department = userModel.Department,
                PassingYear = userModel.PassingYear,
                Session = userModel.Session,
                CurrentPosition = userModel.CurrentPosition,
                AcceptTermsAndConditions = userModel.AcceptTermsAndConditions,
                DateOfBirth = userModel.DateOfBirth

            };
        }

        public static User FromCreateSignUpDto(this CreateSignUpDto createSignUpDto)
        {
            return new User
            {
                FirstName = createSignUpDto.FirstName,
                LastName = createSignUpDto.LastName,
                Email = createSignUpDto.Email,
                Phone = createSignUpDto.Phone,
                Password = createSignUpDto.Password,
                ConfirmPassword = createSignUpDto.ConfirmPassword,
                Department = createSignUpDto.Department,
                PassingYear = createSignUpDto.PassingYear,
                Session = createSignUpDto.Session,
                CurrentPosition = createSignUpDto.CurrentPosition,
                AcceptTermsAndConditions = createSignUpDto.AcceptTermsAndConditions,
                DateOfBirth = createSignUpDto.DateOfBirth

            };
        }

    }
}
