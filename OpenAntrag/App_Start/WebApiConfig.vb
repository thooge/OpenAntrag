
Imports System.Web.Http

Public Class WebApiConfig
    Public Shared Sub Register(ByVal config As HttpConfiguration)

        Dim routes As HttpRouteCollection = config.Routes

        '*************************************************************************

        routes.MapHttpRoute(name:="RepresentationApi",
                            routeTemplate:="api/representation/{action}/{key}",
                            defaults:=New With {.controller = "RepresentationApi",
                                                .key = RouteParameter.Optional})

        routes.MapHttpRoute(name:="RepresentationApiExt",
                            routeTemplate:="api/representation/{action}/{key}/{id}",
                            defaults:=New With {.controller = "RepresentationApi",
                                                .key = "",
                                                .id = ""})

        '*************************************************************************

        '(muss VOR der Default-Route stehen, damit ALL nicht als Action interpretiert wird)
        routes.MapHttpRoute(name:="ProposalApiItemsCount",
                            routeTemplate:="api/proposal/{key}/getcount",
                            defaults:=New With {.controller = "ProposalApi",
                                                .key = "ALL",
                                                .action = "GetCount"})

        routes.MapHttpRoute(name:="ProposalApi",
                            routeTemplate:="api/proposal/{action}/{id}",
                            defaults:=New With {.controller = "ProposalApi",
                                                .id = RouteParameter.Optional})

        routes.MapHttpRoute(name:="ProposalApiTitleUrl",
                            routeTemplate:="api/proposal/{key}/getbytitleurl/{titleUrl}",
                            defaults:=New With {.controller = "ProposalApi",
                                                .key = "",
                                                .titleUrl = ""})

        routes.MapHttpRoute(name:="ProposalApiItemsTop",
                            routeTemplate:="api/proposal/{key}/gettop/{count}",
                            defaults:=New With {.controller = "ProposalApi",
                                                .key = "ALL",
                                                .action = "GetTop",
                                                .count = 5})

        routes.MapHttpRoute(name:="ProposalApiItemsPage",
                            routeTemplate:="api/proposal/{key}/getpage/{pageNo}/{pageCount}",
                            defaults:=New With {.controller = "ProposalApi",
                                                .key = "ALL",
                                                .action = "GetPage",
                                                .pageNo = 1,
                                                .pageCount = 5})

        routes.MapHttpRoute(name:="ProposalApiItemsTag",
                            routeTemplate:="api/proposal/{key}/getbytag/{tag}",
                            defaults:=New With {.controller = "ProposalApi",
                                                .key = "ALL",
                                                .action = "GetByTag",
                                                .tag = ""})

        routes.MapHttpRoute(name:="ProposalApiTags",
                            routeTemplate:="api/proposal/gettags",
                            defaults:=New With {.controller = "ProposalApi",
                                                .action = "GetTags"})


        routes.MapHttpRoute(name:="ProposalApiPost",
                            routeTemplate:="api/proposal/{action}",
                            defaults:=New With {.controller = "ProposalApi"})

        '*************************************************************************

        routes.MapHttpRoute(name:="NotificationsApiItemsLast",
                            routeTemplate:="api/notifications/getlast/{count}",
                            defaults:=New With {.controller = "NotificationsApi",
                                                .action = "GetLast",
                                                .count = 5})

        routes.MapHttpRoute(name:="NotificationsApiItemsLastByType",
                            routeTemplate:="api/notifications/getlastbytype/{typeId}/{count}",
                            defaults:=New With {.controller = "NotificationsApi",
                                                .action = "GetLastByType",
                                                .typeId = 0,
                                                .count = 5})

        routes.MapHttpRoute(name:="NotificationsApi",
                            routeTemplate:="api/notifications/{action}/{key}",
                            defaults:=New With {.controller = "NotificationsApi",
                                                .key = RouteParameter.Optional})

        '*************************************************************************

        'routes.MapHttpRoute(name:="DefaultApi",
        '                    routeTemplate:="api/{controller}/{id}",
        '                    defaults:=New With {.id = RouteParameter.Optional})

        'Uncomment the following line of code to enable query support for actions 
        'with an IQueryable or IQueryable(Of T) return type.
        'To avoid processing unexpected or malicious queries, use the validation 
        'settings on QueryableAttribute to validate incoming queries.
        'For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
        'config.EnableQuerySupport()

    End Sub
End Class