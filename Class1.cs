using System.Collections.Generic;
using BananaWatch;
using BananaWatch.Pages;
using BepInEx;

namespace Template
{
    [BepInPlugin("BananaWatch.TimeChanger", "Time Changer", "1.0.0")]
    public class Template : BaseUnityPlugin { }

    public class TemplatePage : BananaWatchPage
    {
        public override string Title => "Time Changer";
        public override bool PublicPage => true;

        private SelectionHandler selectionHandler = new SelectionHandler();

        private List<string> menuOptions = new List<string>
        {
            "Change Time To Day",
            "Change Time To Night",
            "Make It Rain",
        };

        public override void PageOpened()
        {
            selectionHandler.maxIndex = menuOptions.Count - 1;
            selectionHandler.currentIndex = 0;
        }

        public override string RenderScreenContent()
        {
            string header = "<color=#a11af0>==</color> Time Changer <color=#a11af0>==</color>\n\n";

            string content = header;
            for (int i = 0; i < menuOptions.Count; i++)
            {
                content += selectionHandler.SelectionArrow(i, menuOptions[i]) + "\n";
            }

            return content;
        }

        public override void ButtonPressed(BananaWatchButton buttonType)
        {
            switch (buttonType)
            {
                case BananaWatchButton.Up:
                    selectionHandler.MoveSelectionUp();
                    break;
                case BananaWatchButton.Down:
                    selectionHandler.MoveSelectionDown();
                    break;
                case BananaWatchButton.Enter:
                    switch (selectionHandler.currentIndex)
                    {
                        case 0:
                            BetterDayNightManager.instance.SetTimeOfDay(3); //day
                            break;
                        case 1:
                            BetterDayNightManager.instance.SetTimeOfDay(0); //night
                            break;
                        case 2:
                                for (int i = 1; i < BetterDayNightManager.instance.weatherCycle.Length; i++)
                                {
                                    BetterDayNightManager.instance.weatherCycle[i] = BetterDayNightManager.WeatherType.Raining;
                                } //rain
                            break;
                    }
                    break;
                case BananaWatchButton.Back:
                    NavigateToMainMenu();
                    break;
            }
        }

    }
}
