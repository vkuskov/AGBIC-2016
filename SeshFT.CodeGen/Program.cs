//
// Program.cs
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
using Entitas.CodeGenerator;

namespace SeshFT.CodeGen {
    class MainClass {
        public static void Main(string[] args) {
            generate();
        }

        static void generate() {

            // All code generators that should be used
            var codeGenerators = new ICodeGenerator[] {
                new ComponentIndicesGenerator(),
                new ComponentExtensionsGenerator(),
                new PoolAttributesGenerator(),
                new PoolsGenerator(),
                new BlueprintsGenerator()
            };

            // Specify all pools
            var poolNames = new [] { "Core", "Meta" };

            // Specify all blueprints
            var blueprintNames = new string[0];

            var assembly = Assembly.GetAssembly(typeof(Entity));
            var provider = new TypeReflectionProvider(assembly.GetTypes(), poolNames, blueprintNames);

            const string path = "SeshFT.Gameplay/Generated/";
            var files = CodeGenerator.Generate(provider, path, codeGenerators);

            foreach (var file in files) {
                Console.WriteLine(file.generatorName + ": " + file.fileName);
            }
        }
    }
}
