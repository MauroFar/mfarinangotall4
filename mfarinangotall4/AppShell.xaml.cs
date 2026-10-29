namespace mfarinangotall4
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ContactSummaryPage), typeof(ContactSummaryPage));
        }
    }
}
