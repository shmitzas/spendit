using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using WebApp.Models;

public class AuthProvider : AuthenticationStateProvider
{
    private readonly ProtectedSessionStorage _session;
    private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
    public AuthProvider(ProtectedSessionStorage session)
    {
        _session = session;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var userSessionResult = await _session.GetAsync<User>("UserSession");
            var userSession = userSessionResult.Success ? userSessionResult.Value : null;
            if (userSession == null)
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
            else
            {
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userSession.Id.ToString()),
                    new Claim(ClaimTypes.Name, userSession.Username),
                    new Claim("Password", userSession.Password),
                    new Claim(ClaimTypes.Email, userSession.Email),
                    new Claim("Settings", userSession.Settings)
                }, "CustomAuth"));
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
        }
        catch
        {
            return await Task.FromResult(new AuthenticationState(_anonymous));
        }

    }
    public async Task UpdateAuthState(User userSession)
    {
        try
        {
            ClaimsPrincipal claimsPrincipal;
            if (userSession != null)
            {
                await _session.SetAsync("UserSession", userSession);
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userSession.Id.ToString()),
                new Claim(ClaimTypes.Name, userSession.Username),
                new Claim("Password", userSession.Password),
                new Claim(ClaimTypes.Email, userSession.Email),
                new Claim("Settings", userSession.Settings)
            }));
            }
            else
            {
                await _session.DeleteAsync("UserSession");
                claimsPrincipal = _anonymous;
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"UpdateAuthState: {ex}");
        }
    }
    
    public async Task<User> GetCurrentUserAsync()
    {
        try
        {
            var authState = await GetAuthenticationStateAsync();
            return new User
            {
                Id = int.Parse(authState.User.FindFirstValue(ClaimTypes.NameIdentifier)),
                Username = authState.User.FindFirstValue(ClaimTypes.Name),
                Password = authState.User.FindFirstValue("Password"),
                Email = authState.User.FindFirstValue(ClaimTypes.Email),
                Settings = authState.User.FindFirstValue("Settings")
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new User();
        }
    }
}