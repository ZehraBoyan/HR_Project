using FluentValidation;
using IK_Project.Core.DTOs;
using IK_Project.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IK_Project.Service.Validations
{
	public class PersonelDtoValidator:AbstractValidator<PersonelDTO>
	{
		public PersonelDtoValidator()
		{
			RuleFor(x => x.Photo)
			.Must((personel, photo) =>
			{
				if (string.IsNullOrEmpty(photo))
				{
					string defaultPhotoUrl = GetDefaultPhotoUrlByGender(personel.Gender);
					personel.Photo = defaultPhotoUrl;
				}
				return true;
			})
			.WithMessage("The photo field cannot be empty!");


			RuleFor(e => e.FirstName).NotEmpty().MaximumLength(50);

			RuleFor(e => e.MiddleName).MaximumLength(50);

			RuleFor(e => e.LastName).NotEmpty().MaximumLength(50);

			RuleFor(e => e.SecondLastName).MaximumLength(50);

			RuleFor(e => e.BirthDate).NotEmpty();

			RuleFor(e => e.BirthPlace).NotEmpty().MaximumLength(100);

			RuleFor(e => e.TcIdentityNo).NotEmpty().Length(11);

			RuleFor(e => e.HireDate).NotEmpty();

			RuleFor(e => e.DismissalDate).Must((personel, dismissalDate) =>
				dismissalDate == null || dismissalDate >= personel.HireDate)
				.WithMessage("Dismissal Date cannot be earlier than Hire Date!");

			RuleFor(e => e.CompanyName).NotEmpty().MaximumLength(100);

			RuleFor(e => e.Department).NotEmpty().MaximumLength(100);

			RuleFor(e => e.Occupation).NotEmpty().MaximumLength(100);

			RuleFor(e => e.Address).NotEmpty().MaximumLength(200);

			RuleFor(e => e.PhoneNumber).NotEmpty().MaximumLength(11).Matches(new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}")).WithMessage("PhoneNumber not valid");

			RuleFor(e => e.Salary).NotEmpty().GreaterThan(0);

			RuleFor(x => x.Email)
			.NotNull().WithMessage("Email cannot be null.")
			.NotEmpty().WithMessage("Email cannot be blank.")
			.Matches(@"[a-z0-9]+@[a-z]+\.[a-z]{2,3}").WithMessage("The email is invalid.");

			RuleFor(x => x.Password)
		   .NotNull().WithMessage("Password cannot be null.")
		   .NotEmpty().WithMessage("Password cannot be blank.");
		}

		private string GetDefaultPhotoUrlByGender(Gender gender)
		{
			string defaultPhotoUrl;

			if (gender == Gender.Male)
			{
				// Erkek için varsayılan fotoğraf URL'i
				defaultPhotoUrl = "https://example.com/default-male-photo.jpg";
			}
			else if (gender == Gender.Female)
			{
				// Kadın için varsayılan fotoğraf URL'i
				defaultPhotoUrl = "https://example.com/default-female-photo.jpg";
			}
			else
			{
				// Cinsiyet belirtilmediyse veya geçersizse kullanılacak genel bir varsayılan fotoğraf URL'i
				defaultPhotoUrl = "https://example.com/default-photo.jpg";
			}

			return defaultPhotoUrl;
		}
	}
}

