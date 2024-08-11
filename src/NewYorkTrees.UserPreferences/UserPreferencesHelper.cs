using System.IO.IsolatedStorage;
using System.Text.Json;

namespace NewYorkTrees.UserPreferences;

public class UserPreferencesHelper
{
    private readonly UserPreferences userPreferences;
    private readonly UserPreferences.Validator validator;
    private const string FileName = "UserPreferences.json";

    private readonly IsolatedStorageFile isoStore;

    public UserPreferencesHelper()
    {
        this.isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

        if (this.isoStore.FileExists(FileName))
        {
            using var isoStream = new IsolatedStorageFileStream(FileName, FileMode.Open, this.isoStore);

            this.userPreferences = JsonSerializer.Deserialize<UserPreferences>(isoStream) ?? new UserPreferences();
        }
        else
        {
            using var isoStream = new IsolatedStorageFileStream(FileName, FileMode.CreateNew, this.isoStore);

            this.userPreferences = new UserPreferences();

            JsonSerializer.Serialize(isoStream, this.userPreferences);
        }

        this.validator = new UserPreferences.Validator();
    }

    public string GetUserTheme()
    {
        return this.userPreferences.UserTheme;
    }

    public bool SetUserTheme(string themeName)
    {
        var oldValue = this.userPreferences.UserTheme;
        this.userPreferences.UserTheme = themeName;

        if (!this.validator.Validate(this.userPreferences).IsValid)
        {
            this.userPreferences.UserTheme = oldValue;
            return false;
        }

        using var isoStream = new IsolatedStorageFileStream(FileName, FileMode.Truncate, this.isoStore);
        JsonSerializer.Serialize(isoStream, this.userPreferences);
        return true;
    }
}