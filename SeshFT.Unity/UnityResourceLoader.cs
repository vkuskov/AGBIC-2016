﻿//
// UnityResourceLoader.cs
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
using UnityEngine;

namespace SeshFT.Unity {
    
    public class UnityResourceLoader : IResourceLoader {
        
        public IGameObject LoadGameObject(string assetBundle, string assetName) {
            // ignore assetBundle and use assetName as resouce name
            Debug.LogFormat("Loading resource \"{0}\"", assetName);
            var resource = Resources.Load(assetName);
            if (resource == null)
                throw new HeartcatchException(string.Format("Can't load  resource \"{0}\"", assetName));
            var unityGO = (GameObject)GameObject.Instantiate(resource);
            var go = unityGO.GetComponent<IGameObject>();
            if (go == null)
                throw new HeartcatchException(string.Format("Resource \"{0}\" doesn't have IGameObject component", assetName));
            return go;
        }
    }
}

