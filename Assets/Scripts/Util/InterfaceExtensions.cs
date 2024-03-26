using DefaultNamespace;
using UnityEngine;

namespace Util
{
    public static class InterfaceExtensions
    {
        
        public static JsonController<T> Save<T>(this IController controller)
        {
            JsonController<T> contr = new JsonController<T>();
            contr.AddManager(controller.ArmyManager.Save<T>());
            contr.AddManager(controller.AirforceManager.Save<T>());
            contr.AddManager(controller.MarineManager.Save<T>());
            return contr;
        }

        public static void Load<T>(this IController controller, JsonController<T> state)
        {
            controller.ArmyManager.Load(state.GetAt(0));
            controller.AirforceManager.Load(state.GetAt(1));
            controller.MarineManager.Load(state.GetAt(2));
        }
    }
}