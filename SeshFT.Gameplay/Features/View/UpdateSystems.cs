//
// UpdateSystems.cs
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

    public class UpdateSystem : BaseSystem, IExecuteSystem, ISetPool {
        [Inject]
        private IGameTimeSystem _timeSystem;

        private Group _group;

        public UpdateSystem(IDependencyManager dm) : base(dm) {
        }

        public void Execute() {
            var gameTime = _timeSystem.CurrentGameTime;
            foreach (var it in _group.GetEntities()) {
                it.updateable.value.OnUpdate(gameTime);
            }
        }

        public void SetPool(Pool pool) {
            _group = pool.GetGroup(CoreMatcher.Updateable);
        }
    }

    public class UpdateBeforeSystem : BaseSystem, IExecuteSystem, ISetPool {
        [Inject]
        private IGameTimeSystem _timeSystem;

        private Group _group;

        public UpdateBeforeSystem(IDependencyManager dm) : base(dm) {
        }

        public void Execute() {
            var gameTime = _timeSystem.CurrentGameTime;
            foreach (var it in _group.GetEntities()) {
                it.updateableBefore.value.OnUpdateBefore(gameTime);
            }
        }

        public void SetPool(Pool pool) {
            _group = pool.GetGroup(CoreMatcher.UpdateableBefore);
        }
    }

    public class UpdateAfterSystem : BaseSystem, IExecuteSystem, ISetPool {
        [Inject]
        private IGameTimeSystem _timeSystem;

        private Group _group;

        public UpdateAfterSystem(IDependencyManager dm) : base(dm) {
        }

        public void Execute() {
            var gameTime = _timeSystem.CurrentGameTime;
            foreach (var it in _group.GetEntities()) {
                it.updateableAfter.value.OnUpdateAfter(gameTime);
            }
        }

        public void SetPool(Pool pool) {
            _group = pool.GetGroup(CoreMatcher.UpdateableAfter);
        }
    }
}