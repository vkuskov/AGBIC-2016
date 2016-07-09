//
// describe_ViewFeature.cs
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
using NSpec;
using NSubstitute;
using Entitas;

namespace SeshFT.Gameplay.Test {
    public class describe_ViewFeature : CoreSpec {

        void describe_GameObjectLoader() {
            IResourceLoader loader = null;
            beforeEach = () => {
                loader = Substitute.For<IResourceLoader>();
                _dm.Register<IResourceLoader>(loader);
                addSystem<GameObjectLoaderSystem>();
            };
            it["Should load GameObject by name"] = () => {
                createEntity()
                    .AddResource("AssetBundle", "AssetName");
                execute();
                loader.Received().LoadGameObject("AssetBundle", "AssetName");
            };
        }

        void describe_LinkGameObject() {
            beforeEach = () => {
                addSystem<LinkGameObjectSystem>();
            };
            it["Should link GameObject to Entity"] = () => {
                var go = Substitute.For<IGameObject>();
                var entity = createEntity()
                    .AddGameObject(go);
                execute();
                go.Received().OnEntityCreated(entity);
            };
            it["Should add UpdateableComponent if object has IUpdateable"] = () => {
                var updateable = Substitute.For<IGameObject, IUpdateable>();
                var entity = createEntity()
                    .AddGameObject(updateable);
                execute();
                entity.hasUpdateable.should_be_true();
                entity.updateable.value.should_be(updateable);
            };
            it["Should add UpdateableBeforeComponent if object has IUpdateableBefore"] = () => {
                var updateable = Substitute.For<IGameObject, IUpdateableBefore>();
                var entity = createEntity()
                    .AddGameObject(updateable);
                execute();
                entity.hasUpdateableBefore.should_be_true();
                entity.updateableBefore.value.should_be(updateable);
            };
            it["Should add UpdateableAfterComponent if object has IUpdateableAfter"] = () => {
                var updateable = Substitute.For<IGameObject, IUpdateableAfter>();
                var entity = createEntity()
                    .AddGameObject(updateable);
                execute();
                entity.hasUpdateableAfter.should_be_true();
                entity.updateableAfter.value.should_be(updateable);
            };
        }
    }
}

