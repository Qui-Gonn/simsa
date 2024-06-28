namespace Simsa.Ui.Library.Components.Layout;

public partial class MainLayout
{
    private bool drawerOpen;

    private void DrawerToggle()
    {
        this.drawerOpen = !this.drawerOpen;
    }
}