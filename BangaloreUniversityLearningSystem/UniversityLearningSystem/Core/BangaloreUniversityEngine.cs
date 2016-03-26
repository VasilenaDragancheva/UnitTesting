namespace UniversityLearningSystem.Core
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Data;

    using Interfaces;

    public class BangaloreUnibersityEngine : IEngine
    {
        public void Run()
        {
            var bangaloreUniversityData = new BangaloreUniversityData();
            User u = null;
            while (true)
            {
                string str = Console.ReadLine();
                if (str == null)
                {
                    break;
                }

                var route = new Route(str);
                var controllerType =
                    Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(type => type.Name == route.ControllerName);
                var ctrl = Activator.CreateInstance(controllerType, bangaloreUniversityData, u) as Controller;
                var act = controllerType.GetMethod(route.ActionName);
                object[] @params = MapParameters(route, act);
                try
                {
                    var view = act.Invoke(ctrl, @params) as IView;
                    Console.WriteLine(view.Display());
                    u = ctrl.User;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
        }

        private static object[] MapParameters(Route route, MethodInfo action)
        {
            return action.GetParameters().Select<ParameterInfo, object>(
                p =>
                    {
                        if (p.ParameterType == typeof(int))
                        {
                            return int.Parse(route.Parameters[p.Name]);
                        }

                        return route.Parameters[p.Name];
                    }).ToArray();
        }
    }
}