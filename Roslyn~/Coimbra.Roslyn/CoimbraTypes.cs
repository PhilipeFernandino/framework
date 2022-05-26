﻿namespace Coimbra.Roslyn
{
    public static class CoimbraTypes
    {
        public const string Namespace = "Coimbra";

        public static readonly TypeString ActorClass = new("Actor", Namespace);

        public static readonly TypeString ManagedPoolClass = new("ManagedPool", Namespace);

        public static readonly TypeString ScriptableSettingsClass = new("ScriptableSettings", Namespace);

        public static readonly TypeString SharedManagedPoolAttribute = new("SharedManagedPoolAttribute", Namespace);
    }
}
