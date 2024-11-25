using Zenject;

namespace CodeBase.UI.Scenes.Menu.Windows.Levels
{
    public class ClosedMenuLevelElement
    {
        public ClosedMenuLevelElement(MenuLevelElementMediator mediator, int levelIndex)
        {
            mediator.Label.text = (levelIndex + 1).ToString();
        }

        public class Factory : PlaceholderFactory<MenuLevelElementMediator, int, ClosedMenuLevelElement> { }
    }
}