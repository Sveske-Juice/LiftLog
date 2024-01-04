﻿using Fluxor;
using LiftLog.App.Services;
using LiftLog.Lib.Services;
using LiftLog.Ui;
using MaterialColorUtilities.Maui;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Storage;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using Sentry.Extensions.Logging.Extensions.DependencyInjection;
using INotificationService = LiftLog.Ui.Services.INotificationService;

namespace LiftLog.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();

        builder.Services.AddMauiBlazorWebView();

        builder.UseLocalNotification(notificationBuilder =>
        {
            notificationBuilder.SetSerializer(new NotificationSerializer());
            notificationBuilder.AddCategory(
                new NotificationCategory(NotificationCategoryType.Status)
                {
                    ActionList = new HashSet<NotificationAction>()
                    {
                        new NotificationAction(100)
                        {
                            Title = "Complete Set",
                            Android = { LaunchAppWhenTapped = false, }
                        },
                    }
                }
            );
            notificationBuilder.AddAndroid(android =>
            {
                android.AddChannel(
                    new NotificationChannelRequest()
                    {
                        Id = MauiNotificationService.NextSetNotificationChannelId,
                        Description =
                            "Notifications which remind you when your next set should be started",
                        Importance = AndroidImportance.High,
                        Name = "Set Timers",
                        EnableSound = true,
                        EnableVibration = true,
                        ShowBadge = true,
                        LockScreenVisibility = AndroidVisibilityType.Public,
                        CanBypassDnd = false,
                    }
                );
            });
        });

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif
        // Add this section anywhere on the builder:
        builder.Logging.AddSentry(options =>
        {
            // The DSN is the only required setting.
            options.Dsn =
                "https://94068122cc0e9b1bc7bf65b20bd88bfe@o4505937515249664.ingest.sentry.io/4505937523769344";

            options.Debug = false;

            // Set TracesSampleRate to 1.0 to capture 100% of transactions for performance monitoring.
            // We recommend adjusting this value in production.
            options.TracesSampleRate = 0;
            options.EnableTracing = false;
            options.CaptureFailedRequests = true;

            // Other Sentry options can be set here.
        });

        builder.Services.AddSingleton(new HttpClient());

        builder.Services.AddSingleton(Share.Default);
        builder.Services.AddSingleton(FilePicker.Default);

        builder.Services.RegisterUiServices<
            AppDataFileStorageKeyValueStore,
            SecureStoragePreferenceStore,
            MauiNotificationService,
            MauiShareExporter,
            AppThemeProvider,
            MauiStringSharer,
            AppPurchaseService,
            AesEncryptionService
        >();

        builder.UseMaterialColors<ThemeColorUpdateService>(opts =>
        {
            opts.FallbackSeed = 0xF44336;
            opts.EnableDynamicColor = Preferences.Default.Get("EnableDynamicColor", true);
        });
        return builder.Build();
    }
}
