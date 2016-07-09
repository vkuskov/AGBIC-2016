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
using Heartcatch.Core;

namespace SeshFT.Gameplay.Test {
    public class describe_ViewFeature : CoreSpec {

        void describe_GameObjectLoader() {
            beforeEach = () => {
                _dm.Register<IResourceLoader>(Substitute.For<IResourceLoader>());
                addSystem<GameObjectLoaderSystem>();
            };
            it["Should load GameObject by name"] = () => {
                var loader = _dm.Get<IResourceLoader>();
                createEntity()
                    .AddResource("AssetBundle", "AssetName");
                execute();
                loader.Received().LoadGameObject("AssetBundle", "AssetName");
            };
            it["Should add GameObjectComponent"] = () => {
                var loader = _dm.Get<IResourceLoader>();
                var go = Substitute.For<IGameObject>();
                loader.LoadGameObject(Arg.Any<string>(), Arg.Any<string>()).Returns(go);
                var entity = createEntity()
                    .AddResource("AssetBundle", "AssetName");
                execute();
                entity.hasGameObject.should_be_true();
                entity.gameObject.value.should_be(go);
            };
            it["Should destroy GameObject if component was removed"] = () => {
                var go = Substitute.For<IGameObject>();
                var entity = createEntity()
                    .AddGameObject(go);
                entity.RemoveGameObject();
                go.Received().Destroy();
            };                
            it["Should destroy GameObject if entity was destroyed"] = () => {
                var go = Substitute.For<IGameObject>();
                var entity = createEntity()
                    .AddGameObject(go);
                _pool.DestroyEntity(entity);
                go.Received().Destroy();
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
                go.Received().OnAddedToEntity(entity);
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

        void describe_UnlinkGameObject() {
            beforeEach = () => {
                addSystem<UnlinkGameObjectSystem>();
            };
            it["Should remove all Update* components if GameObject was removed"] = () => {
                var entity = createEntity()
                    .AddGameObject(null)
                    .AddUpdateable(null)
                    .AddUpdateableAfter(null)
                    .AddUpdateableBefore(null);
                entity.RemoveGameObject();
                execute();
                entity.hasUpdateable.should_be_false();
                entity.hasUpdateableAfter.should_be_false();
                entity.hasUpdateableBefore.should_be_false();
            };
        }

        void describe_UnloadGameObject() {
            beforeEach = () => {
                addSystem<UnloadGameObjectSystem>();
            };
            it["Should remove GameObjectComponent if resource was removed"] = () => {
                var entity = createEntity()
                    .AddResource(null, null)
                    .AddGameObject(null);
                entity.RemoveResource();
                execute();
                entity.hasGameObject.should_be_false();
            };
            it["Should destory GameObject if resource was removed"] = () => {
                var loader = _dm.Register<IResourceLoader>(Substitute.For<IResourceLoader>());
                var go = Substitute.For<IGameObject>();
                loader.LoadGameObject(Arg.Any<string>(), Arg.Any<string>()).Returns(go);
                addSystem<GameObjectLoaderSystem>();
                var entity = createEntity()
                    .AddResource(null, null);
                execute();
                entity.hasGameObject.should_be_true();
                entity.RemoveResource();
                execute();
                entity.hasGameObject.should_be_false();
                go.Received().Destroy();
            };
        }

        void describe_UpdateSystems() {
            beforeEach = () => {
                _dm.Register<IGameTimeSystem>(Substitute.For<IGameTimeSystem>());
            };
            it["Should update all Update* components with same GameTime"] = () => {
                var updateable = Substitute.For<IUpdateable>();
                var updateableBefore = Substitute.For<IUpdateableBefore>();
                var updateableAfter = Substitute.For<IUpdateableAfter>();
                addSystem<UpdateBeforeSystem>();
                addSystem<UpdateSystem>();
                addSystem<UpdateAfterSystem>();
                createEntity()
                    .AddUpdateable(updateable)
                    .AddUpdateableBefore(updateableBefore)
                    .AddUpdateableAfter(updateableAfter);
                var gameTime = new GameTime(TimeSpan.FromSeconds(1.0), TimeSpan.FromSeconds(2.0), 3);
                setGameTime(gameTime);
                execute();
                updateableBefore.Received().OnUpdateBefore(gameTime);
                updateable.Received().OnUpdate(gameTime);
                updateableAfter.Received().OnUpdateAfter(gameTime);

            };
        }
    }
}

