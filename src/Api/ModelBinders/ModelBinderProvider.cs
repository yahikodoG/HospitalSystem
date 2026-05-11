using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.ModelBinders;

public class ModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType == typeof(string))
        {
            return new TrimModelBinder();
        }

        return null;
    }
}