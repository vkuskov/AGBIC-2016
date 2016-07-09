//
// GameRunner.cs
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
using SeshFT.Gameplay;
using Heartcatch.Core;
using Heartcatch.Unity;
using UnityEngine;
using Entitas;

namespace SeshFT.Unity {
    
    public sealed class GameRunner : MonoBehaviour, IRunner {

        private Systems _systems;
        private GameTimeSystem _timeSystem;

        public void OnInit(IDependencyManager dm) {
            initDependencies(dm);
            _systems = createSystem(dm);

            Pools.core.CreateEntity()
                .AddResource(null, "TestCube")
                .AddTransformation(new Heartcatch.MathLib.Vector3(0, 0, 2.0f),
                    Heartcatch.MathLib.Quaternion.Identity);
        }

        private void initDependencies(IDependencyManager dm) {
            _timeSystem = new GameTimeSystem();
            dm.Register<IGameTimeSystem>(_timeSystem);
            dm.Register<IResourceLoader>(new UnityResourceLoader());
        }

        private Systems createSystem(IDependencyManager dm) {
            Systems systems = new Systems();
            systems.Add(new ClientCoreSystems(dm, Pools.core));
            return systems;
        }

        private void Update() {
            _timeSystem.OnTick(Time.deltaTime);
            _systems.Execute();
        }
    }
}

