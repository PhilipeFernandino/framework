﻿using Coimbra.Roslyn;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using System.Collections.Immutable;

namespace Coimbra.Services.Events.Roslyn
{
    [ExportCodeFixProvider(LanguageNames.CSharp)]
    public sealed class EventDeclarationNestedTypeCodeFix : MoveToOuterScopeCodeFix
    {
        public override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(Diagnostics.ConcreteEventShouldNotBeNested.Id);
    }
}
