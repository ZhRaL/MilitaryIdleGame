using DefaultNamespace;
using UnityEngine;

namespace Util
{
    public static class InterfaceExtensions
    {
        
        public static JsonController Save(this IController controller)
        {
            JsonController contr = new JsonController();
            contr.AddManager(controller.ArmyManager.Save());
            contr.AddManager(controller.AirforceManager.Save());
            contr.AddManager(controller.MarineManager.Save());
            return contr;
        }

        public static void Load(this IController controller, JsonController state)
        {
            controller.ArmyManager.Load(state.GetAt(0));
            controller.AirforceManager.Load(state.GetAt(1));
            controller.MarineManager.Load(state.GetAt(2));
        }
    }
}