using SF6_Plugin_Core.UI.InjectionShared;
using SF6_Plugin_Core.UI.Localization;
using SF6_Plugin_Core.UI.OptionsSettings.Core;
using SF6_Plugin_Core.UI.OptionsSettings.CustomOptions;
using SF6_Plugin_Core.UI.OptionsSettings.Requests;

namespace SF6_EXS.Settings;

public static class RenderScale
{
    private static readonly Guid IQR_SLIDER = Guid.NewGuid();

    private static readonly List<IUIModificationRequest> _modificationRequests = [];

    public static void Init()
    {
        var iqrMessage = new CustomMessage("Render Scale (%)")
            .AddTranslation(via.Language.Japanese, "レンダリングスケール (%)")
            .AddTranslation(via.Language.English, "Render Scale (%)")
            .AddTranslation(via.Language.French, "Échelle de rendu (%)")
            .AddTranslation(via.Language.Italian, "Scala di rendering (%)")
            .AddTranslation(via.Language.German, "Render-Skalierung (%)")
            .AddTranslation(via.Language.Spanish, "Escala de renderizado (%)")
            .AddTranslation(via.Language.Russian, "Масштаб рендеринга (%)")
            .AddTranslation(via.Language.Polish, "Skala renderowania (%)")
            .AddTranslation(via.Language.Dutch, "Render-schaal (%)")
            .AddTranslation(via.Language.Portuguese, "Escala de renderização (%)")
            .AddTranslation(via.Language.PortugueseBr, "Escala de renderização (%)")
            .AddTranslation(via.Language.Korean, "렌더링 스케일 (%)")
            .AddTranslation(via.Language.TransitionalChinese, "渲染比例 (%)")
            .AddTranslation(via.Language.SimplelifiedChinese, "渲染比例 (%)")
            .AddTranslation(via.Language.Finnish, "Renderöintiskaala (%)")
            .AddTranslation(via.Language.Swedish, "Renderingsskala (%)")
            .AddTranslation(via.Language.Danish, "Renderingsskala (%)")
            .AddTranslation(via.Language.Norwegian, "Renderingsskala (%)")
            .AddTranslation(via.Language.Czech, "Měřítko vykreslování (%)")
            .AddTranslation(via.Language.Hungarian, "Renderelési skála (%)")
            .AddTranslation(via.Language.Slovak, "Mierka vykresľovania (%)")
            .AddTranslation(via.Language.Arabic, "مقياس التصيير (%)")
            .AddTranslation(via.Language.Turkish, "İşleme Ölçeği (%)")
            .AddTranslation(via.Language.Bulgarian, "Мащаб на изобразяване (%)")
            .AddTranslation(via.Language.Greek, "Κλίμακα απόδοσης (%)")
            .AddTranslation(via.Language.Romanian, "Scara de randare (%)")
            .AddTranslation(via.Language.Thai, "สเกลการเรนเดอร์ (%)")
            .AddTranslation(via.Language.Ukrainian, "Масштаб рендерингу (%)")
            .AddTranslation(via.Language.Vietnamese, "Tỷ lệ kết xuất (%)")
            .AddTranslation(via.Language.Indonesian, "Skala Render (%)")
            .AddTranslation(via.Language.Fiction, "Render Scale (%)")
            .AddTranslation(via.Language.Hindi, "रेंडर स्केल (%)")
            .AddTranslation(via.Language.LatinAmericanSpanish, "Escala de renderizado (%)");

        var iqrDescription =
            new CustomMessage(
                    "Adjust the render scale of the game. This setting is only compatible with 'Internal Resolution' set to 5.")
                .AddTranslation(via.Language.Japanese, "ゲームのレンダリングスケールを調整します。この設定は「内部解像度」が5に設定されている場合のみ互換性があります。")
                .AddTranslation(via.Language.English,
                    "Adjust the render scale of the game. This setting is only compatible with 'Internal Resolution' set to 5.")
                .AddTranslation(via.Language.French,
                    "Ajuste l'échelle de rendu du jeu. Ce paramètre n'est compatible que si la 'Résolution interne' est réglée sur 5.")
                .AddTranslation(via.Language.Italian,
                    "Regola la scala di rendering del gioco. Questa impostazione è compatibile solo se la 'Risoluzione interna' è impostata su 5.")
                .AddTranslation(via.Language.German,
                    "Passt die Render-Skalierung des Spiels an. Diese Einstellung ist nur kompatibel, wenn die 'Interne Auflösung' auf 5 eingestellt ist.")
                .AddTranslation(via.Language.Spanish,
                    "Ajusta la escala de renderizado del juego. Esta configuración solo es compatible si la 'Resolución interna' está establecida en 5.")
                .AddTranslation(via.Language.Russian,
                    "Настройте масштаб рендеринга игры. Эта настройка совместима только в том случае, если для параметра «Внутреннее разрешение» установлено значение 5.")
                .AddTranslation(via.Language.Polish,
                    "Dostosuj skalę renderowania gry. To ustawienie jest kompatybilne tylko wtedy, gdy 'Wewnętrzna rozdzielczość' jest ustawiona na 5.")
                .AddTranslation(via.Language.Dutch,
                    "Pas de render-schaal van het spel aan. Deze instelling is alleen compatibel als de 'Interne resolutie' is ingesteld op 5.")
                .AddTranslation(via.Language.Portuguese,
                    "Ajuste a escala de renderização do jogo. Esta definição só é compatível com a 'Resolução Interna' definida para 5.")
                .AddTranslation(via.Language.PortugueseBr,
                    "Ajuste a escala de renderização do jogo. Essa configuração só é compatível se a 'Resolução Interna' estiver definida como 5.")
                .AddTranslation(via.Language.Korean, "게임의 렌더링 스케일을 조정합니다. 이 설정은 '내부 해상도'가 5로 설정된 경우에만 호환됩니다.")
                .AddTranslation(via.Language.TransitionalChinese, "調整遊戲的渲染比例。此設定僅在「內部解析度」設為 5 時相容。")
                .AddTranslation(via.Language.SimplelifiedChinese, "调整游戏的渲染比例。此设置仅在“内部分辨率”设置为5时兼容。")
                .AddTranslation(via.Language.Finnish,
                    "Säädä pelin renderöintiskaalaa. Tämä asetus on yhteensopiva vain, kun 'Sisäinen resoluutio' on asetettu arvoon 5.")
                .AddTranslation(via.Language.Swedish,
                    "Justera spelets renderingsskala. Denna inställning är endast kompatibel om 'Intern upplösning' är satt till 5.")
                .AddTranslation(via.Language.Danish,
                    "Juster spillets renderingsskala. Denne indstilling er kun kompatibel, hvis 'Intern opløsning' er sat til 5.")
                .AddTranslation(via.Language.Norwegian,
                    "Juster spillets renderingsskala. Denne innstillingen er bare kompatibel hvis 'Intern oppløsning' er satt til 5.")
                .AddTranslation(via.Language.Czech,
                    "Upravte měřítko vykreslování hry. Toto nastavení je kompatibilní pouze pokud je 'Vnitřní rozlišení' nastaveno na 5.")
                .AddTranslation(via.Language.Hungarian,
                    "Állítsa be a játék renderelési skáláját. Ez a beállítás csak akkor kompatibilis, ha a 'Belső felbontás' 5-re van állítva.")
                .AddTranslation(via.Language.Slovak,
                    "Upravte mierku vykresľovania hry. Toto nastavenie je kompatibilné iba vtedy, ak je 'Vnútorné rozlíšenie' nastavené na 5.")
                .AddTranslation(via.Language.Arabic,
                    "اضبط مقياس تصيير اللعبة. هذا الإعداد متوافق فقط إذا كانت 'الدقة الداخلية' مضبوطة على 5.")
                .AddTranslation(via.Language.Turkish,
                    "Oyunun işleme ölçeğini ayarlayın. Bu ayar yalnızca 'Dahili Çözünürlük' 5 olarak ayarlandığında uyumludur.")
                .AddTranslation(via.Language.Bulgarian,
                    "Регулирайте мащаба на изобразяване на играта. Тази настройка е съвместима само ако 'Вътрешна резолюция' е зададена на 5.")
                .AddTranslation(via.Language.Greek,
                    "Προσαρμόστε την κλίμακα απόδοσης του παιχνιδιού. Αυτή η ρύθμιση είναι συμβατή μόνο αν η 'Εσωτερική ανάλυση' έχει οριστεί στο 5.")
                .AddTranslation(via.Language.Romanian,
                    "Ajustați scara de randare a jocului. Această setare este compatibilă numai dacă 'Rezoluție internă' este setată la 5.")
                .AddTranslation(via.Language.Thai,
                    "ปรับสเกลการเรนเดอร์ของเกม การตั้งค่านี้เข้ากันได้กับ 'ความละเอียดภายใน' ที่ตั้งค่าเป็น 5 เท่านั้น")
                .AddTranslation(via.Language.Ukrainian,
                    "Налаштуйте масштаб рендерингу гри. Цей параметр сумісний лише тоді, коли для параметра «Внутрішня роздільна здатність» встановлено значення 5.")
                .AddTranslation(via.Language.Vietnamese,
                    "Điều chỉnh tỷ lệ kết xuất của trò chơi. Cài đặt này chỉ tương thích khi 'Độ phân giải nội bộ' được đặt thành 5.")
                .AddTranslation(via.Language.Indonesian,
                    "Sesuaikan skala render game. Pengaturan ini hanya kompatibel jika 'Resolusi Internal' disetel ke 5.")
                .AddTranslation(via.Language.Fiction,
                    "Adjust the render scale of the game. This setting is only compatible with 'Internal Resolution' set to 5.")
                .AddTranslation(via.Language.Hindi,
                    "गेम के रेंडर स्केल को समायोजित करें। यह सेटिंग केवल तभी संगत है जब 'आंतरिक रिज़ॉल्यूशन' 5 पर सेट हो।")
                .AddTranslation(via.Language.LatinAmericanSpanish,
                    "Ajusta la escala de renderizado del juego. Este ajuste solo es compatible si la 'Resolución interna' está configurada en 5.");

        var imageQualityRateNode = new UICustomOptionsValueNode(IQR_SLIDER, new SliderOptionInitializer(
            iqrMessage,
            iqrDescription,
            1, 300, (int)(via.render.Renderer.RenderConfig.ImageQualityRate * 100), 125
        ));
        imageQualityRateNode.AddHook(
            new OnDisableReasonHookRequest(() =>
                via.render.Renderer.RenderConfig.UpscaleType != via.render.FFX.UpscaleType.None));

        imageQualityRateNode.AddHook(new OnValueTypeEventHookRequest((_, value, _) =>
        {
            via.render.Renderer.RenderConfig.ImageQualityRate = value / 100.0f;
        }));

        var appendReq = new OptionsAppendElementsRequest([(0, imageQualityRateNode)]);

        try
        {
            OptionsSettingsManager.RegisterModification([(int)app.Option.TabType.Graphic, 6], appendReq);
        }
        catch (Exception ex)
        {
            API.LogError(ex.ToString());
        }

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