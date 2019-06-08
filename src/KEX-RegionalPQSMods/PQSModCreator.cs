using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Kopernicus;
using Kopernicus.ConfigParser;
using Kopernicus.ConfigParser.Attributes;
using Kopernicus.ConfigParser.BuiltinTypeParsers;
using Kopernicus.Configuration;
using Kopernicus.Configuration.ModLoader;
using Kopernicus.Configuration.Parsing;
using TriAxis.RunSharp;
using UnityEngine;

namespace KopernicusExpansion
{
    namespace RegionalPQSMods
    {
        /// <summary>
        /// Generates regionally limited PQSMods using at runtime compilation
        /// </summary>
        [KSPAddon(KSPAddon.Startup.Instantly, true)]
        public class PQSModCreator : MonoBehaviour
        {
            /// <summary>
            /// Returns a list of all loaded types
            /// </summary>
            private static List<Type> GetModTypes()
            {
                List<Type> _ModTypes = new List<Type>();
                List<Assembly> asms = new List<Assembly>();
                asms.AddRange(AssemblyLoader.loadedAssemblies.Select(la => la.assembly));
                asms.AddUnique(typeof(PQSMod_VertexSimplexHeightAbsolute).Assembly);
                asms.AddUnique(typeof(PQSLandControl).Assembly);
                foreach (Type t in asms.SelectMany(a => a.GetTypes()))
                {
                    _ModTypes.Add(t);
                }
                return _ModTypes;
            }
            
