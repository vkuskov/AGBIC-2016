//
// CoreSpec.cs
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
using System.Reflection;
using Entitas;
using Heartcatch.Core;
using NSpec;

namespace SeshFT.Gameplay.Test {
    
    public abstract class CoreSpec : nspec {

        protected Pool _pool;
        protected IDependencyManager _dm;
        protected Systems _systems;

        void before_each() {
            _pool = new Pool(CoreComponentIds.TotalComponents);
            _dm = new DependencyManager();
            _systems = new Systems();
        }

        protected void addSystem<TSystem>() where TSystem : BaseSystem, ISystem  {
            _systems.Add(_pool.CreateSystem(createSystem<TSystem>()));
        }

        private TSystem createSystem<TSystem>() where TSystem : BaseSystem, ISystem {
            return (TSystem)Activator.CreateInstance(typeof(TSystem), _dm);
        }

        protected Entity createEntity() {
            return _pool.CreateEntity();
        }

        protected void execute() {
            _systems.Execute();
        }
    }
}

