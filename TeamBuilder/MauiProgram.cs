using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using TeamBuilder.Api.Services;
using TeamBuilder.Managers.Busy;
using TeamBuilder.Managers.Resource;
using TeamBuilder.Services.Network;
using TeamBuilder.Services.Storage;
using TeamBuilder.TeamMembers.Application;
using TeamBuilder.TeamMembers.Application.AddTeamMembers;
using TeamBuilder.TeamMembers.Domain;
using TeamBuilder.TeamMembers.Infrastructure;
using TeamBuilder.ViewModels.Menu;
using TeamBuilder.ViewModels.TeamMember;
using MauiStorage = Microsoft.Maui.Storage;

namespace TeamBuilder;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        CheckAndRequestStorageWritePermission();
        CheckAndRequestStorageReadPermission();

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .RegisterViewModels()
            .RegisterServices()
            .RegisterRoutes()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }


    /// <summary>
    /// Registers the view models.
    /// </summary>
    /// <param name="mauiAppBuilder">The maui app builder.</param>
    /// <returns>A MauiAppBuilder.</returns>
    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<AppShell, ShellViewModel>();
        mauiAppBuilder.Services.AddSingleton<TeamMembersPage, TeamMembersViewModel>();
        mauiAppBuilder.Services.AddSingleton<AddTeamMembersPage, TeamMembersViewModel>();

        return mauiAppBuilder;
    }

    /// <summary>
    /// Registers the services.
    /// </summary>
    /// <param name="mauiAppBuilder">The maui app builder.</param>
    /// <returns>A MauiAppBuilder.</returns>
    public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<IBusyManager, BusyManager>();
        mauiAppBuilder.Services.AddSingleton<INetworkService, NetworkService>();
        mauiAppBuilder.Services.AddSingleton<IResourceManager, Managers.Resource.ResourceManager>();
        mauiAppBuilder.Services.AddSingleton<ISecureStorageService, SecureStorageService>();
        mauiAppBuilder.Services.AddSingleton<MauiStorage.ISecureStorage>(SecureStorage.Default);
        mauiAppBuilder.Services.AddTransient<ITeamMembersRepository, DummyTeamMembersRepository>();
        mauiAppBuilder.Services.AddTransient<IApiService, ApiService>();

        return mauiAppBuilder;
    }

    /// <summary>
    /// Registers the appshell routes.
    /// </summary>
    /// <param name="mauiAppBuilder">The maui app builder.</param>
    /// <returns>A MauiAppBuilder.</returns>
    public static MauiAppBuilder RegisterRoutes(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingletonWithShellRoute<AppShell, ShellViewModel>(nameof(AppShell));
        mauiAppBuilder.Services.AddSingletonWithShellRoute<TeamMembersPage, TeamMembersViewModel>(nameof(TeamMembersPage));
        mauiAppBuilder.Services.AddSingletonWithShellRoute<AddTeamMembersPage, AddTeamMembersViewModel>(nameof(AddTeamMembersPage));

        return mauiAppBuilder;
    }

    #region Permissions
    public static async Task<PermissionStatus> CheckAndRequestStorageWritePermission()
    {
        PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

        if (status == PermissionStatus.Granted)
            return status;

        if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
        {
            return status;
        }

        if (Permissions.ShouldShowRationale<Permissions.StorageWrite>())
        {
        }

        status = await Permissions.RequestAsync<Permissions.StorageWrite>();

        return status;
    }

    public static async Task<PermissionStatus> CheckAndRequestStorageReadPermission()
    {
        PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

        if (status == PermissionStatus.Granted)
            return status;

        if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
        {
            return status;
        }

        if (Permissions.ShouldShowRationale<Permissions.StorageWrite>())
        {
        }

        status = await Permissions.RequestAsync<Permissions.StorageRead>();

        return status;
    }
    #endregion

}

