Imports System.Web.Optimization

Public Module BundleConfig
    ' Per altre informazioni sulla creazione di bundle, vedere https://go.microsoft.com/fwlink/?LinkId=301862
    Public Sub RegisterBundles(ByVal bundles As BundleCollection)

        bundles.Add(New ScriptBundle("~/bundles/jquery").Include(
                    "~/Scripts/jquery-{version}.js"))

        bundles.Add(New ScriptBundle("~/bundles/jqueryval").Include(
                    "~/Scripts/jquery.validate*"))

        ' Utilizzare la versione di sviluppo di Modernizr per eseguire attività di sviluppo e formazione. Successivamente, quando si è
        ' pronti per passare alla produzione, usare lo strumento di compilazione disponibile all'indirizzo https://modernizr.com per selezionare solo i test necessari.
        bundles.Add(New ScriptBundle("~/bundles/modernizr").Include(
                    "~/Scripts/modernizr-*"))
        bundles.Add(New ScriptBundle("~/bundles/dropzone").Include(
                     "~/Scripts/dropzone/dropzone.js"))
        'bundles.Add(New ScriptBundle("~/bundles/bootstrap").Include(
        '          "~/Scripts/bootstrap.js"))
        bundles.Add(New ScriptBundle("~/bundles/notify").Include(
                  "~/Scripts/bootstrap.notify.js"))
        bundles.Add(New StyleBundle("~/Content/css").Include(
                  "~/Content/site.css",
                  "~/Content/fontawesome.css",
                  "~/Content/bootstrap.min.css",
                  "~/Content/jtimeline.css",
                   "~/Content/guides.css",
                   "~/Content/datatable.min.css",
                  "~/Content/font-awesome.css"))
    End Sub
End Module
