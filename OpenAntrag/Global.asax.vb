Imports System.Web.Http
Imports System.Web.Optimization
Imports System.Net.Http.Formatting
Imports System.Net.Http.Headers

Public Class MvcApplication
    Inherits System.Web.HttpApplication

    Sub Application_Start()

        MvcHandler.DisableMvcResponseHeader = True

        AreaRegistration.RegisterAllAreas()

        GlobalConfiguration.Configuration.Formatters.Add(
            New CSVMediaTypeFormatter(
                New QueryStringMapping("format", "csv", New MediaTypeHeaderValue("text/csv"))))

        GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(
            New QueryStringMapping("format", "json", New MediaTypeHeaderValue("application/json")))

        GlobalConfiguration.Configuration.Formatters.XmlFormatter.MediaTypeMappings.Add(
            New QueryStringMapping("format", "xml", New MediaTypeHeaderValue("application/xml")))


        'FormatterConfig.RegisterFormatters(GlobalConfiguration.Configuration.Formatters)

        WebApiConfig.Register(GlobalConfiguration.Configuration)
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
        'AuthConfig.RegisterAuth()

        DataDocumentStore.Initialize()

    End Sub
End Class
