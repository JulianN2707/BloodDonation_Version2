using System;
using System.Collections;

namespace Solicitudes.Infrastructure.Services.NotificacionesService;

public abstract class Modificator
    {
        protected Hashtable _parameters = new Hashtable();

        public Hashtable Parameters
        {
            get { return _parameters; }
        }

        public abstract void Apply(ref string Value, params string[] Parameters);
    }

    internal class NL2BR : Modificator
    {
        public override void Apply(ref string Value, params string[] Parameters)
        {
            Value = Value.Replace("\n", "<br>");
        }
    }

    internal class HTMLENCODE : Modificator
    {
        public override void Apply(ref string Value, params string[] Parameters)
        {
            Value = Value.Replace("&", "&amp;");
            Value = Value.Replace("<", "&lt;");
            Value = Value.Replace(">", "&gt;");
        }
    }

    internal class UPPER : Modificator
    {
        public override void Apply(ref string Value, params string[] Parameters)
        {
            Value = Value.ToUpper();
        }
    }

    internal class LOWER : Modificator
    {
        public override void Apply(ref string Value, params string[] Parameters)
        {
            Value = Value.ToLower();
        }
    }

    internal class TRIM : Modificator
    {
        public override void Apply(ref string Value, params string[] Parameters)
        {
            Value = Value.Trim();
        }
    }

    internal class TRIMEND : Modificator
    {
        public override void Apply(ref string Value, params string[] Parameters)
        {
            Value = Value.TrimEnd();
        }
    }

    internal class TRIMSTART : Modificator
    {
        public override void Apply(ref string Value, params string[] Parameters)
        {
            Value = Value.TrimStart();
        }
    }

    internal class DEFAULT : Modificator
    {
        public override void Apply(ref string Value, params string[] Parameters)
        {
            if (Value == null || Value.Trim() == string.Empty)
                Value = Parameters[0];
        }
    }