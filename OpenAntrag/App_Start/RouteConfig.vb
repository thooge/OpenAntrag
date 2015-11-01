Imports System.Web.Mvc
Imports System.Web.Routing

Public Class RouteConfig

    Public Shared Sub RegisterRoutes(ByVal routes As RouteCollection)

        routes.IgnoreRoute("{resource}.axd/{*pathInfo}")

        routes.MapRoute(name:="Overview", url:="Overview",
                        defaults:=New With {.controller = "Home", .action = "Overview"})

        routes.MapRoute(name:="Faq", url:="faq",
                        defaults:=New With {.controller = "Home", .action = "Faq"})

        routes.MapRoute(name:="Api", url:="api",
                        defaults:=New With {.controller = "Home", .action = "Api"})
        routes.MapRoute(name:="ApiDE", url:="schnittstellen",
                        defaults:=New With {.controller = "Home", .action = "Api"})

        routes.MapRoute(name:="ProposalAllFeed", url:="feed",
                        defaults:=New With {.controller = "Home", .action = "ProposalAllFeed"})

        routes.MapRoute(name:="Journal", url:="journal/{pageNo}",
                        defaults:=New With {.controller = "Home", .action = "Journal", .pageNo = 1})

        routes.MapRoute(name:="Success", url:="success/{pageNo}",
                        defaults:=New With {.controller = "Home", .action = "Success", .pageNo = 1})
        routes.MapRoute(name:="SuccessDE", url:="erfolge/{pageNo}",
                        defaults:=New With {.controller = "Home", .action = "Success", .pageNo = 1})

        routes.MapRoute(name:="List", url:="list/",
                        defaults:=New With {.controller = "Home", .action = "List"})
        routes.MapRoute(name:="ListDE", url:="liste/",
                        defaults:=New With {.controller = "Home", .action = "List"})

        routes.MapRoute(name:="Search", url:="search/{searchTerms}/{pageNo}",
                        defaults:=New With {.controller = "Home", .action = "Search", .searchTerms = "", .pageNo = 1})
        routes.MapRoute(name:="SearchDE", url:="suche/{searchTerms}/{pageNo}",
                        defaults:=New With {.controller = "Home", .action = "Search", .searchTerms = "", .pageNo = 1})

        routes.MapRoute(name:="Tags", url:="tags/{tag}/{pageNo}",
                        defaults:=New With {.controller = "Home", .action = "Tags", .tag = "", .pageNo = 1})
        routes.MapRoute(name:="TagsDE", url:="themen/{tag}/{pageNo}",
                        defaults:=New With {.controller = "Home", .action = "Tags", .tag = "", .pageNo = 1})

        routes.MapRoute(name:="FeedbackService", url:="feedback/service/{action}",
                        defaults:=New With {.controller = "Feedback", .action = ""})

        routes.MapRoute(name:="Feedback", url:="feedback/{key}",
                        defaults:=New With {.controller = "Feedback", .action = "Index", .key = ""})

        routes.MapRoute(name:="NotificationsFeed", url:="notifications/feed/{type}",
                        defaults:=New With {.controller = "Notifications", .action = "Feed", .type = -1})
        routes.MapRoute(name:="NotificationsFeedDE", url:="mitteilungen/feed/{type}",
                        defaults:=New With {.controller = "Notifications", .action = "Feed", .type = -1})

        routes.MapRoute(name:="NotificationsService", url:="notifications/service/{action}",
                        defaults:=New With {.controller = "Notifications", .action = ""})

        routes.MapRoute(name:="Notifications", url:="notifications/{type}",
                        defaults:=New With {.controller = "Notifications", .action = "Index", .type = ""})
        routes.MapRoute(name:="NotificationsDE", url:="mitteilungen/{type}",
                        defaults:=New With {.controller = "Notifications", .action = "Index", .type = ""})

        routes.MapRoute(name:="Statistics", url:="statistics/{statPart}",
                        defaults:=New With {.controller = "Statistics", .action = "Index", .statPart = ""})
        routes.MapRoute(name:="StatisticsDE", url:="statistiken/{statPart}",
                        defaults:=New With {.controller = "Statistics", .action = "Index", .statPart = ""})

        routes.MapRoute(name:="AllRepresentationsStyle", url:="allrepresentationstyle.css",
                        defaults:=New With {.controller = "Home", .action = "AllRepresentationsStyle"})


        '*** Representation/Proposal-Routes ***
        'wiesbaden              > Representation
        'wiesbaden/1            > Representation-Page
        'wiesbaden/mein-antrag  > Proposal

        For Each rep As Representation In GlobalData.Representations.Items
            'Representation

            routes.MapRoute(name:=String.Concat("RepresentationHome-", rep.Key), url:=rep.Key,
                            defaults:=New With {.controller = "Representation", .action = "Index", .keyRepresentation = rep.Key})

            routes.MapRoute(name:=String.Concat("RepresentationJournal-", rep.Key), url:=String.Concat(rep.Key, "/journal/{pageNo}"),
                            defaults:=New With {.controller = "Representation", .action = "Journal", .keyRepresentation = rep.Key, .pageNo = 1},
                            constraints:=New With {.pageNo = "\d+"})

            routes.MapRoute(name:=String.Concat("RepresentationList", rep.Key), url:=String.Concat(rep.Key, "/list"),
                            defaults:=New With {.controller = "Representation", .action = "List", .keyRepresentation = rep.Key})
            routes.MapRoute(name:=String.Concat("RepresentationListDE", rep.Key), url:=String.Concat(rep.Key, "/liste"),
                            defaults:=New With {.controller = "Representation", .action = "List", .keyRepresentation = rep.Key})

            If (rep.Status And Representations.StatusConjuction.Ended) = 0 Then
                routes.MapRoute(name:=String.Concat("RepresentationAdd", rep.Key), url:=String.Concat(rep.Key, "/add"),
                                defaults:=New With {.controller = "Representation", .action = "Add", .keyRepresentation = rep.Key})
                routes.MapRoute(name:=String.Concat("RepresentationAddDE", rep.Key), url:=String.Concat(rep.Key, "/neu"),
                                defaults:=New With {.controller = "Representation", .action = "Add", .keyRepresentation = rep.Key})

                routes.MapRoute(name:=String.Concat("RepresentationBanner", rep.Key), url:=String.Concat(rep.Key, "/banner"),
                                defaults:=New With {.controller = "Representation", .action = "Banner", .keyRepresentation = rep.Key})

                routes.MapRoute(name:=String.Concat("RepresentationSettings", rep.Key), url:=String.Concat(rep.Key, "/einstellungen"),
                                defaults:=New With {.controller = "Representation", .action = "Settings", .keyRepresentation = rep.Key})
            End If

            routes.MapRoute(name:=String.Concat("RepresentationStyle", rep.Key), url:=String.Concat(rep.Key, "/style-representation"),
                            defaults:=New With {.controller = "Representation", .action = "RepresentationStyle", .keyRepresentation = rep.Key})

            routes.MapRoute(name:=String.Concat("RepresentationFeedRoute", rep.Key), url:=String.Concat(rep.Key, "/feed"),
                            defaults:=New With {.controller = "Representation", .action = "ProposalFeed", .keyRepresentation = rep.Key})

            'Proposal
            routes.MapRoute(name:=String.Concat("ProposalRoute", rep.Key), url:=String.Concat(rep.Key, "/{titleUrl}"),
                            defaults:=New With {.controller = "Representation", .action = "Proposal", .keyRepresentation = rep.Key, .titleUrl = ""})

            'SuccessStory
            routes.MapRoute(name:=String.Concat("SuccessStoryRoute", rep.Key), url:=String.Concat(rep.Key, "/{titleUrl}/success"),
                            defaults:=New With {.controller = "Representation", .action = "SuccessStory", .keyRepresentation = rep.Key, .titleUrl = ""})
            routes.MapRoute(name:=String.Concat("SuccessStoryRouteDE", rep.Key), url:=String.Concat(rep.Key, "/{titleUrl}/erfolg"),
                            defaults:=New With {.controller = "Representation", .action = "SuccessStory", .keyRepresentation = rep.Key, .titleUrl = ""})

        Next

        Dim tss As New Teasers
        For Each tsr As Teaser In tss.Items

            routes.MapRoute(name:=String.Concat("TeaserHome-", tsr.Key), url:=tsr.TeaserUrl,
                            defaults:=New With {.controller = "Teaser", .action = "Index", .keyTeaser = tsr.Key})

            routes.MapRoute(name:=String.Concat("TeaserStyle", tsr.Key), url:=String.Concat(tsr.TeaserUrl, "/style-teaser"),
                            defaults:=New With {.controller = "Teaser", .action = "TeaserStyle", .keyTeaser = tsr.Key})

        Next

        routes.MapRoute(name:="Error", url:="uups/{action}",
                        defaults:=New With {.controller = "Error", .action = "Index"})

        routes.MapRoute(name:="ErrorLog", url:="errors/{id}",
                        defaults:=New With {.controller = "Error", .action = "ErrorLog"})

        routes.MapRoute(name:="Default", url:="{controller}/{action}",
                        defaults:=New With {.controller = "Home", .action = "Index"})

    End Sub

End Class