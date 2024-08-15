using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PhotoShare.Extensions
{
    public static class ModelStateExtensions
    {
        public static string GetModelStateErrorMeggages(this ModelStateDictionary modelState)
        {
            return string.Join(" , ", modelState.Where(m => m.Value.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid).Select(e => string.Join(" , ", e.Value.Errors.Select(ie => ie.ErrorMessage))));
        }
    }
}