            /// <summary>
            /// Iterate over all loaded mods and emit new types
            /// </summary>
            void Awake()
            {
                // Generate overloaded PQSMod types
                AssemblyGen assembly = new AssemblyGen(Guid.NewGuid().ToString(),
                    new CompilerOptions {OutputPath = Path.GetTempFileName()});
                
                List<Type> modTypes = GetModTypes();
                foreach (Type modType in modTypes)
                {
                    if (typeof(PQSMod).IsAssignableFrom(modType))
                    {
                        // Get the ModLoader type we want to extend 
                        Type loaderType = modTypes.FirstOrDefault(t =>
                            t.BaseType != null && t.BaseType.FullName != null &&
                            t.BaseType.FullName.StartsWith("Kopernicus.Configuration.ModLoader.ModLoader") &&
                            t.BaseType.GetGenericArguments()[0] == modType);
                        if (loaderType == null)
                            continue;

                        // Generate the Mod Type
                        TypeGen modGen = assembly.Public.Class($"{modType.Name}Regional", modType);
                        {
                            FieldGen multiplierMap = modGen.Public.Field(typeof(MapSO), "multiplierMap");
                            FieldGen splitChannels = modGen.Public.Field(typeof(Boolean), "splitChannels");
                            FieldGen multiplier = modGen.Private.Field(typeof(Color), "multiplier");
                            FieldGen preBuildColor = modGen.Private.Field(typeof(Color), "preBuildColor");
                            FieldGen preBuildHeight = modGen.Private.Field(typeof(Double), "preBuildHeight");

                            // OnVertexBuildHeight
                            CodeGen onVertexBuild = modGen.Public.Override.Method(typeof(void), "OnVertexBuild")
                                .Parameter(typeof(PQS.VertexBuildData), "data");
                            {
                                ContextualOperand data = onVertexBuild.Arg("data");
                                onVertexBuild.Assign(multiplier, onVertexBuild.Local(multiplierMap.Invoke(
                                    "GetPixelColor", new TypeMapper(),
                                    data.Field("u"), data.Field("v"))));
                                onVertexBuild.If(!splitChannels);
                                {
                                    onVertexBuild.Assign(multiplier.Field("a", new TypeMapper()),
                                        multiplier.Field("r", new TypeMapper()));
                                }
                                onVertexBuild.End();
                                onVertexBuild.Assign(preBuildColor, data.Field("vertColor"));
                                onVertexBuild.Assign(preBuildHeight, data.Field("vertHeight"));
                                onVertexBuild.Invoke(onVertexBuild.Base(), "OnVertexBuild", data);
                                onVertexBuild.Assign(data.Field("vertColor"),
                                    assembly.StaticFactory.Invoke(typeof(Color), "Lerp", preBuildColor,
                                        data.Field("vertColor"), multiplier.Field("a", new TypeMapper())));
                                onVertexBuild.Assign(data.Field("vertHeight"),
                                    assembly.StaticFactory.Invoke(typeof(UtilMath), "Lerp", preBuildHeight,
                                        data.Field("vertHeight"), multiplier.Field("r", new TypeMapper())));
                            }
                            
                            // OnVertexBuildHeight
                            CodeGen onVertexBuildHeight = modGen.Public.Override.Method(typeof(void), "OnVertexBuildHeight")
                                .Parameter(typeof(PQS.VertexBuildData), "data");
                            {
                                ContextualOperand data = onVertexBuildHeight.Arg("data");
                                onVertexBuildHeight.Assign(multiplier, onVertexBuildHeight.Local(multiplierMap.Invoke(
                                    "GetPixelColor", new TypeMapper(),
                                    data.Field("u"), data.Field("v"))));
                                onVertexBuildHeight.If(!splitChannels);
                                {
                                    onVertexBuildHeight.Assign(multiplier.Field("a", new TypeMapper()),
                                        multiplier.Field("r", new TypeMapper()));
                                }
                                onVertexBuildHeight.End();
                                onVertexBuildHeight.Assign(preBuildColor, data.Field("vertColor"));
                                onVertexBuildHeight.Assign(preBuildHeight, data.Field("vertHeight"));
                                onVertexBuildHeight.Invoke(onVertexBuildHeight.Base(), "OnVertexBuildHeight", data);
                                onVertexBuildHeight.Assign(data.Field("vertColor"),
                                    assembly.StaticFactory.Invoke(typeof(Color), "Lerp", preBuildColor,
                                        data.Field("vertColor"), multiplier.Field("a", new TypeMapper())));
                                onVertexBuildHeight.Assign(data.Field("vertHeight"),
                                    assembly.StaticFactory.Invoke(typeof(UtilMath), "Lerp", preBuildHeight,
                                        data.Field("vertHeight"), multiplier.Field("r", new TypeMapper())));
                            }
                        }
                        
                        // Generate the Loader Type
                        Type modLoader = typeof(ModLoader<>).MakeGenericType(modGen);
                        TypeGen loaderGen =
                            assembly.Public.Class($"{modType.Name.Replace("PQSMod_", "").Replace("PQS", "")}Regional",
                                modLoader);
                        {
                            PropertyGen multiplierMap = loaderGen.Public.Property(typeof(MapSOParserRGB<MapSO>), "multiplierMap")
                                .Attribute(typeof(ParserTarget), "multiplierMap");
                            {
                                CodeGen getter = multiplierMap.Getter();
                                {
                                    getter.Return(getter.Base().Property("Mod").Field("multiplierMap"));
                                }
                                CodeGen setter = multiplierMap.Setter();
                                {
                                    setter.Assign(setter.Base().Property("Mod").Field("multiplierMap"),
                                        setter.PropertyValue());
                                }
                            }
                            
                            PropertyGen splitChannels = loaderGen.Public.Property(typeof(NumericParser<Boolean>), "splitChannels")
                                .Attribute(typeof(ParserTarget), "splitChannels");
                            {
                                CodeGen getter = splitChannels.Getter();
                                {
                                    getter.Return(getter.Base().Property("Mod").Field("splitChannels"));
                                }
                                CodeGen setter = splitChannels.Setter();
                                {
                                    setter.Assign(setter.Base().Property("Mod").Field("splitChannels"),
                                        setter.PropertyValue());
                                }
                            }
                            
                            FieldGen loader = loaderGen.Public.Field(loaderType, "loader")
                                .BeginAttribute(typeof(ParserTarget), "Mod").SetField("AllowMerge", true).End();

                            CodeGen create_PQS = loaderGen.Public.Override.Method(typeof(void), "Create")
                                .Parameter(typeof(PQS), "pqsVersion");
                            {
                                ContextualOperand pqsVersion = create_PQS.Arg("pqsVersion");
                                create_PQS.Invoke(create_PQS.Base(), "Create", pqsVersion);
                                create_PQS.Assign(loader, assembly.ExpressionFactory.New(loaderType));
                                create_PQS.Invoke(loader, "Create", create_PQS.Base().Property("Mod"), pqsVersion);
                            }
                            CodeGen create_Mod_PQS = loaderGen.Public.Override.Method(typeof(void), "Create")
                                .Parameter(modGen, "_mod")
                                .Parameter(typeof(PQS), "pqsVersion");
                            {
                                ContextualOperand _mod = create_Mod_PQS.Arg("_mod");
                                ContextualOperand pqsVersion = create_Mod_PQS.Arg("pqsVersion");
                                create_Mod_PQS.Invoke(create_Mod_PQS.Base(), "Create", _mod, pqsVersion);
                                create_Mod_PQS.Assign(loader, assembly.ExpressionFactory.New(loaderType));
                                create_Mod_PQS.Invoke(loader, "Create", create_Mod_PQS.Base().Property("Mod"), pqsVersion);
                            }
                        }
                    }
                }
                assembly.Save();
                
                // Hacking into my own mod. Oh well.
                modTypes.AddRange(assembly.GetAssembly().GetTypes());
                typeof(Parser).GetField("_modTypes", BindingFlags.NonPublic | BindingFlags.Static)
                    ?.SetValue(null, modTypes);
            }
        }
    }
}