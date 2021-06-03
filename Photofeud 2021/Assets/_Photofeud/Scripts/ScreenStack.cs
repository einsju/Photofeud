using System.Collections.Generic;
using System.Linq;

namespace Photofeud
{
    public class ScreenStack
    {
        public List<Screen> Screens { get; private set; }

        public Screen CurrentScreen => Screens.LastOrDefault() ?? _defaultScreen;
        public Screen PreviousScreen => CurrentScreen != null && Screens.Count > 1 ? Screens[Screens.IndexOf(CurrentScreen) - 1] : _defaultScreen;

        Screen _defaultScreen;

        public ScreenStack(Screen defaultScreen)
        {
            Screens = new List<Screen>();
            _defaultScreen = defaultScreen;
        }

        public void Add(Screen screen)
        {
            if (Screens.Contains(screen)) return;
            Screens.Add(screen);
        }

        public void Remove(Screen screen)
        {
            if (!Screens.Contains(screen)) return;
            Screens.Remove(screen);
        }

        public void Clear()
        {
            Screens.Clear();
        }

        public void ClearHistoryAndSetTop(Screen screen)
        {
            Screens.Clear();
            Screens.Add(screen);
        }
    }
}
