//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGenerator.PoolsGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Entitas;

public static class Pools {

    static Pool[] _allPools;

    public static Pool[] allPools {
        get {
            if (_allPools == null) {
                _allPools = new [] { core, meta };
            }

            return _allPools;
        }
    }

    static Pool _core;

    public static Pool core {
        get {
            if (_core == null) {
                _core = new Pool(CoreComponentIds.TotalComponents, 0, new PoolMetaData("Core Pool", CoreComponentIds.componentNames, CoreComponentIds.componentTypes));
                #if (!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)
                if (UnityEngine.Application.isPlaying) {
                    var poolObserver = new Entitas.Unity.VisualDebugging.PoolObserver(_core);
                    UnityEngine.Object.DontDestroyOnLoad(poolObserver.entitiesContainer);
                }
                #endif
            }

            return _core;
        }
    }

    static Pool _meta;

    public static Pool meta {
        get {
            if (_meta == null) {
                _meta = new Pool(MetaComponentIds.TotalComponents, 0, new PoolMetaData("Meta Pool", MetaComponentIds.componentNames, MetaComponentIds.componentTypes));
                #if (!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)
                if (UnityEngine.Application.isPlaying) {
                    var poolObserver = new Entitas.Unity.VisualDebugging.PoolObserver(_meta);
                    UnityEngine.Object.DontDestroyOnLoad(poolObserver.entitiesContainer);
                }
                #endif
            }

            return _meta;
        }
    }
}