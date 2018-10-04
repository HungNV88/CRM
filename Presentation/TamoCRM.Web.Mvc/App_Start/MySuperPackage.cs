using System;

[assembly: WebActivator.PreApplicationStartMethod(
    typeof(TamoCRM.Web.Mvc.App_Start.MySuperPackage), "PreStart")]

namespace TamoCRM.Web.Mvc.App_Start {
    public static class MySuperPackage {
        public static void PreStart() {
            MVCControlsToolkit.Core.Extensions.Register();
        }
    }
}