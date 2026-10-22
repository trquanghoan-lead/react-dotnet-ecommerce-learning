using ECommerceBackend.Models;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace ECommerceBackend.Services;

public sealed class AccountService
{
    private readonly PasswordHasher<Account> _passwordHasher = new();
    private readonly List<StoredAccount> _accounts;

    public AccountService(IWebHostEnvironment environment)
    {
        var path = Path.Combine(environment.ContentRootPath, "Database", "accounts.json");
        var accounts = JsonSerializer.Deserialize<List<Account>>(File.ReadAllText(path), new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? [];

        _accounts = accounts.Select(account => new StoredAccount(
            account,
            _passwordHasher.HashPassword(account, account.Password))).ToList();
    }

    public Account? ValidateCredentials(string username, string password)
    {
        var stored = _accounts.FirstOrDefault(account =>
            string.Equals(account.Account.Username, username, StringComparison.OrdinalIgnoreCase));

        if (stored is null)
            return null;

        var result = _passwordHasher.VerifyHashedPassword(
            stored.Account,
            stored.PasswordHash,
            password);

        return result is PasswordVerificationResult.Success or PasswordVerificationResult.SuccessRehashNeeded
            ? stored.Account
            : null;
    }

    private sealed record StoredAccount(Account Account, string PasswordHash);
}
