global using System;
global using System.Collections.Generic;
global using System.Runtime.InteropServices;
global using System.Linq;
global using REFrameworkNET;
global using REFrameworkNET.Attributes;
global using REFrameworkNET.Collections;
global using REFrameworkNET.Callbacks;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Loader;
using SF6_Plugin_Core;
using SF6_Plugin_Core.Services;

namespace SF6_EXS;


public class ExpandedSettings
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void InitializePlugin()
    {
        CoreLifecycle.OnCoreUnloading += PluginExitPoint;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void UnloadPlugin()
    {
        // Unsubscribe from Core events/singletons so Core holds zero references to this assembly
        CoreLifecycle.OnCoreUnloading -= PluginExitPoint;
    }

    /// Default plugin entry and exit points
    [PluginEntryPoint]
    public static void PluginEntryPoint()
    {
        var currentALC = AssemblyLoadContext.GetLoadContext(Assembly.GetExecutingAssembly());
        if (currentALC == null)
        {
            API.LogError("Failed to get the current AssemblyLoadContext. Plugin initialization aborted.");
            return;
        }

        currentALC.Resolving += OnResolvingCore;

        InitializePlugin();
    }

    [PluginExitPoint]
    public static void PluginExitPoint()
    {
        try
        {
            UnloadPlugin();
        }
        catch (Exception ex)
        {
            API.LogError($"Exception during PluginExitPoint: {ex}");
        }

        try
        {
            var currentALC = AssemblyLoadContext.GetLoadContext(Assembly.GetExecutingAssembly());

            currentALC?.Resolving -= OnResolvingCore;
        }
        catch (Exception ex)
        {
            API.LogError($"Exception during AssemblyLoadContext cleanup: {ex}");
        }
    }

    private static Assembly OnResolvingCore(AssemblyLoadContext context, AssemblyName assemblyName)
    {
        if (assemblyName.Name == "00_SF6PluginCore")
            // Search all AssemblyLoadContexts to find where REFramework loaded the Core plugin
            foreach (var alc in AssemblyLoadContext.All)
            foreach (var loadedAssembly in alc.Assemblies)
                if (loadedAssembly.GetName().Name == "00_SF6PluginCore")
                    return loadedAssembly;

        API.LogError(
            $"Failed to resolve assembly: {assemblyName.Name}. Ensure that the SF6_Plugin_Core is loaded and accessible.");
#pragma warning disable CS8603 // Possible null reference return.
        return null; // Let default resolution fail if not found
#pragma warning restore CS8603 // Possible null reference return.
    }
}