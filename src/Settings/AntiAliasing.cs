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

        var aaMessage = new CustomMessage("Set the Antialiasing type")
            .AddTranslation(via.Language.Japanese, "アンチエイリアスのタイプを設定")
            .AddTranslation(via.Language.English, "Set the Antialiasing type")
            .AddTranslation(via.Language.French, "Définir le type d'anticrénelage")
            .AddTranslation(via.Language.Italian, "Imposta il tipo di antialiasing")
            .AddTranslation(via.Language.German, "Antialiasing-Typ festlegen")
            .AddTranslation(via.Language.Spanish, "Establecer el tipo de antialiasing")
            .AddTranslation(via.Language.Russian, "Установить тип сглаживания")
            .AddTranslation(via.Language.Polish, "Ustaw typ antyaliasingu")
            .AddTranslation(via.Language.Dutch, "Antialiasing-type instellen")
            .AddTranslation(via.Language.Portuguese, "Definir o tipo de antialiasing")
            .AddTranslation(via.Language.PortugueseBr, "Definir o tipo de antialiasing")
            .AddTranslation(via.Language.Korean, "안티앨리어싱 유형 설정")
            .AddTranslation(via.Language.TransitionalChinese, "設定反鋸齒類型")
            .AddTranslation(via.Language.SimplelifiedChinese, "设置抗锯齿类型")
            .AddTranslation(via.Language.Finnish, "Aseta reunanpehmennyksen tyyppi")
            .AddTranslation(via.Language.Swedish, "Ställ in typ av kantutjämning")
            .AddTranslation(via.Language.Danish, "Indstil type af kantudjævning")
            .AddTranslation(via.Language.Norwegian, "Angi type for kantutjevning")
            .AddTranslation(via.Language.Czech, "Nastavit typ vyhlazování")
            .AddTranslation(via.Language.Hungarian, "Élsimítás típusának beállítása")
            .AddTranslation(via.Language.Slovak, "Nastaviť typ vyhladzovania")
            .AddTranslation(via.Language.Arabic, "تعيين نوع منع التعرج")
            .AddTranslation(via.Language.Turkish, "Kenar yumuşatma türünü ayarla")
            .AddTranslation(via.Language.Bulgarian, "Задаване на тип заглаждане")
            .AddTranslation(via.Language.Greek, "Ορισμός τύπου εξομάλυνσης")
            .AddTranslation(via.Language.Romanian, "Setează tipul de antialiasing")
            .AddTranslation(via.Language.Thai, "ตั้งค่าประเภทการลบรอยหยัก")
            .AddTranslation(via.Language.Ukrainian, "Встановити тип згладжування")
            .AddTranslation(via.Language.Vietnamese, "Cài đặt loại khử răng cưa")
            .AddTranslation(via.Language.Indonesian, "Atur jenis Antialiasing")
            .AddTranslation(via.Language.Fiction, "Set the Antialiasing type")
            .AddTranslation(via.Language.Hindi, "एंटी-अलियासिंग प्रकार सेट करें")
            .AddTranslation(via.Language.LatinAmericanSpanish, "Configurar el tipo de antialiasing");

        var aaNode = new UICustomOptionsValueNode(AA_SPINNER, new SpinBoxOptionInitializer(
            new CustomMessage(Guid.Parse("6beb997e-e882-4ca3-b963-1f65ef2396aa")),
            aaMessage,
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
            OptionsSettingsManager.RegisterModification([(int)app.Option.TabType.Graphic, 5], eliminRequest);
            OptionsSettingsManager.RegisterModification([(int)app.Option.TabType.Graphic, 5], appendReq);
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
            foreach (var mod in _modificationRequests) OptionsSettingsManager.UnregisterModification(mod);

            _modificationRequests.Clear();
        }
        catch (Exception ex)
        {
            API.LogError(ex.ToString());
        }
    }
}