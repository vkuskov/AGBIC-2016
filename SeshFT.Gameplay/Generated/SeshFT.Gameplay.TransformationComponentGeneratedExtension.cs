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
        public SeshFT.Gameplay.TransformationComponent transformation { get { return (SeshFT.Gameplay.TransformationComponent)GetComponent(CoreComponentIds.Transformation); } }

        public bool hasTransformation { get { return HasComponent(CoreComponentIds.Transformation); } }

        public Entity AddTransformation(Heartcatch.MathLib.Vector3 newPosition, Heartcatch.MathLib.Quaternion newRotation) {
            var component = CreateComponent<SeshFT.Gameplay.TransformationComponent>(CoreComponentIds.Transformation);
            component.position = newPosition;
            component.rotation = newRotation;
            return AddComponent(CoreComponentIds.Transformation, component);
        }

        public Entity ReplaceTransformation(Heartcatch.MathLib.Vector3 newPosition, Heartcatch.MathLib.Quaternion newRotation) {
            var component = CreateComponent<SeshFT.Gameplay.TransformationComponent>(CoreComponentIds.Transformation);
            component.position = newPosition;
            component.rotation = newRotation;
            ReplaceComponent(CoreComponentIds.Transformation, component);
            return this;
        }

        public Entity RemoveTransformation() {
            return RemoveComponent(CoreComponentIds.Transformation);
        }
    }
}

    public partial class CoreMatcher {
        static IMatcher _matcherTransformation;

        public static IMatcher Transformation {
            get {
                if (_matcherTransformation == null) {
                    var matcher = (Matcher)Matcher.AllOf(CoreComponentIds.Transformation);
                    matcher.componentNames = CoreComponentIds.componentNames;
                    _matcherTransformation = matcher;
                }

                return _matcherTransformation;
            }
        }
    }
