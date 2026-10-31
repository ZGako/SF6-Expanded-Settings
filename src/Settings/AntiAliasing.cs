using SF6_Plugin_Core.UI.InjectionShared;
using SF6_Plugin_Core.UI.Localization;
using SF6_Plugin_Core.UI.OptionsSettings.Core;
using SF6_Plugin_Core.UI.OptionsSettings.CustomOptions;
using SF6_Plugin_Core.UI.OptionsSettings.Requests;

namespace SF6_EXS.Settings;

public static class AntiAliasing
{
    private static readonly Guid AA_SPINNER = Guid.NewGuid();

    private static readonly List<IUIModificationRequest> _modificationRequests = [];

    public static void Init()
    {
        // hide the "real" AA tab
        var eliminRequest = new RemoveElementsRequest([11]);

        var aaNode = new UICustomOptionsValueNode(AA_SPINNER, new SpinBoxOptionInitializer(
            new CustomMessage(Guid.Parse("6beb997e-e882-4ca3-b963-1f65ef2396aa")),
            new CustomMessage("Change AntiAliasing option setting"),
            [
                new CustomMessage("FXAA"),
                new CustomMessage("TAA"),
                new CustomMessage("FXAA_TAA"),
                new CustomMessage("SMAA"),
                new CustomMessage("None")
            ],
            (int)via.render.Renderer.RenderConfig.AntiAliasing, 1
        ));
        aaNode.AddHook(new OnValueTypeEventHookRequest((_, value, _) =>
        {
            via.render.Renderer.RenderConfig.AntiAliasing = (via.render.RenderConfig.AntiAliasingType)value;
        }));

        var appendReq = new OptionsAppendElementsRequest([(11, aaNode)]);

        try
        {
            OptionsSettingsManager.ModifyUI(() =>
            {
                OptionsSettingsManager.RegisterModification([(int)app.Option.TabType.Graphic, 5], eliminRequest);
                OptionsSettingsManager.RegisterModification([(int)app.Option.TabType.Graphic, 5], appendReq);
            });
        }
        catch (Exception ex)
        {
            API.LogError(ex.ToString());
        }

        _modificationRequests.Add(eliminRequest);
        _modificationRequests.Add(appendReq);
    }

    public static void Unload()
    {
        try
        {
            OptionsSettingsManager.ModifyUI(() =>
            {
                foreach (var mod in _modificationRequests) OptionsSettingsManager.UnregisterModification(mod);
            });
            _modificationRequests.Clear();
        }
        catch (Exception ex)
        {
            API.LogError(ex.ToString());
        }
    }
}