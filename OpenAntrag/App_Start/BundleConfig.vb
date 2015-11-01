Imports System.Web
Imports System.Web.Optimization

Public Class BundleConfig
    ' For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725

    Public Shared Sub RegisterBundles(ByVal bundles As BundleCollection)

        bundles.Add(New ScriptBundle("~/bundle/jquery").Include(
                   "~/Scripts/jquery-{version}.js"))

        bundles.Add(New ScriptBundle("~/bundle/jqueryval").Include(
                    "~/Scripts/jquery.validate*"))

        ' Use the development version of Modernizr to develop with and learn from. Then, when you're
        ' ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
        bundles.Add(New ScriptBundle("~/bundle/modernizr").Include(
                    "~/Scripts/modernizr-*"))

        bundles.Add(New ScriptBundle("~/bundle/plugins-pre") _
                    .IncludeDirectory("~/Scripts/Plugins/_preload", "*.js"))

        bundles.Add(New ScriptBundle("~/bundle/plugins") _
                    .IncludeDirectory("~/Scripts/Plugins", "*.js") _
                    .Include("~/Scripts/Plugins/moment/moment.js") _
                    .Include("~/Scripts/Plugins/moment/de.js") _
                    .Include("~/Scripts/Plugins/bootstrap.js") _
                    .Include("~/Scripts/Plugins/datetimepicker/bootstrap-datetimepicker.js") _
                    .Include("~/Scripts/Plugins/datetimepicker/bootstrap-datetimepicker.de-DE.js") _
                    .Include("~/Scripts/headroom.js") _
                    .Include("~/Scripts/Plugins/perfect-scrollbar/perfect-scrollbar.js"))

        bundles.Add(New ScriptBundle("~/bundle/markdown") _
                    .Include("~/Scripts/MarkdownDeep.js") _
                    .Include("~/Scripts/MarkdownDeepEditor.js") _
                    .Include("~/Scripts/MarkdownDeepEditorUI.js"))

        bundles.Add(New ScriptBundle("~/bundle/main").Include(
                    "~/ScriptsCustom/tools.js",
                    "~/ScriptsCustom/xhr.js",
                    "~/ScriptsCustom/main.js",
                    "~/ScriptsCustom/validation.js",
                    "~/ScriptsCustom/responsive-tables.js"))

        bundles.Add(New ScriptBundle("~/bundle/representations").Include(
                    "~/ScriptsCustom/representations.js"))

        bundles.Add(New ScriptBundle("~/bundle/teaser").Include(
                    "~/ScriptsCustom/teaser.js"))

        bundles.Add(New ScriptBundle("~/bundle/feedback").Include(
                    "~/ScriptsCustom/feedback.js"))

        bundles.Add(New ScriptBundle("~/bundle/notifications").Include(
                    "~/ScriptsCustom/notifications.js"))

        bundles.Add(New ScriptBundle("~/bundle/admin") _
                    .Include("~/ScriptsCustom/admin.js") _
                    .Include("~/Scripts/Plugins/picker/picker.js"))

        'NICHT  'IncludeDirectory', weil Reihenfolge wichtig !!!
        bundles.Add(New StyleBundle("~/css/plugins").Include(
                    "~/Content/Plugins/flatstrap.css",
                    "~/Content/Plugins/flatstrap-responsive.css",
                    "~/Content/Plugins/tooltipster.css",
                    "~/Content/Plugins/bootstrap-select.css",
                    "~/Content/Plugins/selectize.css",
                    "~/Content/Plugins/jqcloud.css",
                    "~/Scripts/mdd_styles.css",
                    "~/Content/Plugins/bootstrap-datetimepicker.css",
                    "~/Scripts/Plugins/perfect-scrollbar/perfect-scrollbar.css"))
        '"~/Content/Plugins/jquery.mCustomScrollbar.css",
        '"~/Content/Plugins/jquery.mCustomScrollbar.openantrag.css"))
        '"~/Content/Plugins/jquery.jscrollpane.css",

        bundles.Add(New StyleBundle("~/css/styles-v2") _
                    .Include("~/Content/style.css") _
                    .Include("~/Content/domain.css"))

        bundles.Add(New StyleBundle("~/css/home").Include("~/Content/home.css"))
        bundles.Add(New StyleBundle("~/css/statistics").Include("~/Content/statistics.css"))
        bundles.Add(New StyleBundle("~/css/feedback").Include("~/Content/feedback.css"))
        bundles.Add(New StyleBundle("~/css/api").Include("~/Content/api.css"))
        bundles.Add(New StyleBundle("~/css/faq").Include("~/Content/faq.css"))
        bundles.Add(New StyleBundle("~/css/list").Include("~/Content/list.css"))
        bundles.Add(New StyleBundle("~/css/search").Include("~/Content/search.css"))
        bundles.Add(New StyleBundle("~/css/success").Include("~/Content/success.css").Include("~/Content/proposal.css"))
        bundles.Add(New StyleBundle("~/css/tags").Include("~/Content/tags.css"))
        bundles.Add(New StyleBundle("~/css/notifications").Include("~/Content/notifications.css"))
        bundles.Add(New StyleBundle("~/css/error").Include("~/Content/error.css"))

        For Each r As Representation In GlobalData.Representations.Items

            bundles.Add(New StyleBundle(String.Concat("~/css/representations-", r.Key)) _
                        .Include("~/Content/representation.css") _
                        .Include(String.Concat("~/Content/Representations/", r.Key, "/style-", r.Key, ".css")))

            bundles.Add(New StyleBundle(String.Concat("~/css/proposal-", r.Key)) _
                        .Include("~/Content/proposal.css") _
                        .Include(String.Concat("~/Content/Representations/", r.Key, "/style-", r.Key, ".css")))
        Next

        Dim tss As New Teasers
        For Each t As Teaser In tss.Items

            bundles.Add(New StyleBundle(String.Concat("~/css/teaser-", t.Key)) _
            .Include("~/Content/teaser.css") _
            .Include(String.Concat("~/Content/Teaser/", t.Key, "/style-", t.Key, ".css")))

        Next

    End Sub

End Class