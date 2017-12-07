namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using TeamBuilder.App.Contracts;
    using TeamBuilder.App.ModelDto;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Models;
    using TeamBuildere.Services.Contracts;

    public class RegisterUserCommand : ICommand
    {
        private const int ArgsCount = 7;
        private const string NonMatchingPasswords = "Passwords do not match!";
        private const string UserExistsMessage = "Username {0} is already taken!";
        private const string LoggedInUserMessage = "You should logout first!";
        private const string SuccessfulRegistration = "User {0} was registered successfully!";
        private readonly IUserService userService;

        public RegisterUserCommand(IUserService userService)
        {
            this.userService = userService;
        }

        public string Execute(params string[] args)
        {
            string result = default(string);

            try
            {
                if (!Validate.ArgsCountValidate(ArgsCount, args))
                {
                    throw new FormatException(Constants.FormatException);
                }

                string username = args[0];
                string password = args[1];
                string repeatPassword = args[2];
                string firstName = args[3];
                string lastName = args[4];
                int age = int.Parse(args[5]);
                string gender = args[6];

                RegisterUserDto registerUser = new RegisterUserDto
                {
                    Username = username,
                    Password = password,
                    RepeatPassword = repeatPassword,
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age,
                    Gender = gender
                };

                // Validate
                List<ValidationResult> validationResult = Validate.EntityValidator(registerUser);

                if (validationResult != null)
                {
                    // We have some errors
                    result = validationResult.First().ErrorMessage;
                    return result;
                }

                if (registerUser.Password != registerUser.RepeatPassword)
                {
                    throw new InvalidOperationException(NonMatchingPasswords);
                }

                if (this.userService.UserExists(registerUser.Username))
                {
                    throw new InvalidOperationException(string.Format(UserExistsMessage, registerUser.Username));
                }

                if (UserSession.LoggedInUser != null)
                {
                    throw new InvalidOperationException(LoggedInUserMessage);
                }

                // Entity is valid
                User newUser = Mapper.Map<User>(registerUser);
                this.userService.RegisterUser(newUser);
                result = string.Format(SuccessfulRegistration, newUser.Username);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }
    }
}