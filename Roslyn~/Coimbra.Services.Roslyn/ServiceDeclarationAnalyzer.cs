﻿using Coimbra.Roslyn;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace Coimbra.Services.Roslyn
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class ServiceDeclarationAnalyzer : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Diagnostics.ConcreteServiceShouldOnlyImplementOneService,
                                                                                                           Diagnostics.ConcreteServiceShouldNotImplementAbstractService,
                                                                                                           Diagnostics.BaseClassIsConcreteServiceAlready,
                                                                                                           Diagnostics.InheritFromServiceActorBaseInstead,
                                                                                                           Diagnostics.ScriptableSettingsShouldNotImplementService);

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.RegisterSyntaxNodeAction(AnalyzeServiceDeclaration, SyntaxKind.ClassDeclaration);
        }

        private static void AnalyzeServiceDeclaration(SyntaxNodeAnalysisContext context)
        {
            if (context.Node is not ClassDeclarationSyntax classDeclarationSyntax
             || classDeclarationSyntax.Modifiers.Any(SyntaxKind.AbstractKeyword)
             || context.SemanticModel.GetDeclaredSymbol(context.Node) is not INamedTypeSymbol typeSymbol)
            {
                return;
            }

            if (!ImplementsService(typeSymbol, out int abstractServiceCount, out int concreteServiceCount))
            {
                return;
            }

            typeSymbol = typeSymbol.BaseType;

            while (typeSymbol != null)
            {
                string containingNamespace = typeSymbol.ContainingNamespace.ToString();

                if (typeSymbol.Name == CoimbraServicesTypes.ServiceActorBaseClass.Name && containingNamespace == CoimbraServicesTypes.ServiceActorBaseClass.Namespace)
                {
                    break;
                }

                if (typeSymbol.Name == CoimbraTypes.ScriptableSettingsClass.Name && containingNamespace == CoimbraTypes.ScriptableSettingsClass.Namespace)
                {
                    context.ReportDiagnostic(Diagnostic.Create(Diagnostics.ScriptableSettingsShouldNotImplementService, classDeclarationSyntax.BaseList!.GetLocation(), classDeclarationSyntax.GetTypeName()));

                    return;
                }

                if (typeSymbol.Name == UnityEngineTypes.ComponentClass.Name && containingNamespace == UnityEngineTypes.ComponentClass.Namespace)
                {
                    context.ReportDiagnostic(Diagnostic.Create(Diagnostics.InheritFromServiceActorBaseInstead, classDeclarationSyntax.BaseList!.GetLocation(), classDeclarationSyntax.GetTypeName()));

                    return;
                }

                if (ImplementsService(typeSymbol, out _, out int parentServiceCount) && parentServiceCount > 0)
                {
                    context.ReportDiagnostic(Diagnostic.Create(Diagnostics.BaseClassIsConcreteServiceAlready, classDeclarationSyntax.BaseList!.GetLocation(), classDeclarationSyntax.GetTypeName(), typeSymbol.Name));

                    return;
                }

                typeSymbol = typeSymbol.BaseType;
            }

            if (abstractServiceCount > 0)
            {
                context.ReportDiagnostic(Diagnostic.Create(Diagnostics.ConcreteServiceShouldNotImplementAbstractService, classDeclarationSyntax.BaseList!.GetLocation(), classDeclarationSyntax.GetTypeName()));
            }

            if (concreteServiceCount > 1)
            {
                context.ReportDiagnostic(Diagnostic.Create(Diagnostics.ConcreteServiceShouldOnlyImplementOneService, classDeclarationSyntax.BaseList!.GetLocation(), classDeclarationSyntax.GetTypeName()));
            }
        }

        private static bool ImplementsService(ITypeSymbol typeSymbol, out int abstractCount, out int concreteCount)
        {
            abstractCount = 0;
            concreteCount = 0;

            foreach (INamedTypeSymbol interfaceSymbol in typeSymbol.Interfaces)
            {
                if (!interfaceSymbol.IsOrImplementsInterface(CoimbraServicesTypes.ServiceInterface))
                {
                    continue;
                }

                if (interfaceSymbol.HasAttribute(CoimbraServicesTypes.AbstractServiceAttribute, out _))
                {
                    abstractCount++;
                }
                else
                {
                    concreteCount++;
                }
            }

            return abstractCount + concreteCount > 0;
        }
    }
}
