using Zenject;

namespace CodeBase.UI.Scenes.Menu.Windows.Levels
{
    public class MenuLevelElement
    {
        private readonly MenuLevelElementMediator _mediator;

        public MenuLevelElement(MenuLevelElementMediator mediator)
        {
            _mediator = mediator;
            
            _mediator.Button.onClick.AddListener(OnClick);
        }

        public class Factory : PlaceholderFactory<MenuLevelElementMediator, MenuLevelElement> { }

        private void OnClick()
        {
            
        }
    }
}