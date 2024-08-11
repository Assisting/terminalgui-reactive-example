using FluentValidation;

namespace NewYorkTrees.UserPreferences;

public class UserPreferences
{
    public string UserTheme { get; set; }

    public UserPreferences()
    {
        this.UserTheme = "Dark";
    }

    public class Validator : AbstractValidator<UserPreferences>
    {
        public Validator()
        {
            this.RuleFor(preferences => preferences.UserTheme)
                .NotEmpty()
                .Must(str => str is "Dark" or "Light" or "Default");
        }
    }
}