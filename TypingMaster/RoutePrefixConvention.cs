using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace TypingMaster;

public class RoutePrefixConvention(string routePrefix) : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        controller.Selectors[0].AttributeRouteModel = new AttributeRouteModel
        {
            Template = $"{routePrefix}/{controller.ControllerName}"
        };
    }
}