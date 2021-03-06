//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGenerator.ComponentExtensionsGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Entitas;

namespace Entitas {
    public partial class Entity {
        public SeshFT.Gameplay.DesiredMoveDirectionComponent desiredMoveDirection { get { return (SeshFT.Gameplay.DesiredMoveDirectionComponent)GetComponent(CoreComponentIds.DesiredMoveDirection); } }

        public bool hasDesiredMoveDirection { get { return HasComponent(CoreComponentIds.DesiredMoveDirection); } }

        public Entity AddDesiredMoveDirection(Heartcatch.MathLib.Vector2 newValue) {
            var component = CreateComponent<SeshFT.Gameplay.DesiredMoveDirectionComponent>(CoreComponentIds.DesiredMoveDirection);
            component.value = newValue;
            return AddComponent(CoreComponentIds.DesiredMoveDirection, component);
        }

        public Entity ReplaceDesiredMoveDirection(Heartcatch.MathLib.Vector2 newValue) {
            var component = CreateComponent<SeshFT.Gameplay.DesiredMoveDirectionComponent>(CoreComponentIds.DesiredMoveDirection);
            component.value = newValue;
            ReplaceComponent(CoreComponentIds.DesiredMoveDirection, component);
            return this;
        }

        public Entity RemoveDesiredMoveDirection() {
            return RemoveComponent(CoreComponentIds.DesiredMoveDirection);
        }
    }
}

    public partial class CoreMatcher {
        static IMatcher _matcherDesiredMoveDirection;

        public static IMatcher DesiredMoveDirection {
            get {
                if (_matcherDesiredMoveDirection == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.DesiredMoveDirection);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherDesiredMoveDirection = matcher;
                }

                return _matcherDesiredMoveDirection;
            }
        }
    }
