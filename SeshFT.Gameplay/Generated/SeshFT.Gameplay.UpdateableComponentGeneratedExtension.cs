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
        public SeshFT.Gameplay.UpdateableComponent updateable { get { return (SeshFT.Gameplay.UpdateableComponent)GetComponent(CoreComponentIds.Updateable); } }

        public bool hasUpdateable { get { return HasComponent(CoreComponentIds.Updateable); } }

        public Entity AddUpdateable(SeshFT.Gameplay.IUpdateable newValue) {
            var component = CreateComponent<SeshFT.Gameplay.UpdateableComponent>(CoreComponentIds.Updateable);
            component.value = newValue;
            return AddComponent(CoreComponentIds.Updateable, component);
        }

        public Entity ReplaceUpdateable(SeshFT.Gameplay.IUpdateable newValue) {
            var component = CreateComponent<SeshFT.Gameplay.UpdateableComponent>(CoreComponentIds.Updateable);
            component.value = newValue;
            ReplaceComponent(CoreComponentIds.Updateable, component);
            return this;
        }

        public Entity RemoveUpdateable() {
            return RemoveComponent(CoreComponentIds.Updateable);
        }
    }
}

    public partial class CoreMatcher {
        static IMatcher _matcherUpdateable;

        public static IMatcher Updateable {
            get {
                if (_matcherUpdateable == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.Updateable);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherUpdateable = matcher;
                }

                return _matcherUpdateable;
            }
        }
    }
