using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Pdf.BusinessLogic.Providers.Interfaces;

namespace Pdf.BusinessLogic.Providers;

public class RazorProvider: IRazorProvider
{
    private readonly IRazorViewEngine _razorViewEngine;
    private readonly IServiceProvider _serviceProvider;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITempDataProvider _tempDataProvider;

    public RazorProvider(IRazorViewEngine razorViewEngine, IHttpContextAccessor httpContextAccessor,
        ITempDataProvider tempDataProvider, IServiceProvider serviceProvider)
    {
        _razorViewEngine = razorViewEngine;
        _httpContextAccessor = httpContextAccessor;
        _tempDataProvider = tempDataProvider;
        _serviceProvider = serviceProvider;
    }

    public async Task<string> RenderPageAsync<T>(string path, T model)
    {
        var httpContext = new DefaultHttpContext
        {
            RequestServices = _serviceProvider,
            User = _httpContextAccessor.HttpContext.User
        };

        var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

        await using var writer = new StringWriter();

        ViewEngineResult viewEngineResult = _razorViewEngine.GetView(null, path, false);
        
        if (viewEngineResult.View == null)
        {
            throw new ArgumentException($"{path} is not a view or it doesn't exists.");
        }
        
        var viewDataDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(),
            new ModelStateDictionary()) { Model = model };

        var viewContext = new ViewContext(actionContext, viewEngineResult.View, viewDataDictionary,
            new TempDataDictionary(actionContext.HttpContext, _tempDataProvider), writer,
            new HtmlHelperOptions());

        await viewEngineResult.View.RenderAsync(viewContext);

        return writer.ToString();
    }
}