﻿//
// GameObjectLoaderSystem.cs
//
// Author:
//       Vladimir Kuskov <vladimir.kuskov@hotmail.com>
//
// Copyright (c) 2016 Vladimir Kuskov
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using Entitas;
using Heartcatch.Core;

namespace SeshFT.Gameplay {
    
    public class GameObjectLoaderSystem : BaseSystem, IReactiveSystem, ISetPool {
        [Inject]
        private IResourceLoader _loader;

        public GameObjectLoaderSystem(IDependencyManager dm) : base(dm) {
        }

        public void Execute(List<Entity> entities) {
            foreach (var it in entities) {
                var resource = it.resource;
                var go = _loader.LoadGameObject(resource.assetBundle, resource.assetName);
                it.AddGameObject(go);
            }
        }

        private void onGameObjectRemoved(Group group, Entity entity, int index, IComponent component) {
            if (component is GameObjectComponent) {
                var go = ((GameObjectComponent)component).value;
                go.Destroy();
            }
        }

        public TriggerOnEvent trigger {
            get {
                return CoreMatcher.Resource.OnEntityAdded();
            }
        }

        public void SetPool(Pool pool) {
            pool.GetGroup(CoreMatcher.GameObject).OnEntityRemoved += onGameObjectRemoved;
        }
    }

}

